using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetingReticle : MonoBehaviour
{
    public GameObject Obj;
    public Camera CameraObj;
    public GameObject Player;
    public GameObject DisplayTextParent;
    public Text ConditionText;
    public Text DistanceText;
    public bool isMothership;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Obj == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Vector3 targetposition = CameraObj.WorldToScreenPoint(Obj.transform.position);
            float targetscale = Vector3.Distance(Player.transform.position, Obj.transform.position);
            if (targetposition.z < 0 || targetscale < 25)
            {
                this.GetComponent<Image>().enabled = false;
                DisplayTextParent.SetActive(false);
            }
            else
            {
                this.GetComponent<Image>().enabled = true;
                DisplayTextParent.SetActive(true);
            }
            transform.position = new Vector3(targetposition.x, targetposition.y, 0);
            if (targetscale > 100) { targetscale = 100; } else if (targetscale < 50) { targetscale = 50; }
            targetscale = (150 - targetscale) / 50;
            transform.localScale = new Vector3(targetscale, targetscale, 1);
            DistanceText.text = "Distance: " + Vector3.Distance(Player.transform.position, Obj.transform.position) + " m";
            if (isMothership) {ConditionText.text = "Health:" + Obj.GetComponent<Mothershipscript>().EnemyHealth; }
        }
    }

    public void SetTarget(GameObject newobject, GameObject camera, GameObject newplayer)
    {
        Obj = newobject;
        CameraObj = camera.GetComponent<Camera>();
        Player = newplayer;
    }
}
