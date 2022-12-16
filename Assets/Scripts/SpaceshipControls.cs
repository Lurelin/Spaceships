using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class SpaceshipControls : MonoBehaviour
{
    public Rigidbody Rb;
    public float Throttle;
    public float Thrust = 300;
    public Material enginemat;
    public Color enginematcolor;
    public ParticleSystem lazer1;
    public ParticleSystem lazer2;
    public float Firerate = 0.3f;
    float fireratecheck;
    bool fireallowed = true;
    public float damagescreentime = 0.075f;
    public float damagescreencheck;
    bool damagetaken = false;
    public Image damagescreen;
    public Slider Healthbar;
    public float healthlevel = 100;
    public GameObject modelobject;
    public GameObject turret;
    public float Ultraspeed;
    public CinemachineVirtualCamera hyperspacecamera;
    bool UltraspeedActive = false;
    // Start is called before the first frame update
    void Start()
    {
        enginemat = modelobject.GetComponent<MeshRenderer>().materials[3];
        enginematcolor = enginemat.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        Healthbar.value = healthlevel;
        if (Throttle > 0 && Input.GetKey(KeyCode.Z)) {
            Throttle -= 35 * Time.deltaTime;
        } else if (Throttle < 100 && Input.GetKey(KeyCode.X)) {
            Throttle += 35 * Time.deltaTime;
        }
        Rb.AddForce(-transform.forward * Thrust * (Throttle / 100) * 60 * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, -75 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, 75 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(50 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(-50 * Time.deltaTime, 0, 0);
        }
        if (fireallowed == false)
        {
            if (fireratecheck > Firerate)
            {
                fireallowed = true;
                fireratecheck = 0;
            } else {
                fireratecheck += Time.deltaTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && fireallowed == true)
        {
            Rb.AddForce(-transform.forward * Thrust * 6000 * Time.deltaTime);
            fireallowed = false;
            fireratecheck = -5;
        }
        if (Input.GetKey(KeyCode.Space) && fireallowed == true)
        {
            lazer1.Emit(1);
            lazer2.Emit(1);
            fireallowed = false;
        }
        if (damagetaken == true)
        {
            if (damagescreentime > damagescreencheck)
            {
                damagetaken = false;
                damagescreentime = 0;
                damagescreen.enabled = false;
            }
            else
            {
                damagescreentime += Time.deltaTime;
                damagescreen.enabled = true;
            }
        }
        else {
            damagescreen.enabled = false;
        }
        enginemat.SetColor("_EmissionColor", enginematcolor * (Throttle / 50));
        Time.timeScale = 1 - Input.GetAxis("Fire1") / 1.5f;
        turret.transform.localRotation = Quaternion.Euler(Input.GetAxis("VerticalArrows") * 30, Input.GetAxis("HorizontalArrows") * 20, 0);
        if (Ultraspeed > 0)
        {
            if (UltraspeedActive == false)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    hyperspacecamera.enabled = true;
                    Ultraspeed = 4;
                    UltraspeedActive = true;
                }
            }
            else {
                if (Ultraspeed - Time.deltaTime < 0)
                {
                    Ultraspeed = -10;
                }
                Ultraspeed -= Time.deltaTime;
                if (Ultraspeed < 3) {
                    Rb.AddForce(-transform.forward * 20000000 * Time.deltaTime);
                }
            }
        }
        else {
            Ultraspeed += Time.deltaTime;
            hyperspacecamera.enabled = false;
            UltraspeedActive = false;
        }
    }
    void OnParticleCollision()
    {
        if (healthlevel > 0)
        {
            healthlevel -= 0.1f;
            damagetaken = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
