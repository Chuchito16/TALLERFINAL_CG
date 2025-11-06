using UnityEngine;
using UnityEngine.InputSystem;

public class CollectableItem : MonoBehaviour
{
    public enum ItemType
    {
        CapsulaVerde,   // suma puntos
        CapsulaRoja     // resta puntos
    }

    public ItemType itemType;
    public int itemValue = 10;      // valor base positivo
    public float clickDistance = 3f;

    private Transform player;
    private Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        // Si quieres poder recoger con click usando raycast
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    Collect();
                }
            }
        }
    }

    // Tambien se puede recoger por trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    // ESTE es el metodo que llama PlayerMovement
    public void Collect()
    {
        // Calcular cuanto puntaje suma o resta
        int deltaScore = Mathf.Abs(itemValue);
        bool isGood = (itemType == ItemType.CapsulaVerde);

        if (!isGood)
        {
            deltaScore = -deltaScore; // resta si es capsula roja
        }

        // Actualizar GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(deltaScore);
            GameManager.Instance.AddItem();
        }

        // Sonidos
        if (AudioController.Instance != null)
        {
            if (isGood)
                AudioController.Instance.PlayCaptureGoodSound();
            else
                AudioController.Instance.PlayCaptureBadSound();
        }

        // Destruir el objeto
        Destroy(gameObject);
    }
}
