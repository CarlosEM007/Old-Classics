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
        [SerializeField] private Vector2 Eixos;
        [SerializeField] private Vector2 Direcoes;

        private Vector3 PosicaoInicial;

        private Rigidbody2D rigid;

        void Awake()
        {
            PosicaoInicial = gameObject.transform.position;

            rigid = GetComponent<Rigidbody2D>();

            Direcoes.x = Random.RandomRange(0.7f, 1f);
            Direcoes.y = Random.RandomRange(0.7f, 1f);

            Eixos.x = Random.Range(0, 2) == 0 ? -1 : 1;
            Eixos.y = 1;
        }

        void FixedUpdate()
        {
            Vector2 NovaDirecao = new Vector2(Eixos.x * Direcoes.x, Eixos.y * Direcoes.y);

            Vector2 novaPosicao = rigid.position + NovaDirecao * Velocidade * Time.fixedDeltaTime;
            rigid.MovePosition(novaPosicao);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DetectarColisaoTijolos(collision);
            DetectarColisaoParede(collision);
            DetectarColisaoRaquete(collision);
        }

        #region Detectar Colisões
        private void DetectarColisaoTijolos(Collision2D collision)
        {
            if (!collision.collider.CompareTag("Tijolos")) return;

            DetectarPosicaoRelacaoTile(collision, true);
        }

        private void DetectarColisaoParede(Collision2D collision)
        {
            if (collision.collider.CompareTag("Parede"))
            {
                Eixos.x *= -1;
            }

            if (collision.collider.CompareTag("Teto"))
            {
                Eixos.y *= -1;
            }

        }

        private void DetectarPosicaoRelacaoTile(Collision2D collision, bool Deletar)
        {
            Tilemap Tile = collision.collider.GetComponent<Tilemap>();

            if (Tile == null) return;

            ContactPoint2D contact = collision.GetContact(0);

            Vector3 CelulaDeColisao = contact.point - contact.normal * cellPickOffset;
            Vector3Int cell = Tile.WorldToCell(CelulaDeColisao);

            if (!Tile.HasTile(cell)) return;

            SideFromNormal(contact.normal);

            if (Deletar)
            {
                Tile.SetTile(cell, null);
                Controlador.AtualizarPontos();
            }
        }

        private void DetectarColisaoRaquete(Collision2D collision)
        {
            if (!collision.collider.CompareTag("Raquete")) return;

            Bounds raquete = collision.collider.bounds;

            float half = raquete.extents.x;

            float distanciaCentro = (transform.position.x - raquete.center.x) / half;
            distanciaCentro = Mathf.Clamp(distanciaCentro, -1f, 1);

            Direcoes.x = Mathf.Abs(distanciaCentro);
            Direcoes.y = Random.Range(0.7f, 1f);

            Eixos.x = distanciaCentro > 0 ? 1 : -1;
            Eixos.y = 1;
        }
        #endregion

        private void SideFromNormal(Vector2 n)
        {
            n = n.normalized;

            HitSide hit = new HitSide();

            if (Mathf.Abs(n.x) > Mathf.Abs(n.y))
                hit = (n.x > 0f) ? HitSide.Right : HitSide.Left;
            else
                hit = (n.y > 0f) ? HitSide.Top : HitSide.Bottom;

            AlterarDirecao(hit);
        }

        public void AlterarDirecao(HitSide LadoBloco)
        {
            switch (LadoBloco)
            {
                case HitSide.Left:
                    Eixos.x = -1;
                    break;
                case HitSide.Right:
                    Eixos.x = 1;
                    break;
                case HitSide.Top:
                    Eixos.y = 1;
                    break;
                case HitSide.Bottom:
                    Eixos.y = -1;
                    break;
            }
        }
    }
}
