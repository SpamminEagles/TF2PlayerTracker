using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PlayerTracker.AppServer.Model
{
    [Serializable]
    public class AuthorizationInformationModel
    {

        public BsonObjectId _id { get; set; }

        [BsonElement(nameof(Name))]
        public string Name { get; set; }

        [BsonElement(nameof(Token))]
        public string Token { get; set; }
        
        [BsonElement(nameof(Grants))]
        public string[] Grants { get; set; }

    }
}
