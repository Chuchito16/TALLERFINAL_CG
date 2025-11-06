using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    [Header("Movimiento vertical")]
    public float distance = 3f;      // Distancia total en metros
    public float speed = 1f;         // Velocidad del movimiento

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void LateUpdate()
    {
        // PingPong crea movimiento de ida y vuelta
        float t = Mathf.PingPong(Time.time * speed, 1f);
        Vector3 pos = startPos;
        pos.y += t * distance;
        transform.position = pos;
    }
}
