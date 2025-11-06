using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right; 
    public float moveDistance = 2f;               
    public float moveSpeed = 2f;                  

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingToTarget = true;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveDirection.normalized * moveDistance;
    }

    void Update()
    {
        
        transform.position = Vector3.MoveTowards(
            transform.position,
            movingToTarget ? targetPos : startPos,
            moveSpeed * Time.deltaTime
        );

        
        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
            movingToTarget = false;
        else if (Vector3.Distance(transform.position, startPos) < 0.05f)
            movingToTarget = true;
    }
}
