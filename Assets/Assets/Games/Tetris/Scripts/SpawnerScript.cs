using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] Tetrominoes;

    void Start()
    {
        NewTetromino();

    }

    public void NewTetromino()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
    }
}
