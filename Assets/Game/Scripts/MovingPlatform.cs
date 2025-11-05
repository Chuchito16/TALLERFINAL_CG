using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 2f;   // amplitud vertical
    public float moveSpeed = 1f;      // velocidad del movimiento

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // Movimiento vertical tipo seno: sube y baja
        float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}

