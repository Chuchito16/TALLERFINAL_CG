using UnityEngine;
using TMPro; 

public class CollisionPoints : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;      
    public GameObject finalPanel;
    public TMP_Text resultText;     

    private int hitsGreen = 0;
    private int hitsRed = 0;

    void Start()
    {
        UpdateUI();
        if (finalPanel != null)
            finalPanel.SetActive(false);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.CompareTag("Verde"))
        {
            score += 5;
            hitsGreen++;
            Destroy(hit.gameObject);
            UpdateUI();
        }

        if (hit.gameObject.CompareTag("Rojo"))
        {
            score -= 2;
            hitsRed++;
            Destroy(hit.gameObject);
            UpdateUI();
        }

        if (score >= 100)
        {
            ShowFinalPanel();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Puntos: " + score;
    }

    void ShowFinalPanel()
    {
        if (finalPanel != null)
        {
            finalPanel.SetActive(true);
            resultText.text =
                "¡Juego terminado!\n\n" +
                "Puntaje total: " + score + "\n" +
                "Aciertos (verdes): " + hitsGreen + "\n" +
                "Desaciertos (rojos): " + hitsRed;
        }
    }
}
