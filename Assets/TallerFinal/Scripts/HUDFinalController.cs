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
    public TMP_Text txtFallsFinal;   

    private bool alreadyShown = false;

    void Start()
    {

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

            if (txtScoreFinal != null)
            {
                txtScoreFinal.text = "Puntos: " + GameManager.Instance.Score.ToString();
            }


            if (txtTimeFinal != null)
            {
                float t = GameManager.Instance.GlobalTime;
                int minutes = Mathf.FloorToInt(t / 60f);
                int seconds = Mathf.FloorToInt(t % 60f);

                txtTimeFinal.text = string.Format("Tiempo: {0:00}:{1:00}", minutes, seconds);
            }

            if (txtItemsFinal != null)
            {
                txtItemsFinal.text = "Items: " + GameManager.Instance.ItemsCount.ToString();
            }

            if (txtFallsFinal != null)
            {
                txtFallsFinal.text = "Caidas: " + GameManager.Instance.FallsCount.ToString();
            }
        }

        if (panelFinal != null)
        {
            panelFinal.SetActive(true);
        }

    }
}