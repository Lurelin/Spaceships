using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mothershipscript : MonoBehaviour
{
    public float speed = 10;
    public float SpawnEnemyRate = 30f;
    public float PrimarySpawnRate = 5f;
    public GameObject Player;
    public Vector3 spawnoffset;
    public GameObject Canvas;
    public GameObject Camera;
    public GameObject enemyprefab;
    public GameObject targetingprefab;
    float spawnenemyratecheck;
    bool spawnenemyallowed = true;
    public Rigidbody RB;
    float enemiesspawned = 0;
    public float EnemyHealth = 100;
    bool Hyperspace = false;
    public float HyperspaceCooldown = 10;
    void Update()
    {
        Vector3 relativePos = transform.position - Player.transform.position;
        Quaternion newquat = Quaternion.LookRotation(relativePos, Vector3.forward);
        if (Vector3.Distance(transform.position, Player.transform.position) > 6000)
        {
            if (Quaternion.Angle(newquat, transform.rotation) < 1 && HyperspaceCooldown > 0)
            {
                Hyperspace = true;
                HyperspaceCooldown = 5;
            }
        }
        if (Vector3.Distance(transform.position, Player.transform.position) > 1500)
        {
            if (spawnenemyratecheck > SpawnEnemyRate || (enemiesspawned < 3 && spawnenemyratecheck > PrimarySpawnRate) || (enemiesspawned < 10 && spawnenemyratecheck > PrimarySpawnRate / 2))
            {
                GameObject newenemy = Instantiate(enemyprefab, transform.position += spawnoffset, Quaternion.identity);
                GameObject targetobj = Instantiate(targetingprefab, new Vector3(0, 0, 0), Quaternion.identity);
                targetobj.GetComponent<TargetingReticle>().SetTarget(newenemy, Camera, Player);
                targetobj.transform.parent = Canvas.transform;
                newenemy.GetComponent<EnemyScript>().Player = Player;
                spawnenemyratecheck = 0;
                enemiesspawned++;
            }
            else
            {
                spawnenemyratecheck += Time.deltaTime;
            }
            if (Hyperspace == true)
            {
                RB.AddForce(-transform.forward * 800000000 * Time.deltaTime);
                HyperspaceCooldown -= Time.deltaTime;
            }
            else {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, newquat, speed * Time.deltaTime);
                if (Quaternion.Angle(newquat, transform.rotation) < 1)
                {
                    RB.AddForce(-transform.forward * 4000000 * Time.deltaTime);
                }
            }
        }
        else {
            RB.velocity = RB.velocity * 0.95f;
            if (Hyperspace == true) {
                Hyperspace = false;
                HyperspaceCooldown = -10;
                RB.velocity = RB.velocity * 0.05f;
            }
        }
        if (HyperspaceCooldown < 0 && Hyperspace == false)
        {
            HyperspaceCooldown += Time.deltaTime;
        }
        if (HyperspaceCooldown < 0) {
            Hyperspace = false;
        }
        if (EnemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnParticleCollision()
    {
        EnemyHealth--;
    }
}
