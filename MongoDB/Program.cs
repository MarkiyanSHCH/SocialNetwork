using MongoDB.Bson;
using MongoDB.Controllers;
using MongoDB.Models;
using System;
using System.Collections.Generic;

namespace MongoDB
{
    class Program
    {
        static void AddPost(ObjectId userId, Post post, DataController db)
        {
            db.InsertEntity<Post>("Posts", post);
            var getUser = db.GetEntityById<User>("Users", userId);
            getUser.PostsId.Add(post.Id);
            db.UpsertEntity("Users", getUser.Id, getUser);
        }

        static void AddComment(ObjectId postId, Comment comment, DataController db)
        {
            db.InsertEntity<Comment>("Comments", comment);
            var getPost = db.GetEntityById<Post>("Posts", postId);
            getPost.CommentsId.Add(comment.Id);
            db.UpsertEntity("Posts", getPost.Id, getPost);
        }

        static void AddLike(ObjectId postId, ObjectId userId, DataController db)
        {
            var getPost = db.GetEntityById<Post>("Posts", postId);
            getPost.UserLikesId.Add(userId);
            db.UpsertEntity("Posts", getPost.Id, getPost);
        }

        static void Main(string[] args)
        {
            DataController dt = new DataController("MongoSocialNetwork");
            //create
            User us = new User
            {
                FirstName = "Taras",
                SecondName = "Popip",
                Email = "popip02@gmail.com",
                Password = "1234",

                Interests = new List<string>(),
                FollowersId = new List<ObjectId>(),
                FollowingsId = new List<ObjectId>(),
                PostsId = new List<ObjectId>()

            };

            User us2 = new User
            {
                FirstName = "Peter",
                SecondName = "Lolip",
                Email = "peter013@gmail.com",
                Password = "0990",

                Interests = new List<string>(),
                FollowersId = new List<ObjectId>(),
                FollowingsId = new List<ObjectId>(),
                PostsId = new List<ObjectId>()

            };
            User us4 = new User
            {
                FirstName = "Ostap",
                SecondName = "Elar",
                Email = "ostap987@gmail.com",
                Password = "0987",

                Interests = new List<string>(),
                FollowersId = new List<ObjectId>(),
                FollowingsId = new List<ObjectId>(),
                PostsId = new List<ObjectId>()

            };
            User us3 = new User
            {
                FirstName = "Ivan",
                SecondName = "Terin",
                Email = "ivan043@gmail.com",
                Password = "2345",

                Interests = new List<string>(),
                FollowersId = new List<ObjectId>(),
                FollowingsId = new List<ObjectId>(),
                PostsId = new List<ObjectId>()

            };

            dt.InsertEntity<User>("Users", us);
            dt.InsertEntity<User>("Users", us2);
            dt.InsertEntity<User>("Users", us3);
            dt.InsertEntity<User>("Users", us4);

            //update
            //var User = dt.ReadEntity<User>("Users");
            //var us = User[0];
            //var post = dt.ReadEntity<Post>("Posts");
            //var ps1 = post[0];
            //us.Password = "password";
            //dt.UpsertEntity<User>("Users",us.Id,us);

            //Post ps = new Post
            //{
            //    Text = "Hello guys!",
            //    insertTime = DateTime.Now,
            //    CommentsId= new List<ObjectId>(),
            //    UserLikesId= new List<ObjectId>()
            //};

            //Comment cm = new Comment
            //{
            //    Text = "Hello guys!",
            //    insertTime = DateTime.Now,

            //    UserId = us.Id
            //};

            ////AddPost(us.Id, ps, dt);
            ////AddComment(ps1.Id, cm, dt);
            //AddLike(ps1.Id, us.Id, dt);

        }
    }
}
