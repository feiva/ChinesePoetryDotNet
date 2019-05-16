using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ChinesePoetry.Database.Models
{
    public class MingJu
    {
        [BsonId]
        [IgnoreMap]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public long OId { get; set; }
        public string OIdNew { get; set; }
        public string Content { get; set; }
        public long OShiId { get; set; }
        public string OShiIdNew { get; set; }
        public string Author { get; set; }
        public string SourceName { get; set; }
    }
}
