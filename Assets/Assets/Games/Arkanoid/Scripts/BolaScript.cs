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
        [SerializeField] private float EixoX;
        [SerializeField] private float EixoY;

        void Start()
        {

        }

        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DetectarColisaoTijolos(collision);
        }

        private void DetectarColisaoTijolos(Collision2D collision)
        {
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
    }
}
