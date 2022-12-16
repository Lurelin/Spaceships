using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 25;
    public float Firerate = 0.3f;
    public GameObject Player;
    public ParticleSystem lazer1;
    public ParticleSystem lazer2;
    float fireratecheck;
    bool fireallowed = true;
    public Rigidbody RB;

    void Update()
    {
        Vector3 relativePos = transform.position - Player.transform.position;
        Quaternion newquat = Quaternion.LookRotation(relativePos, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newquat, speed * Time.deltaTime);
        if (fireallowed == false)
        {
            if (fireratecheck > Firerate)
            {
                fireallowed = true;
                fireratecheck = 0;
            }
            else
            {
                fireratecheck += Time.deltaTime;
            }
        }
        if (Quaternion.Angle(newquat, transform.rotation) < 10 && fireallowed == true) {
            lazer1.Emit(1);
            lazer2.Emit(1);
            fireallowed = false;
        }
        if (Vector3.Distance(transform.position, Player.transform.position) > 30)
        {
            RB.AddForce(-transform.forward * 4000 * (180 - Quaternion.Angle(newquat, transform.rotation)) * Time.deltaTime);
        }

    }

    void OnParticleCollision() {
        Destroy(this.gameObject);
    }
}
