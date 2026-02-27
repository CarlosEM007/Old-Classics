using Assets.Assets.Shared;
using Unity.VisualScripting;

namespace Assets.Assets.Games.Arkanoid.Scripts.Menu
{
    public class MenuArkanoidScript: GamesMenu
    {
        protected override void AcionarCampo()
        {
            if(Indice == 0)
            {
                AlterarCena("", IDScene);
            }
            else
            {
                AlterarCena("", 0);
            }
        }
    }
}
