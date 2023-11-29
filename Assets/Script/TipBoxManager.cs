using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipBoxManager : MonoBehaviour
{
    public static TipBoxManager _instance;
    public TextMeshProUGUI textComponent;


    private void Awake(){
        if (_instance != null && _instance != this){
            Destroy(this.gameObject);
        }
        else{
            _instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;   
    }

    public void SetAndShowTipBox(string message){
        gameObject.SetActive(true);
        textComponent.text = message;
    }
    public void HideTipBox(){
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }
}
