using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text itemsText;
    [SerializeField] private TMP_Text fallsText;

    private void Update()
    {
        if (GameManager.Instance == null) return;

        scoreText.text = "Score: " + GameManager.Instance.Score;
        itemsText.text = "Items: " + GameManager.Instance.ItemsCount;
        fallsText.text = "Falls: " + GameManager.Instance.FallsCount;
    }
}
