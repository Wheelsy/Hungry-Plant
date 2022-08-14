using System.Collections;
using UnityEngine.Advertisements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public Player player;
    public Spawner spawnerScript;
    public GameObject energyBar;
    public GameObject gameOverMenu;
    public GameObject startMenu;
    public TextMeshProUGUI finalScore;
    public GameObject inputField;
    public GameObject username;
    public GameObject watchAd;
    public Animator anim;
    public MenuButtons btns;
    public float deathTimer = 0;
    public int flysEaten = 0;
    public int beesEaten = 0;
    public int butterflysEaten = 0;
    public int queenBeesEaten = 0;
    public int queenFlysEaten = 0;
    public int ladybugsEaten = 0;
    public int stagBeetlesEaten = 0;
    public TextMeshProUGUI flysAmount;
    public TextMeshProUGUI beesAmount;
    public TextMeshProUGUI butterflysAmount;
    public TextMeshProUGUI queenBeesAmount;
    public TextMeshProUGUI queenFlysAmount;
    public TextMeshProUGUI ladybugsAmount;
    public TextMeshProUGUI stagBeetlesAmount;
    public TextMeshProUGUI statsScore;
    public bool gameOver = false;
    public GameObject countdown;

    public static bool playAgainClicked;

    private HighScores hs;
    private float timeSlice;
    private float energyRate = 0.1f;
    private int speedStage = 0;
    private int counter = 0;

    public float TimeSlice { get => timeSlice; set => timeSlice = value; }
    public int SpeedStage { get => speedStage; set => speedStage = value; }

    void Awake()
    {
        deathTimer = 5;
        spawnerScript.GetComponent<Spawner>();
        hs = gameObject.GetComponent<HighScores>();
    }

    void Start()
    {
        StartCoroutine(RefreshHighscores());
    }

    public void GameStart()
    {
        Debug.Log(energyRate);
        playAgainClicked = false;
        SpeedStage = 0;
        counter = 0;
        energyRate = 0.1f;
        spawnerScript.enabled = true;
        InvokeRepeating("StartTimer", 0, 1);
        StartCoroutine(DecreaseSlider());
        startMenu.GetComponent<Image>().enabled = false;

        for (int i = 0; i < startMenu.transform.childCount; i++)
        {
            Transform child = startMenu.transform.GetChild(i);

            if (child != null)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void StartTimer()
    {
        counter++;
        if (SpeedStage < 6)
        {
            switch (counter)
            {
                case 10:
                    SpeedStage = 1;
                    SpeedUp();
                    break;
                case 20:
                    SpeedStage = 2;
                    SpeedUp();
                    break;
                case 30:
                    SpeedStage = 3;
                    SpeedUp();
                    break;
                case 40:
                    SpeedStage = 4;
                    SpeedUp();
                    break;
                case 50:
                    SpeedStage = 5;
                    SpeedUp();
                    break;
                case 60:
                    SpeedStage = 6;
                    SpeedUp();
                    break;
            }
        }
        else
        {
            CancelInvoke("StartTimer");
        }
    }

    void SpeedUp()
    {
        spawnerScript.GetComponent<Spawner>().SpawnRepeatRate -= 0.01f;
        deathTimer -= 0.55f;
        energyRate -= 0.0097f;
    }

    public IEnumerator DecreaseSlider()
    {
        Slider slider = energyBar.GetComponent<Slider>();
        if (energyBar != null)
        {
            TimeSlice = (slider.value / 125);
            while (slider.value > 0)
            {
                slider.value -= TimeSlice;
                yield return new WaitForSeconds(energyRate);

                if (slider.value <= 0.001f && !gameOver)
                {
                    CancelInvoke("StartTimer");
                    spawnerScript.enabled = false;
                    gameOver = true;
                    player.canAttack = false;
                    anim.SetTrigger("Asleep");
                    if (CheckForHighscore(player.Points))
                    {
                        inputField.SetActive(true);
                    }
                    else
                    {
                        EndGame();
                    }

                }
            }
        }
    }

    public void EndGame()
    {
        for (int i = 0; i < gameOverMenu.transform.childCount; i++)
        {
            Transform child = gameOverMenu.transform.GetChild(i);

            if (child != null)
            {
                child.gameObject.SetActive(true);
            }
        }

        flysAmount.text = flysEaten.ToString();
        beesAmount.text = beesEaten.ToString();
        butterflysAmount.text = butterflysEaten.ToString();
        queenBeesAmount.text = queenBeesEaten.ToString();
        queenFlysAmount.text = queenFlysEaten.ToString();
        ladybugsAmount.text = ladybugsEaten.ToString();
        stagBeetlesAmount.text = stagBeetlesEaten.ToString();
        statsScore.text = finalScore.text;
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

                EndGame();
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
