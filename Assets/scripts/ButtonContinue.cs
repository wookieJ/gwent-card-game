using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonContinue : MonoBehaviour {
    private GameObject buttonObject;
    private Button button;

    void Awake()
    {
        buttonObject = GameObject.Find("Button");
        button = buttonObject.GetComponent<Button>();
    }

    public void removeButton()
    {
        button.transform.position = new Vector3(0, 15f, -1f);
    }   
}
