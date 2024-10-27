using GorillaNetworking;
using Photon.Pun;

namespace Newtilla
{
    internal class RoomManagement : MonoBehaviourPunCallbacks
    {
        public override void OnJoinedRoom()
        {
            try
            {
                string GameMode = ((string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"]).Replace(PhotonNetworkController.Instance.currentJoinTrigger.networkZone, "").Replace(GorillaComputer.instance.currentQueue, "");
                Newtilla.currentMode = GameMode;
                if (GameMode.Substring(0, 7) == "MODDED_")
                {
                    Newtilla.TriggerOnJoin(GameMode);
                }
                if (Newtilla.JoinActions.ContainsKey(GameMode))
                    Newtilla.JoinActions[GameMode]();
                Utilla.Events.TriggerRoomJoin(new Utilla.Events.RoomJoinedArgs { Gamemode = GameMode, isPrivate = !PhotonNetwork.CurrentRoom.IsVisible });
            }
            //Do nothing because if the substring fails we don't need to do anything anyway lol
            catch
            {
            }
        }
        public override void OnLeftRoom()
        {
            try
            {
                string GameMode = Newtilla.currentMode;
                if (GameMode.Substring(0, 7) == "MODDED_")
                {
                    Newtilla.TriggerOnLeave(GameMode);
                }
                if (Newtilla.LeaveActions.ContainsKey(GameMode))
                    Newtilla.LeaveActions[GameMode]();
                Newtilla.currentMode = string.Empty;
                Utilla.Events.TriggerRoomLeft(new Utilla.Events.RoomJoinedArgs { Gamemode = GameMode, isPrivate = false });
            }
            //Do nothing because if the substring fails we don't need to do anything anyway lol
            catch
            {
            }
        }
    }
}
