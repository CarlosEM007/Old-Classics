using TMPro;
using UnityEngine;
using OldClassics.Games.Pong;

public class ControllerScript : MonoBehaviour
{
    [Header("Bola")]
    [SerializeField] private GameObject Bola;
    [SerializeField] private BolaScript BolaScript;

    [Header("Pontos")]
    [SerializeField] private TextMeshProUGUI PontosPlayer1;
    [SerializeField] private TextMeshProUGUI PontosPlayer2;

    [Header("Posições")]
    [SerializeField] private float PosicaoXPlayer1;
    [SerializeField] private float PosicaoXPlayer2;

    int Pontos1;
    int Pontos2;

    void Start()
    {
        Pontos1 = 0;
        Pontos1 = 0;

        AtualizarPontos();
    }

    void Update()
    {
        DetectarPosicaoBola();
    }

    private void DetectarPosicaoBola()
    {
        float posicaoBola = Bola.transform.localPosition.x;

        if(posicaoBola <= PosicaoXPlayer1)
        {
            AdicionarPontos(ref Pontos1);
        }

        if(posicaoBola >= PosicaoXPlayer2)
        {
            AdicionarPontos(ref Pontos2);
        }
    }

    private void AdicionarPontos(ref int Pontos)
    {
        Pontos += 1;
        AtualizarPontos();

        BolaScript.Recomecar();
    }

    private void AtualizarPontos()
    {
        PontosPlayer1.text = Pontos2.ToString();
        PontosPlayer2.text = Pontos1.ToString();
    }
}
