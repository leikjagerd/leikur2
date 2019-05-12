using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickPlayScript : MonoBehaviour
{

	void Start()
    {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate() { StartGame(); });
    }

    public void StartGame(){
    	Application.LoadLevel(1);
    }
}
