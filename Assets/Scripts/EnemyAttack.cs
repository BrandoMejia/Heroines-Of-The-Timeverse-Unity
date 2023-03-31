using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public bool playerInSight;
    public int maxThrows;
    public int countThrows;
    public GameObject sniper;
    public GameObject bullet;
    public Transform attackPoint;
    public float waitTime;
    

    void Start()
    {
        playerInSight = false;
        countThrows = 0;
        waitTime = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInSight)
        {
           if(countThrows <= maxThrows && waitTime <=0)
            {
                bullet = Instantiate(sniper, attackPoint.position, this.gameObject.transform.rotation);

                bullet.transform.eulerAngles = new Vector3(attackPoint.position.x, attackPoint.position.y, attackPoint.position.z - 90.0f);

                Rigidbody projectileRB = bullet.GetComponent<Rigidbody>();

                Vector3 forceToAdd = this.gameObject.transform.forward * 20;
                projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

                countThrows++;
                waitTime = 5.0f;
                Destroy(bullet, 10);
            }
        }
        if(waitTime >0)
        {
            waitTime -= Time.deltaTime;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerInSight = true;
        }

    }
}
