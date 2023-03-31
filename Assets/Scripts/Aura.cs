using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    public Animator auraAnimator;
    Rigidbody rb;
    public float jumpHeight = 10;
    public bool grounded;
    public bool isGliding = false;
    public float waitTime = 5.0f;
    public PlayerStats stamina;
    public GameManager wait;
    public GameObject wind;
    public GameObject melee;
    public bool attack;
    public AudioSource[] voice;

    // Start is called before the first frame update
    void Start()
    {
        auraAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        attack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            auraAnimator.SetTrigger("Jump");
            voice[0].Play();

        }

        if (stamina.stamina <= 0)
        {
            StaminaDeplet();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if(!attack && !isGliding)
            {
                if(stamina.stamina > 0)
                {
                    stamina.stamina = stamina.stamina - 20;
                    attack = true;
                    wind.SetActive(true);
                    auraAnimator.SetTrigger("Attack");
                    voice[2].Play();
                }
                
            }
            
            
            if(stamina.stamina <= 0)
            {
                wind.SetActive(false);
                attack = false;
            }
        }
        else if(Input.GetKeyUp(KeyCode.R))
        {
            wind.SetActive(false);
            attack = false;
        }

        if(attack)
        {
            StaminaDrain();
            
        }


        if (Input.GetKeyDown(KeyCode.Space) && !grounded && stamina.stamina > 0)
        {
            rb.drag = 10;
            isGliding = true;
            auraAnimator.SetBool("Glide", true);
            voice[3].Play();
        }

        if(isGliding)
        {
            waitTime--;
            StaminaDrain();
            if (Input.GetKeyDown(KeyCode.Space) && waitTime < 0)
            {
                Debug.Log("Entre al waittime");
                StaminaDeplet();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Attack());

        }
    }

    public void StaminaDeplet()
    {
        rb.drag = 0;
        auraAnimator.SetBool("Glide", false);
        attack = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded = true;
            rb.drag = 0;
            isGliding = false;
            waitTime = 5;
            auraAnimator.SetBool("Grounded", true);
            auraAnimator.SetBool("Glide", false);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            auraAnimator.SetBool("Grounded", false);
            grounded = false;
        }
    }

    public void StaminaDrain()
    {
        stamina.stamina -= Time.deltaTime * 5;
        
    }

    IEnumerator Attack()
    {
        auraAnimator.SetTrigger("Throw");
        voice[1].Play();
        melee.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Sali del ataque");
        melee.SetActive(false);
    }
}
