using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddUGUI : MonoBehaviour {
    public Slider slider;
    // Use this for initialization
    void Start () {
        Button Addbtn = this.GetComponent<Button>();
        Addbtn.onClick.AddListener(Click);
    }

    public void Click()
    {
        if (slider.value < 1)
        {
            slider.value += 0.1f;
        }
        else
        {
            slider.value = 1;
        }
    }
}
