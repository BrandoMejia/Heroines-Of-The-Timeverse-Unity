using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Slider playerHealth;
    public Slider playerStamina;
    public int lives = 2;
    public GameObject[] lifes;
    public GameObject gameOver;
    public float maxHealth = 100.0f;
    public float maxStamina = 100.0f;
    public float health;
    public float stamina;
 
    // Start is called before the first frame update
    void Start()
    {
        playerHealth.value = maxHealth;
        playerStamina.value = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        playerStamina.value = stamina;
        playerHealth.value = health;
        if(lives == 1)
        {
            lifes[1].SetActive(false);
        }
        if(lives <= 0)
        {
            gameOver.SetActive(true);
        }
    }

}
