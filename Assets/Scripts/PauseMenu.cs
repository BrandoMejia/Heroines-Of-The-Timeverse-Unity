using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public bool press;
    // Start is called before the first frame update
    void Start()
    {
        press = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pausa();
        }

       /*if(Input.GetKeyDown(KeyCode.Escape) && press)
        {
            Continue();
        }*/
    }

    public void Pausa()
    {
        menu.SetActive(true);
        Time.timeScale = 0.0f;
        press = true;

    }
    public void Continue()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        press = false;
    }

    public void Title()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
