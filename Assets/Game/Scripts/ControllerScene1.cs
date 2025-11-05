using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ControllerScene1 : MonoBehaviour
{
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoItem;
    public Timer tiempoEscena;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        textoScore.text= GameManager.Instance.Score.ToString();
        textoItem.text= GameManager.Instance.ItemsCount.ToString();

    }

    public void TimeScene()
    {
        tiempoEscena.TimerStop();

        float tiempoParado = tiempoEscena.StopTime;
        GameManager.Instance.AddTime(tiempoParado);
    }


}
