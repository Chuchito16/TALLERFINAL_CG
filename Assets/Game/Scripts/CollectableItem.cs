using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollectableItem : MonoBehaviour
{
    public enum ItemType { Cereza, Kiwi, Bandera } 
    public ItemType itemType;
    public int itemValue = 0;
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
        // Detectar clic izquierdo
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    float distance = Vector3.Distance(transform.position, player.position);
                    if (distance <= clickDistance)
                    {
                        CollectItem();
                    }
                    else
                    {
                        Debug.Log("Demasiado lejos para recoger el ítem.");
                    }
                }
            }
        }
    }

    void CollectItem()
    {
        // Sumar al GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(itemValue);
            GameManager.Instance.AddItem();
        }

        Debug.Log($"Recolectado: {name} (+{itemValue} puntos)");
        Destroy(gameObject);

        if (GameManager.Instance.ItemsCount >= 15)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "Scene_1") 
            {
                SceneManager.LoadScene("Scene_2");
            }
        }
    }

    public void SetMainCamera(Camera cam)
    {
        mainCamera = cam;
    }
}
