using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject miyuki;
    public GameObject aura;
    public GameObject silvie;
    public GameObject[] doors;
    public PlayerStats stamina;
    public Miyuki boolStaminaM;
    public Aura boolStaminaA;
    public Silvie boolStaminaS;
    public int  platformCounter = 0;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Character") == 1)
        {
            miyuki.SetActive(true);
            doors[PlayerPrefs.GetInt("Character") - 1].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Character") == 2)
        {
            aura.SetActive(true);
            doors[PlayerPrefs.GetInt("Character") - 1].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Character") == 3)
        {
            silvie.SetActive(true);
            doors[PlayerPrefs.GetInt("Character") - 1].SetActive(false);
        }

        stamina = GetComponent<PlayerStats>();
        waitTime = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!boolStaminaA.isGliding || !boolStaminaS.tiempoActivo)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0.0f)
            {
                if (stamina.stamina < 100.0f)
                {
                    stamina.stamina += Time.deltaTime * 10;
                }
                if (stamina.stamina > 100)
                {
                    stamina.stamina = stamina.maxStamina;
                }
            }
        }

        if (boolStaminaA.isGliding || boolStaminaS.tiempoActivo || boolStaminaA.attack)
        {
            waitTime = 10.0f;
        }
    }
}

        
        
   
