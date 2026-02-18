using UnityEngine;

public class RaquetePlayerScript : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private float Velocidade = 8f;

    [Header("Controles")]
    [SerializeField] private KeyCode KeyCodeCima = KeyCode.W;
    [SerializeField] private KeyCode KeyCodeBaixo = KeyCode.S;

    private Rigidbody2D rigid;
    private float eixoY;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        eixoY = 0f;

        if (Input.GetKey(KeyCodeCima)) eixoY += 1f;
        if (Input.GetKey(KeyCodeBaixo)) eixoY -= 1f;

    }

    void FixedUpdate()
    {
        Vector2 novaPosicao = rigid.position + Vector2.up * eixoY * Velocidade * Time.fixedDeltaTime;
        rigid.MovePosition(novaPosicao);
    }
}
