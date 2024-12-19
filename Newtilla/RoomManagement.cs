using BepInEx.Logging;
using GorillaGameModes;
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
                PhotonNetwork.CurrentRoom.CustomProperties["gameMode"] = "MODDED_" + (string)PhotonNetwork.CurrentRoom.CustomProperties["gameMode"];
                string GameMode = "MODDED_CASUAL";
                Newtilla.currentMode = "MODDED_CASUAL";
                Newtilla.TriggerOnJoin("MODDED_CASUAL");
                Logger.CreateLogSource("onjoin").LogWarning("OnJoin Gamemode: "+ "MODDED_CASUAL");
                
                if (Newtilla.JoinActions.ContainsKey("MODDED_CASUAL"))
                    Newtilla.JoinActions["MODDED_CASUAL"]();
            }
            catch
            {
            }
        }
        public override void OnLeftRoom()
        {
            try
            {
                Logger.CreateLogSource("onleave").LogWarning("On Leave Gamemode: " + Newtilla.currentMode);
                if (Newtilla.currentMode.Substring(0, 7) == "MODDED_")
                {
                    Newtilla.TriggerOnLeave(Newtilla.currentMode);
                }
                if (Newtilla.LeaveActions.ContainsKey(Newtilla.currentMode))
                    Newtilla.LeaveActions[Newtilla.currentMode]();
                Newtilla.currentMode = string.Empty;
            }
            catch
            {
            }
        }
    }
}
