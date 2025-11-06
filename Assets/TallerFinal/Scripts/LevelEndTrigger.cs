using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    [Header("Scene settings")]
    [SerializeField] private string nextSceneName = "Q2"; 

    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player"))
            return;


        SceneManager.LoadScene(nextSceneName);
    }
}
