using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    public TMP_Text ammo;

    bool readyToThrow;
    bool projectileLife;

    public GameObject[] projectile;

    public int maxThrows;

    public float waitProjectile = 10.0f;

    public Animator characterAnimator;

    public Transform CMcam;

    public AudioSource voice;


    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        readyToThrow = true;        
    }

    void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw(totalThrows);
            characterAnimator.SetTrigger("Throw");
            voice.Play();
        }

        ammo.text = "Objects: " + totalThrows.ToString();

        

    }

    public void Throw(int ammo)
    {
        readyToThrow = false;



        projectile[ammo] = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        if (objectToThrow.name == "Kunai")
        {
            projectile[ammo].transform.eulerAngles = new Vector3 (cam.transform.rotation.x -90.0f, cam.transform.rotation.y, cam.transform.rotation.z);
        }
        else
        {
            if (objectToThrow.name == "Fan")
            {
                projectile[ammo].transform.eulerAngles = new Vector3(cam.transform.rotation.x - 90.0f, cam.transform.rotation.y, cam.transform.rotation.z);
            }
            else
            {
                if (objectToThrow.name == "Rock")
                {
                    projectile[ammo].transform.eulerAngles = new Vector3(cam.transform.rotation.x - 90.0f, cam.transform.rotation.y, cam.transform.rotation.z);
                }
            }
        }

        

        Rigidbody projectileRB = projectile[ammo].GetComponent<Rigidbody>();

        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;
        projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        Invoke(nameof(ResetThrow), throwCooldown);

        Destroy(projectile[ammo], 10);

        //DestroyProjectile(projectile[ammo]);
    }

    public void  ResetThrow()
    {
        readyToThrow = true;
    }

    public void DestroyProjectile(GameObject gun)
    {
        waitProjectile -= Time.deltaTime;
        if (waitProjectile <= 0)
        {
            Destroy(gun);
            waitProjectile = 10.0f;
        }
            
    }

}
