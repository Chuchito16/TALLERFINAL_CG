using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Configuracion de seguimiento")]
    public Transform target; 
    public Vector3 offset = new Vector3(0, 5, -10); 
    [Range(0.01f, 1f)]
    public float smoothTime = 0.15f;

    private void LateUpdate()
    {
        if (target == null) return;


        Vector3 desiredPosition = target.position + target.TransformDirection(offset);


        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            Time.deltaTime / smoothTime
        );


        transform.LookAt(target);
    }
}
