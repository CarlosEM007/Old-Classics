using UnityEngine;
using UnityEngine.Tilemaps;

public class BlocosScript : MonoBehaviour
{
    public Tilemap tilemap;

    void Start()
    {
        var tilemap = GetComponent<Tilemap>();
        var bounds = tilemap.cellBounds;

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                tilemap.SetColor(pos, Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f));
                tilemap.RefreshTile(pos);
            }
        }
    }
}
