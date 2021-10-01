using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<string> Interests { get; set; }

        public List<ObjectId> FollowersId { get; set; }

        public List<ObjectId> FollowingsId { get; set; }

        public List<ObjectId> PostsId { get; set; }

    }
}
