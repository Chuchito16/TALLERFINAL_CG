using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Tiempo total
    private float globalTime = 0f;
    private bool timerRunning = true;

    // Puntaje e items
    private int score = 0;
    private int itemsCount = 0;

    // Caidas
    private int fallsCount = 0;

    // Propiedades publicas de solo lectura
    public float GlobalTime { get { return globalTime; } }
    public int Score { get { return score; } }
    public int ItemsCount { get { return itemsCount; } }
    public int FallsCount { get { return fallsCount; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (timerRunning)
        {
            globalTime += Time.deltaTime;
        }
    }

    // ===== METODOS PUBLICOS =====

    public void ResetAll()
    {
        globalTime = 0f;
        score = 0;
        itemsCount = 0;
        fallsCount = 0;
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void AddTime(float timeScene)
    {
        globalTime += timeScene;
    }

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
