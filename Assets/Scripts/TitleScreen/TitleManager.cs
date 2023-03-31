using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public GameObject title;
    public GameObject character;
    public GameObject howTo;
    public GameObject[] loadingScreen;
    public GameObject loadingImage;
    public Image loadingBarFill;


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            character.SetActive(false);
            howTo.SetActive(false);
            title.SetActive(true);
        }
    }



    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    public void Title()
    {
        title.gameObject.SetActive(true);
        character.gameObject.SetActive(false);
    }

    public void Character()
    {
        title.gameObject.SetActive(false);
        character.gameObject.SetActive(true);
    }

    public void HowToPlay()
    {
        title.SetActive(false);
        howTo.SetActive(true);
    }

    public void Miyuki()
    {
        PlayerPrefs.SetInt("Character", 1);
        LoadScene(1);
    }

    public void Aura()
    {
        PlayerPrefs.SetInt("Character", 2);
        LoadScene(1);
    }

    public void Silvie()
    {
        PlayerPrefs.SetInt("Character", 3);
        LoadScene(1);
    }

    public void Salir()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        loadingImage.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        loadingScreen[Random.Range(0, 5)].SetActive(true);
        character.gameObject.SetActive(false);


        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }
}
