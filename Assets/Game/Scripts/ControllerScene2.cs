using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ControllerScene2 : MonoBehaviour
{
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("El tiempo de la escena 1 "+GameManager.Instance.GlobalTime.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
        textoScore.text= GameManager.Instance.Score.ToString();
        textoItem.text= GameManager.Instance.ItemsCount.ToString();

    }


}
