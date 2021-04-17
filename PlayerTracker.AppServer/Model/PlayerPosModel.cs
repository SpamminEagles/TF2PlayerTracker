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
        public string PlayerId;

        /// <summary>
        /// Position of the player
        /// </summary>
        public (float x, float y, float z) Position;

        /// <summary>
        /// Angle the player is looking at
        /// </summary>
        public (float pitch, float yaw, float roll) Angle;

        /// <summary>
        /// Server time at the moment of capturing the data
        /// (milliseconds sice the unix epoch)
        /// </summary>
        public int Unixtime;

    }

}
