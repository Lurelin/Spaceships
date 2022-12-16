using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Camera;
    public GameObject Player;
    public GameObject enemyprefab;
    public GameObject targetingprefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            GameObject newenemy = Instantiate(enemyprefab, new Vector3(0, 0, 0), Quaternion.identity);
            GameObject targetobj = Instantiate(targetingprefab, new Vector3(0, 0, 0), Quaternion.identity);
            targetobj.GetComponent<TargetingReticle>().SetTarget(newenemy, Camera, Player);
            targetobj.transform.parent = Canvas.transform;
            newenemy.GetComponent<EnemyScript>().Player = Player;
        }
    }
}
