using UnityEngine;
using TMPro;

public class HUDFinalController : MonoBehaviour
{
    [Header("Panel final")]
    public GameObject panelFinal;

    [Header("Textos final")]
    public TMP_Text txtScoreFinal;
    public TMP_Text txtTimeFinal;
    public TMP_Text txtItemsFinal;

    private bool alreadyShown = false;

    void Start()
    {
        // Al iniciar la escena el panel final debe estar oculto
        if (panelFinal != null)
        {
            panelFinal.SetActive(false);
        }
    }

    public void ShowFinalHUD()
    {
        if (alreadyShown)
            return;

        alreadyShown = true;

        if (GameManager.Instance != null)
        {
            // Puntos
            if (txtScoreFinal != null)
            {
                txtScoreFinal.text = "Puntos: " + GameManager.Instance.Score.ToString();
            }

            // Tiempo en formato mm:ss
            if (txtTimeFinal != null)
            {
                float t = GameManager.Instance.GlobalTime;
                int minutes = Mathf.FloorToInt(t / 60f);
                int seconds = Mathf.FloorToInt(t % 60f);

                txtTimeFinal.text = string.Format("Tiempo: {0:00}:{1:00}", minutes, seconds);
            }

            // Items
            if (txtItemsFinal != null)
            {
                txtItemsFinal.text = "Items: " + GameManager.Instance.ItemsCount.ToString();
            }
        }

        if (panelFinal != null)
        {
            panelFinal.SetActive(true);
        }

        // Opcional: pausar el juego
        // Time.timeScale = 0f;
    }
}
