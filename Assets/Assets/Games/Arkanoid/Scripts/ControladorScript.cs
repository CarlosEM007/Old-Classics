using Assets.Assets.Games.Arkanoid.Enum;
using UnityEngine;

public class ControladorScript : MonoBehaviour
{
    public float X;
    public float Y;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlterarDirecao(HitSide LadoBloco)
    {
        switch (LadoBloco)
        {
            case HitSide.Left:
                X *= -1;
                break;
            case HitSide.Right:
                X *= 1;
                break;
            case HitSide.Top:
                Y *= 1;
                break;
            case HitSide.Bottom:
                Y *= -1;
                break;
        }
    }
}
