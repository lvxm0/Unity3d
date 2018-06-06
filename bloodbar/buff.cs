using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buff : MonoBehaviour {

    public GameObject slider;
    private Image sImage;
    // Use this for initialization
    void Start()
    {
        sImage = slider.GetComponent<Image>();
        Button Subbtn = this.GetComponent<Button>();
        Subbtn.onClick.AddListener(Click);
    }

    public void Click()
    {
        sImage.color = Color.green;
    }
}
