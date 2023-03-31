using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Silvie : MonoBehaviour
{
    public PlayerMovement move;
    public GameObject create;
    public Animator silvieAnimator;
    public bool tiempoActivo = false;
    public bool poderActivo = false;
    public float jumpHeight = 10;
    public bool grounded;
    public bool creating;
    public GameObject[] platform;
    public GameObject spawn;
    public GameObject[] destroy;
    public GameObject melee;
    Rigidbody rb;
    public PlayerStats stamina;
    public GameManager pCounter;
    public AudioSource[] music;
    public AudioSource[] voice;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        silvieAnimator = GetComponent<Animator>();
        move = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            move.playerVerticalInput = 0.0f;
            move.playerHorizontalInput = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            silvieAnimator.SetTrigger("Jump");
            voice[0].Play();
        }

        if (creating)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Creando");
                CreatePlatform();
                silvieAnimator.SetTrigger("Create");
                voice[4].Play();

            }
        }


        

        if (stamina.stamina > 0)
        {
            if (tiempoActivo)
            {
                if (Time.timeScale == 2.0f)
                {
                    StaminaDrainFast();
                    if (stamina.stamina <= 0)
                    {
                        Time.timeScale = 1.0f;
                        move.speed = 0.04f;
                        music[0].pitch = 1.0f;
                        music[1].pitch = 1.0f;
                        //voice[5].Play();
                        silvieAnimator.speed = 1;
                        tiempoActivo = false;
                    }
                }

                if (Time.timeScale == 0.2f)
                {
                    StaminaDrainSlow();
                    if (stamina.stamina <= 0)
                    {
                        Time.timeScale = 1.0f;
                        move.speed = 0.04f;
                        music[0].pitch = 1.0f;
                        music[1].pitch = 1.0f;
                        //voice[5].Play();
                        silvieAnimator.speed = 1;
                        tiempoActivo = false;
                    }
                }

            }
            else
            {
                Time.timeScale = 1.0f;
                silvieAnimator.speed = 1;
                music[0].pitch = 1.0f;
                music[1].pitch = 1.0f;
                //voice[5].Play();
                move.speed = 0.04f;
            }
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            Time.timeScale = 0.2f;
            move.speed = 0.02f;
            silvieAnimator.speed = 5;
            music[0].pitch = 0.8f;
            music[1].pitch = 0.8f;
            voice[2].Play();
            tiempoActivo = true;
            silvieAnimator.SetTrigger("Power");
        }

        

        if (pCounter.platformCounter >= 3)
        {
            
            creating = false;
            pCounter.platformCounter = 0;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            tiempoActivo = false;
            silvieAnimator.SetTrigger("Power");
            voice[5].Play();
        }

        

        if (Input.GetKeyDown(KeyCode.C))
        {
            Time.timeScale = 2.0f;
            move.speed = 0.08f;
            music[0].pitch = 1.2f;
            music[1].pitch = 1.2f;
            voice[3].Play();
            silvieAnimator.speed = 0.5f;
            tiempoActivo = true;
            silvieAnimator.SetTrigger("Power");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Attack());

        }

        Debug.Log("Tiempo: " + Time.timeScale + " , MoveSpeed: " + move.speed);
    }

    public void StaminaDrainSlow()
    {
        stamina.stamina -= Time.deltaTime * 8;
    }

    public void StaminaDrainFast()
    {
        stamina.stamina -= Time.deltaTime * 1;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded = true;
            silvieAnimator.SetBool("Grounded", true);
            //jumpsRemaining = maxJumpCount;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            silvieAnimator.SetBool("Grounded", false);
            grounded = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Creation")
        {
            Debug.Log("Entre al Trigger");
            if (!creating)
            {
                creating = true;
                create.SetActive(true);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Creation")
        {
            Debug.Log("Sali del Trigger");
            if (creating)
            {
                creating = false;
                create.SetActive(false);
            }
        }
    }

    public void CreatePlatform()
    {
        if (pCounter.platformCounter < 3)
        {
            Destroy(destroy[pCounter.platformCounter]);
            destroy[pCounter.platformCounter] = Instantiate(platform[pCounter.platformCounter], new Vector3(spawn.transform.position.x, spawn.transform.position.y, spawn.transform.position.z), Quaternion.identity);
            pCounter.platformCounter++;
            creating = false;
        }
    }

    IEnumerator Attack()
    {
        silvieAnimator.SetTrigger("Throw");
        melee.SetActive(true);
        voice[1].Play();
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Sali del ataque");
        melee.SetActive(false);
    }

}
