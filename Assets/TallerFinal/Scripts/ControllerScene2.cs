using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ControllerScene2 : MonoBehaviour
{
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoItem;


    void Start()
    {
        Debug.Log("El tiempo de la escena 1 "+GameManager.Instance.GlobalTime.ToString());
    }


    void Update()
    {
        
        textoScore.text= GameManager.Instance.Score.ToString();
        textoItem.text= GameManager.Instance.ItemsCount.ToString();

    }


}
