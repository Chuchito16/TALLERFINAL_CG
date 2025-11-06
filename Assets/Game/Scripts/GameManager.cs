using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private float globalTime = 0f;


    private int score = 0;
    private int itemsCount = 0;
    private int fallsCount = 0;


    public float GlobalTime { get { return globalTime; } }
    public int Score { get { return score; } }
    public int ItemsCount { get { return itemsCount; } }
    public int FallsCount { get { return fallsCount; } }

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ========= METODOS DE TIEMPO =========

    // El Timer llama a esto para actualizar el tiempo global
    public void SetTime(float time)
    {
        globalTime = time;
    }

    public void ResetAll()
    {
        globalTime = 0f;
        score = 0;
        itemsCount = 0;
        fallsCount = 0;
    }

    // ========= METODOS DE SCORE / ITEMS / CAIDAS =========

    public void AddScore(int scoreItem)
    {
        score += scoreItem;
    }

    public void AddItem()
    {
        itemsCount++;
    }

    public void RegisterFall()
    {
        fallsCount++;
    }
}
