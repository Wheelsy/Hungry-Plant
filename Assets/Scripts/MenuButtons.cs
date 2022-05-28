using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameMaster gm;
    public GameObject spawner;
    [Space]
    public GameObject startMenu;
    [Space]
    public GameObject gameOverMenu;
    public GameObject highscoreScreen;
    public GameObject howToPlayScreen;
    public GameObject slider;
    public Sprite mute;
    public Sprite unmute;
    public GameObject volume;
    public Player player;

    private float previousVolume = 0;

    public void Menu()
    {
        GameMaster.playAgainClicked = false;
        SceneManager.LoadScene("Main");
    }

    public void PlayAgain()
    {
        GameMaster.playAgainClicked = true;
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();         
    }

    public void StartGame()
    {
        spawner.SetActive(true);
        StartCoroutine(gm.StartTimer());
        StartCoroutine(gm.DecreaseSlider());

        startMenu.GetComponent<Image>().enabled = false;

        for(int i = 0; i < startMenu.transform.childCount; i++)
        {
            Transform child = startMenu.transform.GetChild(i);

            if (child != null)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void Highscores()
    {
        startMenu.GetComponent<Image>().enabled = false;

        for (int i = 0; i < startMenu.transform.childCount; i++)
        {
            Transform child = startMenu.transform.GetChild(i);

            if (child != null)
            {
                child.gameObject.SetActive(false);
            }
        }
        highscoreScreen.SetActive(true);
    }

    public void HowToPlay()
    {
        startMenu.GetComponent<Image>().enabled = false;

        for (int i = 0; i < startMenu.transform.childCount; i++)
        {
            Transform child = startMenu.transform.GetChild(i);

            if (child != null)
            {
                child.gameObject.SetActive(false);
            }
        }

        howToPlayScreen.SetActive(true);
    }

    public void VolumeMenu()
    {
        if (slider.activeInHierarchy == true)
        {
            slider.SetActive(false);
            volume.GetComponent<Image>().sprite = mute;
            previousVolume = slider.GetComponent<Slider>().value;
            slider.GetComponent<Slider>().value = 0.0001f;
        }
        else
        {
            slider.SetActive(true);
            volume.GetComponent<Image>().sprite = unmute;

            if (previousVolume != 0)
            {
                slider.GetComponent<Slider>().value = previousVolume;
            }
            else
            {
                slider.GetComponent<Slider>().value = 0.0001f;
            }

        }
    }
}
