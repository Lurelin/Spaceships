using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackCameraScript : MonoBehaviour
{
    public Image Image1;
    public RawImage Image2;
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Image1.enabled = true;
            Image2.enabled = true;
        }
        else {
            Image1.enabled = false;
            Image2.enabled = false;
        }
    }
}
