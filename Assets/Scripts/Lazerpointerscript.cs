using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazerpointerscript : MonoBehaviour
{
    public GameObject Lazerpointertarget;
    public Camera CameraObj;
    void Update()
    {
        Vector3 targetposition = CameraObj.WorldToScreenPoint(Lazerpointertarget.transform.position);
        transform.position = new Vector3(targetposition.x, targetposition.y, 0);
    }
}
