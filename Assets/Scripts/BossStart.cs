using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public AudioSource[] original;
    public AudioSource[] boss;
    public GameObject bossHealth;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bossHealth.SetActive(true);
            for(int i = 0; i < original.Length; i++)
            {
                original[i].Stop();
            }

            boss[PlayerPrefs.GetInt("Character") - 1].Play();
        }
    }


}
