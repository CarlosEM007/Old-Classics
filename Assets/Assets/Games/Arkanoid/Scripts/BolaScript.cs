using Assets.Assets.Games.Arkanoid.Enum;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace OldClassics.Games.Arkanoid
{
    public class BolaScript : MonoBehaviour
    {

        [Header("Objetos")]
        [SerializeField] private ControladorScript Controlador;

        [Header("Atributos")]
        [SerializeField] private float cellPickOffset = 0.01f;
        [SerializeField] private float Velocidade;
        [SerializeField] private float EixoX;
        [SerializeField] private float EixoY;
        [SerializeField] private float DirecX;
        [SerializeField] private float DirecY;

        private Vector3 PosicaoInicial;

        private Rigidbody2D rigid;

        void Awake()
        {
            PosicaoInicial = gameObject.transform.position;

            rigid = GetComponent<Rigidbody2D>();

            AlterarDirecaoBola();
        }

        void Update()
        {
            EixoX = Controlador.X;
            EixoY = Controlador.Y;
        }

        void FixedUpdate()
        {
            Vector2 novaPosicao = rigid.position + (Vector2.right * (EixoX * DirecX) * Velocidade * Time.fixedDeltaTime) + (Vector2.up * (EixoY * DirecY) * Velocidade * Time.fixedDeltaTime);
            rigid.MovePosition(novaPosicao);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DetectarColisaoTijolos(collision);

            if (collision.collider.tag == "Raquete")
            {
                Controlador.Y = 1;

                AlterarDirecaoBola();
            }

            if (collision.collider.tag == "Parede")
            {
                Controlador.X *= -1;
            }
        }

        private void DetectarColisaoTijolos(Collision2D collision)
        {
            if (!collision.collider.CompareTag("Tijolos")) return;

            Tilemap Tile = collision.collider.GetComponent<Tilemap>();

            if (Tile == null) return;

            ContactPoint2D contact = collision.GetContact(0);

            Vector3 CelulaDeColisao = contact.point - contact.normal * cellPickOffset;
            Vector3Int cell = Tile.WorldToCell(CelulaDeColisao);

            if (!Tile.HasTile(cell)) return;

            SideFromNormal(contact.normal);

            Tile.SetTile(cell, null);
        }

        private void SideFromNormal(Vector2 n)
        {
            n = n.normalized;

            HitSide hit = new HitSide();

            if (Mathf.Abs(n.x) > Mathf.Abs(n.y))
                hit = (n.x > 0f) ? HitSide.Right : HitSide.Left;
            else
                hit = (n.y > 0f) ? HitSide.Top : HitSide.Bottom;

            Controlador.AlterarDirecao(hit);
        }

        private void AlterarDirecaoBola()
        {
            DirecX = UnityEngine.Random.RandomRange(0.7f, 1f);
            DirecY = UnityEngine.Random.RandomRange(0.7f, 1f);
        }
    }
}
