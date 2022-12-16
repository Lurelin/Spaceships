using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretscript : MonoBehaviour
{
    public GameObject target;
    public GameObject barrel;
    public GameObject mothership;
    public ParticleSystem lazerobj;
    public LayerMask raycastmask;
    float Fire;
    public float firecheck;
    // Update is called once per frame
    void Start() {
    }
    void Update()
    {
        Vector3 relativePos = transform.position - target.transform.position;
        Quaternion newquat = Quaternion.LookRotation(relativePos, Vector3.forward);
        if (Physics.Linecast(transform.position, target.transform.position, raycastmask) == false)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newquat, 20 * Time.deltaTime);
            if (Quaternion.Angle(newquat, transform.rotation) < 1)
            {
                if (Fire >= firecheck)
                {
                    lazerobj.Emit(1);
                    Fire = 0;
                }
            }
        }
        if (Fire < firecheck)
        {
            Fire += Time.deltaTime;
        }
    }
}
