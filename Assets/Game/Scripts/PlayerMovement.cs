using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f;

    [Header("Physics")]
    [SerializeField] private float gravity = -9.81f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 2f;

    [Header("Optional")]
    public Camera mouseOrbitCamera;

    [Header("Moving Platforms")]
    [SerializeField] private string movingPlatformTag = "MovingPlatform";
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;
    private bool onMovingPlatform = false;

    [Header("Checkpoints")]
    [SerializeField] private Transform defaultSpawnPoint;
    [SerializeField] private string checkpointTag = "Checkpoint";
    [SerializeField] private string deathZoneTag = "DeathZone";
    private Vector3 lastCheckpointPosition;

    private CharacterController controller;
    private Animator anim;

    private Vector2 moveInput;
    private Vector3 velocity;
    private bool jumpRequest = false;

    private static readonly int VelX = Animator.StringToHash("velX");
    private static readonly int VelY = Animator.StringToHash("velY");
    private static readonly int JumpTrig = Animator.StringToHash("Jump");

    [SerializeField] private float animDamp = 0.05f;
    private float velXCur, velYCur;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        if (defaultSpawnPoint != null)
            lastCheckpointPosition = defaultSpawnPoint.position;
        else
            lastCheckpointPosition = transform.position;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            jumpRequest = true;
    }

    private void Update()
    {
        Vector3 platformDelta = Vector3.zero;
        onMovingPlatform = currentPlatform != null;

        if (onMovingPlatform)
        {
            platformDelta = currentPlatform.position - lastPlatformPosition;
            lastPlatformPosition = currentPlatform.position;
        }

        if (!controller.isGrounded)
            currentPlatform = null;

        // Dirección de movimiento
        Vector3 input = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 moveWorld;

        if (mouseOrbitCamera != null && mouseOrbitCamera.gameObject.activeInHierarchy)
        {
            Vector3 camFwd = mouseOrbitCamera.transform.forward;
            camFwd.y = 0f;
            camFwd.Normalize();
            Vector3 camRight = mouseOrbitCamera.transform.right;
            camRight.y = 0f;
            camRight.Normalize();
            moveWorld = camRight * input.x + camFwd * input.z;
        }
        else
        {
            moveWorld = transform.right * input.x + transform.forward * input.z;
        }

        // Rotación
        Vector3 lookDir = new Vector3(moveWorld.x, 0f, moveWorld.z);
        if (lookDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        Vector3 horizontal = moveWorld * moveSpeed;

        // Ground + salto
        if (controller.isGrounded)
        {
            if (velocity.y < 0f)
                velocity.y = -2f;

            if (jumpRequest)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpRequest = false;
                currentPlatform = null;
                if (anim != null)
                {
                    anim.ResetTrigger(JumpTrig);
                    anim.SetTrigger(JumpTrig);
                }
            }
        }
        else
        {
            // Mientras estamos en el aire, no aplicamos la plataforma
            onMovingPlatform = false;
        }

        // Gravedad
        velocity.y += gravity * Time.deltaTime;

        // Cuando estamos sobre una plataforma, anulamos micro-correcciones
        if (onMovingPlatform && controller.isGrounded && velocity.y < 0)
            velocity.y = 0f;

        Vector3 totalMove = (horizontal + velocity) * Time.deltaTime + platformDelta;
        controller.Move(totalMove);

        velXCur = Mathf.SmoothDamp(velXCur, moveInput.x, ref velXCur, animDamp);
        velYCur = Mathf.SmoothDamp(velYCur, moveInput.y, ref velYCur, animDamp);
        anim.SetFloat(VelX, velXCur);
        anim.SetFloat(VelY, velYCur);
    }

    private void RespawnAtCheckpoint()
    {
        controller.enabled = false;
        transform.position = lastCheckpointPosition + Vector3.up * 0.5f;
        velocity = Vector3.zero;
        currentPlatform = null;
        controller.enabled = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag(movingPlatformTag))
        {
            if (currentPlatform != hit.collider.transform)
            {
                currentPlatform = hit.collider.transform;
                lastPlatformPosition = currentPlatform.position;
            }
        }

        if (hit.collider.CompareTag(checkpointTag))
        {
            lastCheckpointPosition = hit.collider.transform.position;
        }

        if (hit.collider.CompareTag(deathZoneTag))
        {
            GameManager.Instance?.RegisterFall();
            RespawnAtCheckpoint();
        }

        CollectableItem collectible = hit.collider.GetComponent<CollectableItem>();
        if (collectible != null)
        {
            collectible.Collect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(checkpointTag))
            lastCheckpointPosition = other.transform.position;

        if (other.CompareTag(deathZoneTag))
        {
            GameManager.Instance?.RegisterFall();
            RespawnAtCheckpoint();
        }
    }
}
