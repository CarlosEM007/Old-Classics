using Assets.Assets.Games.Arkanoid.Enum;
using UnityEngine;

public class ControladorScript : MonoBehaviour
{
    public float X;
    public float Y;

    void Awake()
    {
        DirecaoInicial();
    }

    public void AlterarDirecao(HitSide LadoBloco)
    {
        switch (LadoBloco)
        {
            case HitSide.Left:
                X = -1;
                break;
            case HitSide.Right:
                X = 1;
                break;
            case HitSide.Top:
                Y = 1;
                break;
            case HitSide.Bottom:
                Y = -1;
                break;
        }
    }

    private void DirecaoInicial()
    {
        X = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        Y = 1;
    }
}
