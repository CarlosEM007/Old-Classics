using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace OldClassics.Games.Pong
{
    public class BolaScript : MonoBehaviour
    {
        [Header("Atributos")]
        [SerializeField] private float Velocidade;

        [Header("HUD")]
        [SerializeField] private GameObject ComecarText;

        private Rigidbody2D rigid;

        public float EixoX;
        public float EixoY;

        public float DirecX;
        public float DirecY;

        int Toques = 0;
        bool PodeAndar = false;

        Vector2 PosicaoInicial;

        private void Awake()
        {
            PosicaoInicial = gameObject.transform.position;

            ComecarText.SetActive(true);

            EixoX = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
            EixoY = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;

            AlterarDirecaoBola();

            rigid = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Comecar();
        }

        void FixedUpdate()
        {
            if (!PodeAndar) return;

            Vector2 novaPosicao = rigid.position + (Vector2.right * (EixoX * DirecX) * Velocidade * Time.fixedDeltaTime) + (Vector2.up * (EixoY * DirecY) * Velocidade * Time.fixedDeltaTime);
            rigid.MovePosition(novaPosicao);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Raquete")
            {
                EixoX *= -1;

                Toques += 1;
                AumentarVelocidade();
                AlterarDirecaoBola();
            }

            if (collision.collider.tag == "Parede")
            {
                EixoY *= -1;
            }
        }

        private void Comecar()
        {
            if (Input.GetKeyDown(KeyCode.Return) && !PodeAndar)
            {
                ComecarText.SetActive(false);

                PodeAndar = true;
            }
        }

        private void AumentarVelocidade()
        {
            if (Toques % 3 == 0)
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

        public void Recomecar()
        {
            PodeAndar = false;
            gameObject.transform.position = PosicaoInicial;

            ComecarText.SetActive(true);
        }
    }

}

