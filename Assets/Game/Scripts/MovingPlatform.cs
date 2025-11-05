using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 2f;   // amplitud del movimiento
    public float moveSpeed = 1f;      // velocidad del movimiento
    public bool vertical = true;      // true = eje Y, false = eje X

    private Vector3 startPos;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;      // muy importante
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
            newPos.x += offset; // o newPos.z si prefieres
        }

        rb.MovePosition(newPos);
    }
}
