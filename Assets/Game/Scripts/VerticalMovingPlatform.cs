using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    [Header("Movimiento vertical")]
    public float distance = 3f;
    public float speed = 1f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void LateUpdate()
    {

        float t = Mathf.PingPong(Time.time * speed, 1f);
        Vector3 pos = startPos;
        pos.y += t * distance;
        transform.position = pos;
    }
}

