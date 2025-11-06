using UnityEngine;

public class OrbitingPlatform : MonoBehaviour
{
    [Header("Centro de la orbita")]
    public Transform center;       

    [Header("Movimiento")]
    public Vector3 axis = Vector3.up; 
    public float angularSpeed = 30f;  

    private void Reset()
    {
        if (transform.parent != null)
            center = transform.parent;
    }

    private void Update()
    {
        if (center == null) return;


        transform.RotateAround(
            center.position,
            axis.normalized,
            angularSpeed * Time.deltaTime
        );
    }
}




