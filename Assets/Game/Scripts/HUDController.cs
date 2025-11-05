using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text itemsText;
    [SerializeField] private TMP_Text fallsText;

    private void Update()
    {
        if (GameManager.Instance == null) return;

        UpdateTimer(GameManager.Instance.GlobalTime);

        scoreText.text = "Score: " + GameManager.Instance.Score;
        itemsText.text = "Items: " + GameManager.Instance.ItemsCount;
        fallsText.text = "Falls: " + GameManager.Instance.FallsCount;
    }

    private void UpdateTimer(float time)
    {
        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60f);
        int millis = (int)((time - Mathf.Floor(time)) * 1000f);

        timerText.text = string.Format(
            "Time: {0:00}:{1:00}:{2:000}",
            minutes, seconds, millis
        );
    }
}
