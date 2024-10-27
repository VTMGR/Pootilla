using System;

namespace Utilla
{
    public class Events
    {
        public static event EventHandler<RoomJoinedArgs> RoomJoined;

        public static event EventHandler<RoomJoinedArgs> RoomLeft;

        public static event EventHandler GameInitialized;
        
        public static void TriggerGameInitialized()
        {
            GameInitialized(null, EventArgs.Empty);
        }

        public static void TriggerRoomJoin(RoomJoinedArgs e)
        {
            RoomJoined(null, e);
        }

        public static void TriggerRoomLeft(RoomJoinedArgs e)
        {
            RoomLeft(null, e);
        }

        public class RoomJoinedArgs : EventArgs
        {
            /// <summary>
            /// Whether or not the room is private.
            /// </summary>
            public bool isPrivate { get; set; }

            /// <summary>
            /// The gamemode that the current lobby is 
            /// </summary>
            public string Gamemode { get; set; }
        }
    }
}
