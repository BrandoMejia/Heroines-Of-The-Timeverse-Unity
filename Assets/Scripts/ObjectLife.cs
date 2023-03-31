using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLife : MonoBehaviour
{
    public float waitTime = 5.0f;

    public GameObject player;

    public bool Aura;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

            waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);

        }

        if (other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
