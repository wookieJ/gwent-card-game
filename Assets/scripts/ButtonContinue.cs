using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonContinue : MonoBehaviour {
    public GameObject buttonObject;
    public Button button;

    void Awake()
    {
        button = buttonObject.GetComponent<Button>();
    }

    public void removeButton()
    {
        button.transform.position = new Vector3(0, 15f, -1f);
    }
}
