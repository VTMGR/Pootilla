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
        }
    }
}
