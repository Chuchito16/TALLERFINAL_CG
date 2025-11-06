using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 2f;
    public float moveSpeed = 1f; 
    public bool vertical = true;

    private Vector3 startPos;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        Vector3 newPos = startPos;

        if (vertical)
        {
            newPos.y += offset;
        }
        else
        {
            newPos.x += offset;
        }

        rb.MovePosition(newPos);
    }
}
