using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public PlayerStats player;
    public ThrowObject[] character;
    public AudioSource[] sound;
    
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag == "Health")
        {
            if (other.gameObject.tag == "Player")
            {
                player.health += 20.0f;
                Destroy(this.gameObject);
                sound[0].Play();
            }
        }
        else
        {
            if(this.gameObject.tag == "Stamina")
            {
                if (other.gameObject.tag == "Player")
                {
                    player.stamina += 20.0f;
                    Destroy(this.gameObject);
                    sound[1].Play();
                }
            }
            else
            {
                if(this.gameObject.tag == "Ammo")
                {
                    if (other.gameObject.tag == "Player")
                    {
                        for (int i = 0; i < character.Length; i++)
                        {
                            if (character[i].totalThrows < character[i].maxThrows)
                            {
                               
                                character[i].totalThrows = character[i].maxThrows;
                                sound[2].Play();
                                Destroy(this.gameObject);
                            }
                        }
                    }
                }
                
            }
        }
    }
}
