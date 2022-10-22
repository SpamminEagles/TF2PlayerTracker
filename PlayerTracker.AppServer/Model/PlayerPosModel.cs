using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PlayerTracker.AppServer.Model
{
    /// <summary>
    /// Player position model class
    /// </summary>
    [Serializable]
    public class PlayerPosModel
    {
        public const string COLLECTION = "playerpos";

        /// <summary>
        /// Id of the player (steamid)
        /// </summary>
        [BsonElement(nameof(PlayerId))]
        public string PlayerId { get; set; }

        /// <summary>
        /// Self determined serverid
        /// </summary>
        [BsonElement(nameof(ServerId))]
        public string ServerId { get; set; }

        /// <summary>
        /// Position of the player
        /// </summary>
        [BsonElement(nameof(Position))]
        public float[] Position { get; set; }

        /// <summary>
        /// Angle the player is looking at
        /// </summary>
        [BsonElement(nameof(Angle))]
        public float[] Angle { get; set; }

        /// <summary>
        /// Server time at the moment of capturing the data
        /// (milliseconds sice the unix epoch)
        /// </summary>
        [BsonElement(nameof(Unixtime))]
        public long? Unixtime { get; set; }

        public bool Validate()
        {
            return PlayerId != null && Unixtime != null 
                && Position != null && Position.Length == 3
                && Angle != null && Angle.Length == 3;
        }

    }

}
