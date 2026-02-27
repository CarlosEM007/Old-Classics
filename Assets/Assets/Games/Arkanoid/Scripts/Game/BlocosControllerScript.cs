using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlocosControllerScript : MonoBehaviour
{
    public Tilemap tilemap;

    void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;

        Color[] Cores = SelecionadorDeCores();

        AleatorizadorDeCoresNosBlocos(Cores, bounds);
    }

    private Color[] SelecionadorDeCores()
    {
        Color[] Cores = new Color[5];

        for (int i = 0; i < Cores.Length; ++i)
        {
            Cores[i] = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
        }

        return Cores;
    }

    private void AleatorizadorDeCoresNosBlocos(Color[] Cores, BoundsInt Bounds)
    {
        foreach (Vector3Int pos in Bounds.allPositionsWithin)
        {
            int IndexCor = Random.Range(0, Cores.Length - 1);

            if (tilemap.HasTile(pos))
            {
                tilemap.SetColor(pos, Cores[IndexCor]);
                tilemap.RefreshTile(pos);
            }
        }
    }
}
