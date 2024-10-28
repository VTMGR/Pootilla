using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace Newtilla.Patches
{
    [HarmonyPatch(typeof(GameModeSelectorButtonLayout), "Start")]
    public class GamemodeLayoutPatch
    {
        static void Prefix(GameModeSelectorButtonLayout __instance)
        {
            foreach (Transform tr in __instance.transform)
            {
                Object.Destroy(tr.gameObject);
            }

            if (!__instance.transform.parent.Find("NextPage"))
            {
                ModeSelectButton modeSelectButton = Object.Instantiate(__instance.pf_button, __instance.transform.parent);
                modeSelectButton.transform.localPosition = new Vector3(-0.608f, 0.276f, 0.179f);
                modeSelectButton.transform.localRotation = Quaternion.Euler(0f, 0f, 270f);
                modeSelectButton.SetInfo(Newtilla.PageButtonInfo);
                Transform Page = modeSelectButton.transform;
                Page.gameObject.name = "NextPage";
                SwitchPage PageButton = Page.AddComponent<SwitchPage>();
                PageButton.isNext = true;
                PageButton.buttonRenderer = modeSelectButton.buttonRenderer;
                PageButton.pressedMaterial = modeSelectButton.pressedMaterial;
                PageButton.unpressedMaterial = modeSelectButton.unpressedMaterial;
                Object.Destroy(modeSelectButton);
            }

            if (!__instance.transform.parent.Find("PrevPage"))
            {
                ModeSelectButton modeSelectButton = Object.Instantiate(__instance.pf_button, __instance.transform.parent);
                modeSelectButton.transform.localPosition = new Vector3(-0.608f, -0.374f, 0.179f);
                modeSelectButton.transform.localRotation = Quaternion.Euler(0f, 0f, 270f);
                modeSelectButton.SetInfo(Newtilla.PageButtonInfo);
                Transform Page = modeSelectButton.transform;
                Page.gameObject.name = "PrevPage";
                SwitchPage PageButton = Page.AddComponent<SwitchPage>();
                PageButton.isNext = false;
                PageButton.buttonRenderer = modeSelectButton.buttonRenderer;
                PageButton.pressedMaterial = modeSelectButton.pressedMaterial;
                PageButton.unpressedMaterial = modeSelectButton.unpressedMaterial;
                Object.Destroy(modeSelectButton);
            }
        }

        //Shouldn't have had to make this patch but network triggers are scuffed.
        [HarmonyPrefix]
        [HarmonyPatch(typeof(GorillaNetworkJoinTrigger), "OnBoxTriggered")]
        static bool JoinTriggerPatch(GorillaNetworkJoinTrigger __instance)
        {
            return !PhotonNetwork.InRoom || __instance.GetActiveNetworkZone() != __instance.networkZone;
        }
    }
}
