using UnityEngine;
using UnityEngine.InputSystem;

public class CollectableItem : MonoBehaviour
{
    public enum ItemType
    {
        CapsulaVerde,  
        CapsulaRoja
    }

    public ItemType itemType;
    public int itemValue = 10;   
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {

        int deltaScore = Mathf.Abs(itemValue);
        bool isGood = (itemType == ItemType.CapsulaVerde);

        if (!isGood)
        {
            deltaScore = -deltaScore;
        }


        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(deltaScore);
            GameManager.Instance.AddItem();
        }


        if (AudioController.Instance != null)
        {
            if (isGood)
                AudioController.Instance.PlayCaptureGoodSound();
            else
                AudioController.Instance.PlayCaptureBadSound();
        }


        Destroy(gameObject);
    }
}
