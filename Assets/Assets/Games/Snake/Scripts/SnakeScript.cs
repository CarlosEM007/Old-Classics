using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeScript : MonoBehaviour
{
    [SerializeField] private Vector3 target;
    [SerializeField] private Vector3 saveDir;

    [SerializeField] private Transform bodyPrefab;
    [SerializeField] private List<Transform> childList;

    [SerializeField] private Transform applePrefab;
    [SerializeField] private Transform appleInGame;

    [Header("Atributos")]
    [SerializeField] private float Velocidade;

    void Start()
    {
        appleInGame = SpawnApple();

        target = transform.position;
        saveDir = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCobra();
        PositionCheck();
    }

    private void MoveCobra()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.position = Vector3.MoveTowards(transform.position, target, Velocidade * Time.deltaTime);

        if (dir.x != 0)
        {
            saveDir = Vector3.right * dir.x;
        }

        if (dir.y != 0)
        {
            saveDir = Vector3.up * dir.y;
        }

        if (transform.position == target)
        {
            target += saveDir;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Parede"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void PositionCheck()
    {
        if(appleInGame != null && transform.position == appleInGame.position)
        {
            Destroy(appleInGame);

            Transform obj = Instantiate(bodyPrefab, transform.position, Quaternion.identity);
            childList.Add(obj);

            appleInGame = SpawnApple();
        }
    }

    private Transform SpawnApple()
    {
        return Instantiate(applePrefab, new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0), Quaternion.identity);
    }
}
