using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Camera cam;
    public Transform cinemachineCamera;
    public Animator characterAnimator;
    public int contador = 2;
    public float rotationSpeed;
    public GameObject checkpoint;
    public PlayerStats health;
    public AudioSource[] voice;
    public Image background;
    public bool carga;
    public bool muerte;

    public float playerVerticalInput;
    public float playerHorizontalInput;

    public void Awake()
    {
        StartCoroutine(Wait());
    }

    void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerVerticalInput = Input.GetAxis("Vertical");
        playerHorizontalInput = Input.GetAxis("Horizontal");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeVerticalInput = (playerVerticalInput * forward) * speed;
        Vector3 rightRelativeHorizontalInput = (playerHorizontalInput * right) * speed;

        Vector3 cameraRelativeMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;
        this.transform.Translate(cameraRelativeMovement, Space.World);
        if (playerHorizontalInput > 0 || playerVerticalInput > 0) {
            Vector3 viewDir = this.transform.position - new Vector3(cinemachineCamera.position.x, this.transform.position.y, cinemachineCamera.position.z);
            
            //Debug.Log(viewDir);
            this.transform.forward = Vector3.Slerp(this.transform.forward, viewDir.normalized, Time.deltaTime * rotationSpeed);
        }

        characterAnimator.SetFloat("Run", playerVerticalInput);

        if(health.health <= 0.0f && !muerte)
        {
            voice[1].Play();
            muerte = true;
            StartCoroutine(DeathScreen());
        }

        health.lives = contador;

        

        Debug.Log(background.color.a);


    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            checkpoint = other.gameObject;
            voice[2].Play();
            checkpoint.transform.position = other.transform.position;
        }

        if (other.gameObject.tag == "Void")
        {
            health.health = 0.0f;
            
        }

        if (other.gameObject.tag == "Enemy")
        {
            health.health = health.health - 10.0f;
            voice[0].Play();
        }

        if (other.gameObject.tag == "Spike")
        {
            health.health = health.health - 50.0f;
            voice[0].Play();
        }

    }

    public IEnumerator DeathScreen()
    {
        Debug.Log("Entre a la deathscreen");
        if (background.color.a < 1.0f)
        {
            background.color = new Color(background.color.r, background.color.g, background.color.b, background.color.a + (Time.deltaTime));
        }
        yield return new WaitForSeconds(3);
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        

        StartCoroutine(Respawn());

    }
    
    public IEnumerator Respawn()
    {
        contador--;
        this.transform.position = checkpoint.transform.position;
        health.health = 100.0f;
        muerte = false;
        yield return null;
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }
}
