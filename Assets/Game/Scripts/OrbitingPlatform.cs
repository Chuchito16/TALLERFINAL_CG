using UnityEngine;

public class OrbitingPlatform : MonoBehaviour
{
    [Header("Centro de la orbita")]
    public Transform center;          // Punto alrededor del cual gira

    [Header("Movimiento")]
    public Vector3 axis = Vector3.up; // Eje de giro (up = como planeta)
    public float angularSpeed = 30f;  // grados por segundo

    private void Reset()
    {
        if (transform.parent != null)
            center = transform.parent;
    }

    private void Update()
    {
        if (center == null) return;

        // Gira alrededor del centro manteniendo el radio
        transform.RotateAround(
            center.position,
            axis.normalized,
            angularSpeed * Time.deltaTime
        );
    }
}




