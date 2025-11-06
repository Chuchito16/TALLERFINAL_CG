using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;               // Objeto a seguir (el jugador)
    public Vector3 offset = new Vector3(0, 5, -10); // Posicion relativa detras del jugador
    public float smoothTime = 0.15f;       // Tiempo de suavizado (en segundos aprox)

    private Vector3 currentVelocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;

        // Posicion deseada, relativa a la rotacion del jugador
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Suavizar el movimiento de la camara
        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref currentVelocity,
            smoothTime
        );
        transform.position = smoothedPosition;

        // Hacer que la camara mire al jugador
        transform.LookAt(target);
    }
}

