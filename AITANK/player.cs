using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public float hp;
    // Use this for initialization
    void Start () {
        hp = 100f;

	}
	
	// Update is called once per frame
	void Update () {
		if(hp < 0)
        {
            this.gameObject.SetActive(false);
        }
	}
    public float gethp()
    {
        return hp;
    }

    public void sethp(float hp)
    {
        this.hp = hp;
    }
}
