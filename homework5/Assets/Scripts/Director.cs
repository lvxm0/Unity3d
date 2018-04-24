using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : System.Object
{
    //singlton instance
    private static Director _instance;
    public ISceneController currentScenceController { get; set; }
    private Director() { }
    //get instance
    public static Director getInstance()
    {
        if (_instance == null)
        {
            _instance = new Director();
        }
        return _instance;
    }
}
