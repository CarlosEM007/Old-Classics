using UnityEngine;

public class SnakeBodyScript : MonoBehaviour
{
    private Vector3 nextBodyPos;
    private int waitUps;

    void Start()
    {
        nextBodyPos = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            nextBodyPos,
            SnakeScript.Velocidade * Time.deltaTime
        );
    }

    public void SetTarget(Vector3 pos)
    {
        if (waitUps > 0)
        {
            waitUps--;
            return;
        }

        nextBodyPos = pos;
    }

    public void WaitHead(int value)
    {
        waitUps = value;
    }
}