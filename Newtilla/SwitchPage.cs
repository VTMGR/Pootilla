using System.Collections;
using UnityEngine;

namespace Newtilla
{
    public class SwitchPage : GorillaPressableButton
    {
        public bool isNext;

        public override void ButtonActivation()
        {
            if (isNext)
            {
                Newtilla.currentPage++;
                if (Newtilla.currentPage > Newtilla.maxPage)
                {
                    Newtilla.currentPage--;
                    return;
                }
                Newtilla.UpdateDisplayedGameModes();
            }
            else
            {
                Newtilla.currentPage--;
                if (Newtilla.currentPage < 0)
                {
                    Newtilla.currentPage++;
                    return;
                }
                Newtilla.UpdateDisplayedGameModes();
            }
            StartCoroutine(ButtonColorUpdate());
        }

        private IEnumerator ButtonColorUpdate()
        {
            buttonRenderer.material = pressedMaterial;
            yield return new WaitForSeconds(0.25f);
            buttonRenderer.material = unpressedMaterial;
        }
    }
}