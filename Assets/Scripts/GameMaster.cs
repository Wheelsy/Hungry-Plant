using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public Player player;
    public GameObject spawnerObj;
    public GameObject energyBar;
    public GameObject gameOverMenu;
    public GameObject startMenu;
    public TextMeshProUGUI finalScore;
    public GameObject inputField;
    public GameObject username;
    public Animator anim;
    public float deathTimer = 0;
    public int flysEaten = 0;
    public int beesEaten = 0;
    public int butterflysEaten = 0;
    public int queenBeesEaten = 0;
    public int queenFlysEaten = 0;
    public int ladybugsEaten = 0;
    public TextMeshProUGUI flysAmount;
    public TextMeshProUGUI beesAmount;
    public TextMeshProUGUI butterflysAmount;
    public TextMeshProUGUI queenBeesAmount;
    public TextMeshProUGUI queenFlysAmount;
    public TextMeshProUGUI ladybugsAmount;
    public TextMeshProUGUI statsScore;
    public bool gameOver = false;

    public static bool playAgainClicked;

    private HighScores hs;
    private float timeSlice;
    private bool upSpeed = true;

    public float TimeSlice { get => timeSlice; set => timeSlice = value; }

    void Awake()
    {
        deathTimer = 5;
        spawnerObj.GetComponent<Spawner>();
        hs = gameObject.GetComponent<HighScores>();
    }

    void Start()
    {
        if (playAgainClicked == true)
        {         
            startMenu.GetComponent<MenuButtons>().StartGame();
        }

        StartCoroutine(RefreshHighscores());
    }

    public IEnumerator StartTimer()
    {
        for(int i = 0; i < 40; i++)
        {
            switch (i)
            {
                case 10:
                    SpeedUp();
                    break;

                case 20:
                    SpeedUp();
                    break;

                case 25:
                    SpeedUp();
                    break;

                case 30:
                    SpeedUp();
                    break;
                case 35:
                    SpeedUp();
                    break;
            }
            yield return new WaitForSeconds(1);

            upSpeed = true;
        }
    }

    void SpeedUp()
    {
        if (upSpeed == true)
        {
            deathTimer -= 0.5f;
            TimeSlice += 0.04f;
            upSpeed = false;
        }
    }

    public IEnumerator DecreaseSlider()
    {
        Slider slider = energyBar.GetComponent<Slider>();
        if (energyBar != null)
        {
            TimeSlice = (slider.value / 20);
            while(slider.value > 0)
            {
                slider.value -= TimeSlice;
                yield return new WaitForSeconds(1);
                
                if(slider.value <= 0.001f)
                {
                    gameOver = true;
                    player.canAttack = false;
                    spawnerObj.SetActive(false);
                    anim.SetTrigger("Asleep");

                    flysAmount.text = flysEaten.ToString();
                    beesAmount.text = beesEaten.ToString();
                    butterflysAmount.text = butterflysEaten.ToString();
                    queenBeesAmount.text = queenBeesEaten.ToString();
                    queenFlysAmount.text = queenFlysEaten.ToString();
                    ladybugsAmount.text = ladybugsEaten.ToString();
                    statsScore.text = finalScore.text;

                    if (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        for (int i = 0; i < gameOverMenu.transform.childCount; i++)
                        {
                            Transform child = gameOverMenu.transform.GetChild(i);

                            if (child != null)
                            {
                                child.gameObject.SetActive(true);
                            }
                        }
                    }
                    else
                    {
                        if (CheckForHighscore(int.Parse(finalScore.text.ToString())) == true)
                        {
                            inputField.SetActive(true);
                        }
                        else
                        {
                            for (int i = 0; i < gameOverMenu.transform.childCount; i++)
                            {
                                Transform child = gameOverMenu.transform.GetChild(i);

                                if (child != null)
                                {
                                    child.gameObject.SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void IncreaseSlider(float amount)
    {
        Slider slider = energyBar.GetComponent<Slider>();

        if ((slider.value += amount) <= slider.maxValue)
        {           
            slider.value += amount;
        }
        else
        {
            slider.value = slider.maxValue;
        }
    }

    bool CheckForHighscore(int score)
    {
        int lowestScore;

        if (hs.highscoresList.Length > 0)
        {
            lowestScore = hs.highscoresList[0].score;
        }
        else
        {
            lowestScore = 0;
        }

        if (hs.highscoresList.Length < 10)
        {
            return true;
        }
        else
        {
            for (int i = 0; i < hs.highscoresList.Length; i++)
            {
                if (hs.highscoresList[i].score < lowestScore)
                {
                    lowestScore = hs.highscoresList[i].score;
                }
            }
        }

        if (score > lowestScore)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UploadScore()
    {
        int result;

        if (hs.CheckUsername() == false)
        {

            if (int.TryParse(finalScore.text.ToString(), out result))
            {
                StartCoroutine(hs.UploadNewScore(username.GetComponent<TextMeshProUGUI>().text, result));
                gameOverMenu.GetComponent<Image>().enabled = false;
                inputField.SetActive(false);

                for (int i = 0; i < gameOverMenu.transform.childCount; i++)
                {
                    Transform child = gameOverMenu.transform.GetChild(i);

                    if (child != null)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    IEnumerator RefreshHighscores()
    {      
        while (true)
        {
            StartCoroutine(hs.DownloadHighscoresFromDB());

            yield return new WaitForSeconds(10);
        }
    }
}
