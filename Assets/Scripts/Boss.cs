using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxThrows;
    public int countThrows;
    public GameObject sniper;
    public GameObject[] bullet;
    public Transform[] attackPoint;
    public float waitTime;
    public float spikeTime;
    public GameObject spikes;
    public BoxCollider[] cajas;
    bool dentro;
    bool active;
    public Image health;


    void Start()
    {
        countThrows = 0;
        waitTime = 3.0f;
        spikeTime = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {

        for(int i = 0; i <= attackPoint.Length; i++)
        {
            if (countThrows <= maxThrows && waitTime <= 0)
            {
                for(int j = 0; j<= attackPoint.Length; j ++)
                {
                    bullet[j] = Instantiate(sniper, attackPoint[j].position, this.gameObject.transform.rotation);

                    bullet[j].transform.eulerAngles = new Vector3(attackPoint[j].position.x, attackPoint[i].position.y, attackPoint[i].position.z - 90.0f);

                    Rigidbody projectileRB = bullet[j].GetComponent<Rigidbody>();

                    Vector3 forceToAdd = this.gameObject.transform.forward * 20;
                    projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

                    countThrows++;
                    waitTime = 3.0f;
                    Destroy(bullet[j], 5);
                }
                
            }
        }
    
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }

        if(health.fillAmount == 0.0f)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(2);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            StartCoroutine(SpikeTrap());       
                        
        }

        if(other.gameObject.tag == "Melee")
        {
            /*for (int i = 0; i <= cajas.Length; i++)
            {
                cajas[i].enabled = false;
            }*/
            health.fillAmount = health.fillAmount - 0.05f;
            //StartCoroutine(Invincible());
            /*for (int i = 0; i <= cajas.Length; i++)
            {
                cajas[i].enabled = true;
            }*/
        }
    }

    public IEnumerator SpikeTrap()
    {
        if(!active)
        {
            active = true;
            yield return new WaitForSeconds(2.0f);
            spikes.SetActive(true);
            yield return new WaitForSeconds(10.0f);
            spikes.SetActive(false);
            active = false;
        }
           
    }

    public IEnumerator Invincible()
    {
        
        Debug.Log("Cajas desactivadas");
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Cajas Activadas");
    }
}
 

