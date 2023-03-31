using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if((other.gameObject.tag == "Enemy") || (other.gameObject.tag == "Puzzle"))
        {
            rb = other.GetComponent<Rigidbody>();
            rb.AddForce(this.gameObject.transform.forward * 10, ForceMode.Impulse);
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        rb = null;
    }
}
