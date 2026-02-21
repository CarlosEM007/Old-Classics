using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private float Velocidade = 8f;

    [Header("Controles")]
    [SerializeField] private KeyCode KeyCodeEsq = KeyCode.A;
    [SerializeField] private KeyCode KeyCodeDir = KeyCode.D;

    private Rigidbody2D rigid;
    private float eixoX;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        eixoX = 0f;

        if (Input.GetKey(KeyCodeDir)) eixoX += 1f;
        if (Input.GetKey(KeyCodeEsq)) eixoX -= 1f;
    }

    private void FixedUpdate()
    {
        Vector2 novaPosicao = rigid.position + Vector2.right * eixoX * Velocidade * Time.fixedDeltaTime;
        rigid.MovePosition(novaPosicao);
    }
}
