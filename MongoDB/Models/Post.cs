using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MongoDB.Models
{
    public class Post
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Text { get; set; }
        public DateTime insertTime { get; set; }
        public ObjectId UserId {  get; set; }
        public List<ObjectId> CommentsId { get; set; }
        public List<ObjectId> UserLikesId { get; set; }
    }
}
