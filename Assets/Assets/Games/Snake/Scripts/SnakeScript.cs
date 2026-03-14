using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeScript : MonoBehaviour
{
    [SerializeField] private Vector3 target;
    [SerializeField] private Vector3 saveDir;

    private Vector3 lastHeadPosition;

    [SerializeField] private Transform bodyPrefab;
    [SerializeField] private List<Transform> childList;

    [SerializeField] private Transform applePrefab;
    [SerializeField] private Transform appleInGame;

    [Header("Atributos")]
    [SerializeField] public static float Velocidade = 5;
    [SerializeField] public bool MorderCorpo = true;

    void Start()
    {
        appleInGame = SpawnApple();

        target = transform.position;
        saveDir = Vector3.up;

        lastHeadPosition = transform.position;
    }

    void Update()
    {
        MoveCobra();
    }

    private void MoveCobra()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        transform.position = Vector3.MoveTowards(transform.position, target, Velocidade * Time.deltaTime);

        if (dir.x != 0)
            saveDir = Vector3.right * dir.x;

        if (dir.y != 0)
            saveDir = Vector3.up * dir.y;

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            SetNewTarget();

            lastHeadPosition = transform.position;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);

            Vector3 spawnPos = lastHeadPosition;

            if (childList.Count > 0)
                spawnPos = childList[childList.Count - 1].position;

            Transform obj = Instantiate(bodyPrefab, spawnPos, Quaternion.identity);

            obj.GetComponent<SnakeBodyScript>().WaitHead(childList.Count + 1);

            childList.Add(obj);

            appleInGame = SpawnApple();

            Velocidade += 0.1f;

            MorderCorpo = false;
            StartCoroutine(EnableBite());
        }

        if (collision.CompareTag("Corpo") && MorderCorpo)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator EnableBite()
    {
        yield return new WaitForSeconds(0.2f);
        MorderCorpo = true;
    }

    private Transform SpawnApple()
    {
        return Instantiate(
            applePrefab,
            new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0),
            Quaternion.identity
        );
    }

    private void SetNewTarget()
    {
        if (childList.Count == 0) return;

        childList[0].GetComponent<SnakeBodyScript>().SetTarget(lastHeadPosition);

        for (int i = 1; i < childList.Count; i++)
        {
            childList[i].GetComponent<SnakeBodyScript>().SetTarget(childList[i - 1].position);
        }
    }
}