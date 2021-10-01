using MongoDB.Bson;
using MongoDB.Controllers;
using MongoDB.Models;
using System;
using System.Collections.Generic;

namespace UI
{
    public class BL
    {
        public DataController dt = new DataController("MongoSocialNetwork");
        public List<Post> posts;
        public List<User> users;
        public List<Comment> comments;


        public BL()
        {
            posts = dt.ReadEntity<Post>("Posts");
            users = dt.ReadEntity<User>("Users");
            comments = dt.ReadEntity<Comment>("Comments");
        }
        public void AddPost(ObjectId userId, Post post)
        {
            dt.InsertEntity<Post>("Posts", post);
            var getUser = dt.GetEntityById<User>("Users", userId);
            getUser.PostsId.Add(post.Id);
            dt.UpsertEntity("Users", getUser.Id, getUser);
        }

        public void AddComment(ObjectId postId, Comment comment)
        {
            dt.InsertEntity<Comment>("Comments", comment);
            var getPost = dt.GetEntityById<Post>("Posts", postId);
            getPost.CommentsId.Add(comment.Id);
            dt.UpsertEntity("Posts", getPost.Id, getPost);
        }

        public void AddLike(ObjectId postId, ObjectId userId)
        {
            var getPost = dt.GetEntityById<Post>("Posts", postId);
            getPost.UserLikesId.Add(userId);
            dt.UpsertEntity("Posts", getPost.Id, getPost);
        }

        public void DeleteLike(ObjectId postId, ObjectId userId)
        {
            var post = dt.GetEntityById<Post>("Posts", postId);
            post.UserLikesId.Remove(userId);
            dt.UpsertEntity<Post>("Posts", postId, post);
        }


        public void AddFollower(ObjectId CurrentUserId, ObjectId FollowerUserId)
        {
            var getUser = dt.GetEntityById<User>("Users", FollowerUserId);
            getUser.FollowersId.Add(CurrentUserId);
            dt.UpsertEntity("Users", getUser.Id, getUser);
        }

        public void DeleteFollower(ObjectId CurrentUserId, ObjectId FollowerUserId)
        {
            var getUser = dt.GetEntityById<User>("Users", FollowerUserId);
            getUser.FollowersId.Remove(CurrentUserId);
            dt.UpsertEntity<User>("Users", getUser.Id, getUser);
        }

        public void AddFollowing(ObjectId CurrentUserId, ObjectId FollowerUserId)
        {
            var getUser = dt.GetEntityById<User>("Users", CurrentUserId);
            getUser.FollowingsId.Add(FollowerUserId);
            dt.UpsertEntity("Users", getUser.Id, getUser);
        }

        public void DeleteFollowing(ObjectId CurrentUserId, ObjectId FollowerUserId)
        {
            var getUser = dt.GetEntityById<User>("Users", CurrentUserId);
            getUser.FollowingsId.Remove(FollowerUserId);
            dt.UpsertEntity<User>("Users", getUser.Id, getUser);
        }

        public List<ObjectId> GetPostLikeById(ObjectId postId)
        {
            var post = dt.GetEntityById<Post>("Posts", postId);
            return post.UserLikesId;
        }

        public List<ObjectId> GetUserFollowers(ObjectId userId)
        {
            var user = dt.GetEntityById<User>("Users", userId);
            return user.FollowersId;
        }

        public List<ObjectId> GetUserFollowings(ObjectId userId)
        {
            var user = dt.GetEntityById<User>("Users", userId);
            return user.FollowingsId;
        }


        public string GetUserNameById(ObjectId userId)
        {
            var user = dt.GetEntityById<User>("Users", userId);
            return user.FirstName + " " + user.SecondName;
        }



        public string like(object Idpost, User DefUser)
        {

            string tmp = Convert.ToString(Idpost);
            tmp = tmp.Remove(0, 1);
            var listLike = GetPostLikeById(ObjectId.Parse(tmp));

            if (listLike.Exists(x => x == DefUser.Id))
            {
                DeleteLike(ObjectId.Parse(tmp), DefUser.Id);
            }
            else
            {
                AddLike(ObjectId.Parse(tmp), DefUser.Id);
            }


            return Convert.ToString(GetPostLikeById(ObjectId.Parse(tmp)).Count);
        }

        public string Followers(object IdUser, User DefUser)
        {
            string tmp = Convert.ToString(IdUser);
            var Followers = GetUserFollowers(ObjectId.Parse(tmp));

            if (Followers.Exists(x => x == DefUser.Id))
            {
                DeleteFollower(DefUser.Id, ObjectId.Parse(tmp));
            }
            else
            {
                AddFollower(DefUser.Id, ObjectId.Parse(tmp));
            }



            return Convert.ToString(GetUserFollowers(ObjectId.Parse(Convert.ToString(DefUser.Id))).Count);
        }

        public string Followings(object IdUser, User DefUser)
        {

            string tmp = Convert.ToString(IdUser);
            var Followings = GetUserFollowings(ObjectId.Parse(Convert.ToString(DefUser.Id)));

            if (Followings.Exists(x => x == ObjectId.Parse(tmp)))
            {
                DeleteFollowing(DefUser.Id, ObjectId.Parse(tmp));
            }
            else
            {
                AddFollowing(DefUser.Id, ObjectId.Parse(tmp));

            }

            return Convert.ToString(GetUserFollowings(ObjectId.Parse(Convert.ToString(DefUser.Id))).Count);
        }

    }
}
