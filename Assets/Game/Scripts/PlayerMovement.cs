using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f; // grados/seg

    [Header("Physics")]
    [SerializeField] private float gravity = -9.81f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 2f; // altura del salto en metros aprox

    [Header("Optional")]
    [Tooltip("Si se asigna, el movimiento sera relativo a esta camara (ej: camara orbital).")]
    public Camera mouseOrbitCamera;

    // Plataformas en movimiento
    [Header("Moving Platforms")]
    [SerializeField] private string movingPlatformTag = "MovingPlatform";
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;

    private CharacterController controller;
    private Animator anim;

    // Nuevo Input System: valor actual de la accion "Move"
    private Vector2 moveInput;           // x: izq-der, y: adelante-atras
    private Vector3 velocity;            // para gravedad y salto

    // Flag para pedir salto desde el callback y procesarlo en Update
    private bool jumpRequest = false;

    // Hash para parametros del Animator (evita typos y es mas rapido)
    private static readonly int VelX = Animator.StringToHash("velX");
    private static readonly int VelY = Animator.StringToHash("velY");

    // Suavizado para el Blend Tree
    [SerializeField] private float animDamp = 0.05f;
    private float velXCur, velYCur;      // internos para damping

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // ====== NUEVO INPUT SYSTEM ======
    // Evento de movimiento
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>(); // (-1..1 , -1..1)
    }

    // Evento de salto
    public void OnJump(InputAction.CallbackContext ctx)
    {
        // Solo marcamos la intencion de saltar cuando la accion se "performea"
        if (ctx.performed)
        {
            jumpRequest = true;
        }
    }

    private void Update()
    {
        // Si no esta en el suelo, olvida la plataforma actual
        if (!controller.isGrounded)
        {
            currentPlatform = null;
        }

        // 1) Calcular direccion de movimiento (relativa a camara si existe)
        Vector3 input = new Vector3(moveInput.x, 0f, moveInput.y); // x=strafe, z=forward

        Vector3 moveWorld;
        if (mouseOrbitCamera != null && mouseOrbitCamera.gameObject.activeInHierarchy)
        {
            // plano XZ de la camara
            Vector3 camFwd = mouseOrbitCamera.transform.forward;
            camFwd.y = 0f;
            camFwd.Normalize();

            Vector3 camRight = mouseOrbitCamera.transform.right;
            camRight.y = 0f;
            camRight.Normalize();

            // aqui usamos input.x e input.z
            moveWorld = camRight * input.x + camFwd * input.z;
        }
        else
        {
            // sin camara: usar el sistema local del personaje
            moveWorld = transform.right * input.x + transform.forward * input.z;
        }

        // 2) Rotar hacia la direccion de avance si hay input
        Vector3 lookDir = new Vector3(moveWorld.x, 0f, moveWorld.z);
        if (lookDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );
        }

        // 3) Movimiento horizontal
        Vector3 horizontal = moveWorld * moveSpeed;

        // 4) Manejo de suelo y salto
        if (controller.isGrounded)
        {
            if (velocity.y < 0f)
            {
                // Pequeno empuje hacia abajo para mantener grounded
                velocity.y = -2f;
            }

            if (jumpRequest)
            {
                // Formula clasica de salto:
                // v = sqrt(altura * -2 * gravedad)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpRequest = false;

                // al saltar, ya no seguimos la plataforma
                currentPlatform = null;
            }
        }

        // 5) Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;

        // 6) Mover personaje (horizontal + vertical)
        Vector3 finalMove = horizontal;
        finalMove.y = velocity.y;
        controller.Move(finalMove * Time.deltaTime);

        // 6b) Aplicar movimiento extra de la plataforma si estamos sobre una
        HandlePlatformMovement();

        // 7) Parametros del Animator (Blend Tree 2D Freeform: velX, velY)
        velXCur = Mathf.SmoothDamp(velXCur, moveInput.x, ref velXCur, animDamp);
        velYCur = Mathf.SmoothDamp(velYCur, moveInput.y, ref velYCur, animDamp);
        anim.SetFloat(VelX, velXCur);
        anim.SetFloat(VelY, velYCur);
    }

    private void HandlePlatformMovement()
    {
        if (currentPlatform == null) return;

        Vector3 platformDelta = currentPlatform.position - lastPlatformPosition;
        if (platformDelta.sqrMagnitude > 0f)
        {
            // Este Move extra hace que el player se mueva con la plataforma
            controller.Move(platformDelta);
        }

        lastPlatformPosition = currentPlatform.position;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // 1) Detectar si pisamos una plataforma en movimiento
        if (hit.collider.CompareTag(movingPlatformTag))
        {
            if (currentPlatform != hit.collider.transform)
            {
                currentPlatform = hit.collider.transform;
                lastPlatformPosition = currentPlatform.position;
            }
        }

        // 2) Detectar coleccionables (verde/rojo)
        CollectableItem collectible = hit.collider.GetComponent<CollectableItem>();
        if (collectible != null)
        {
            collectible.Collect();
        }
    }
}
