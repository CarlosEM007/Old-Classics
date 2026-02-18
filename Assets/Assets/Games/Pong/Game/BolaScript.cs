using Unity.Mathematics;
using UnityEngine;

public class BolaScript : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private float Velocidade;

    private Rigidbody2D rigid;

    public float EixoX;
    public float EixoY;

    public float DirecX;
    public float DirecY;

    int Toques = 0;

    private void Awake()
    {
        EixoX = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        EixoY = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;

        AlterarDirecaoBola();

        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 novaPosicao = rigid.position + (Vector2.right * (EixoX * DirecX) * Velocidade * Time.fixedDeltaTime) + (Vector2.up * (EixoY * DirecY) * Velocidade * Time.fixedDeltaTime);
        rigid.MovePosition(novaPosicao);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Raquete")
        {
            EixoX *= -1;

            Toques += 1;
            AumentarVelocidade();
            AlterarDirecaoBola();
        }

        if(collision.collider.tag == "Parede")
        {
            EixoY *= -1;
        }
    }

    private void AumentarVelocidade()
    {
        if(Toques % 3 == 0)
        {
            Velocidade += 1;
            Toques = 0;
        }
    }

    private void AlterarDirecaoBola()
    {
        DirecX = UnityEngine.Random.RandomRange(0.7f, 1f);
        DirecY = UnityEngine.Random.RandomRange(0.7f, 1f);
    }
}
