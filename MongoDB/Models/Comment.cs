using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.Models
{
    public class Comment
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Text { get; set; }
        public DateTime insertTime { get; set; }
        public ObjectId UserId {  get; set;}
    }
}
