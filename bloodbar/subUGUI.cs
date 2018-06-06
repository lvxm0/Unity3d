using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class subUGUI : MonoBehaviour {
    public Slider slider;
    // Use this for initialization
    void Start()
    {
        Button Subbtn = this.GetComponent<Button>();
        Subbtn.onClick.AddListener(Click);
    }

    public void Click()
    {
        if (slider.value > 0)
        {
            slider.value -= 0.1f;
        }
        else
        {
            slider.value = 0;
        }
    }
}
