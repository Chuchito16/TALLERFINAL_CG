using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableItem : MonoBehaviour
{
    public enum ItemType
    {
        CapsulaVerde,
        CapsulaRoja
    }

    [Header("Config")]
    public ItemType itemType;
    public int itemValue = 10;  // valor base, el signo se decide por el tipo

    private bool collected = false;  // para evitar recoger dos veces

    // Metodo publico por si lo llamas desde otro lado
    public void Collect()
    {
        if (collected) return;
        collected = true;
        CollectItem();
    }

    // Se llama cuando el player entra en el trigger del item
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Collect();
    }

    private void CollectItem()
    {
        // Tomamos el valor absoluto y luego decidimos el signo
        int deltaScore = Mathf.Abs(itemValue);

        bool isPositiveItem = (itemType == ItemType.CapsulaVerde);
        if (!isPositiveItem)
        {
            // Capsula roja: resta
            deltaScore = -deltaScore;
        }

        // Actualizar GameManager
        if (GameManager.Instance != null)
        {
            // siempre actualiza el score (positivo o negativo)
            GameManager.Instance.AddScore(deltaScore);

            // solo contamos items que suman
            if (isPositiveItem && deltaScore > 0)
            {
                GameManager.Instance.AddItem();
            }
        }

        Debug.Log("Recolectado: " + name + " (" + deltaScore + " puntos)");

        // Cambio de escena si ya se recolectaron 15 items positivos
        if (GameManager.Instance != null && GameManager.Instance.ItemsCount >= 15)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "Scene_1")   // ojo con el nombre real de tu escena
            {
                SceneManager.LoadScene("Scene_2");
                return;
            }
        }

        // Destruir el objeto
        Destroy(gameObject);
    }
}