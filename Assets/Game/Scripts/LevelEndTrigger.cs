using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    [Header("Scene settings")]
    [SerializeField] private string nextSceneName = "Q2"; // pon aqui el nombre exacto de tu escena 2

    private void OnTriggerEnter(Collider other)
    {
        // solo reaccionar cuando entra el Player
        if (!other.CompareTag("Player"))
            return;

        // cargar la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
