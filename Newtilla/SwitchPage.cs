namespace Newtilla
{
    public class SwitchPage : GorillaPressableButton
    {
        public bool isNext;

        public override void ButtonActivationWithHand(bool isLeftHand)
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
        }
    }
}
