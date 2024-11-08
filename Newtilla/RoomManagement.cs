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
                if (Newtilla.currentMode.Substring(0, 7) == "MODDED_")
                {
                    Newtilla.TriggerOnLeave(Newtilla.currentMode);
                }
                if (Newtilla.LeaveActions.ContainsKey(Newtilla.currentMode))
                    Newtilla.LeaveActions[Newtilla.currentMode]();
                Newtilla.currentMode = string.Empty;
            }
            //Do nothing because if the substring fails we don't need to do anything anyway lol
            catch
            {
            }
        }
    }
}
