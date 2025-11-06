using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Configuracion de seguimiento")]
    public Transform target;               // Objeto a seguir (el jugador)
    public Vector3 offset = new Vector3(0, 5, -10); // Posicion relativa
    [Range(0.01f, 1f)]
    public float smoothTime = 0.15f;       // Tiempo de suavizado

    private void LateUpdate()
    {
        if (target == null) return;

        // Posicion deseada
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Suavizado con Lerp (más estable que SmoothDamp)
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            Time.deltaTime / smoothTime
        );

        // Hacer que mire al jugador
        transform.LookAt(target);
    }
}
