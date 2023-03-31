using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miyuki : MonoBehaviour
{
    public Animator miyukiAnimator;
    Rigidbody rb;
    public float jumpHeight = 10;
    public bool grounded;
    public GameObject melee;

    public LayerMask whatIsWall;
    public float wallRunForce;
    public float maxWallRunTime;
    public float maxWallSpeed;
    [SerializeField] bool isWallRight;
    [SerializeField] bool isWallLeft;
    [SerializeField] bool isWallRunning;
    public float maxWallRunCameraTilt;
    public float wallRunCameraTilt;
    public int contadorMuro = 0;

    public AudioSource[] voice;

    public Transform orientation;

    public PlayerStats stamina;

    public GameManager tiempo;

    public int maxJumpCount = 2;
    public int jumpsRemaining = 0;

    public bool enter;

    public bool entro;

    // Start is called before the first frame update
    void Start()
    {
        miyukiAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jumpsRemaining -= 1;
            entro = false;
            miyukiAnimator.SetTrigger("Jump");
            voice[0].Play();
        }


        if(isWallRunning)
        {
            tiempo.waitTime = 10.0f;
        }

        if (jumpsRemaining == 0)
        {
            miyukiAnimator.SetTrigger("Double Jump");
        }

        if(contadorMuro == 1)
        {
            Debug.Log("Baje Stamina");
            stamina.stamina -= Time.deltaTime * 5;
        }

        if(stamina.stamina <= 0)
        {
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
            jumpsRemaining = 0;
        }

        CheckForWall();
        WallRunInput();

        enter = !isWallRunning;

        if(!enter && !entro)
        {
            entro = true;
            stamina.stamina -= 20;
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Attack());
            
        }

        

    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            grounded = true;
            miyukiAnimator.SetBool("Grounded", true);
            jumpsRemaining = maxJumpCount;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            miyukiAnimator.SetBool("Grounded", false);
            grounded = false;
        }
    }

    private void WallRunInput()
    {
        if(Input.GetKey(KeyCode.D) && isWallRight)
        {
            StartWallRun();
            StaminaDrain();
        }

        if (Input.GetKey(KeyCode.A) && isWallLeft)
        {
            StartWallRun();
            StaminaDrain();
        }
    }

    private void StartWallRun()
    {

            
            rb.useGravity = false;
            isWallRunning = true;

            if (rb.velocity.magnitude <= maxWallSpeed)
            {
                rb.AddForce(orientation.forward * wallRunForce * Time.deltaTime);
                if (isWallRight)
                {
                    rb.AddForce(orientation.right * wallRunForce / 5 * Time.deltaTime);
                }
                else
                {
                    rb.AddForce(-orientation.right * wallRunForce / 5 * Time.deltaTime);
                }
            }
       
    }

    private void StopWallRun()
    {
        rb.useGravity = true;
        isWallRunning = false;
        contadorMuro = 0;
    }

    private void CheckForWall()
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, 1f, whatIsWall);
        isWallLeft = Physics.Raycast(transform.position, - orientation.right, 1f, whatIsWall);

        if(!isWallLeft || !isWallRight)
        {
            StopWallRun();
        }
        
        if(isWallLeft || isWallRight)
        {
            jumpsRemaining = maxJumpCount;
        }
    }

    public void StaminaDrain()
    {
        stamina.stamina -= Time.deltaTime * 5;
    }

    IEnumerator Attack()
    {
        miyukiAnimator.SetTrigger("Throw");
        voice[1].Play();
        melee.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Sali del ataque");
        melee.SetActive(false);
    }
}
