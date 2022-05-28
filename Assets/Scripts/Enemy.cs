using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public Transform scorePopup;
    public Transform deathPrefab;
    public Transform runPrefab;
    public int slotPos;
    public bool canDie = true;
    public int score;

    private float deathTimer = 6f;
    private Spawner spawner;
    private Vector3 position;
    private GameMaster gm;

    public float DeathTimer { get => deathTimer; set => deathTimer = value; }

    void Awake()
    {
        deathTimer = GameObject.Find("GameMaster").GetComponent<GameMaster>().deathTimer;
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    void Start()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        position = transform.position;
        StartCoroutine(StartDeathTimer());
    }

    void Update()
    {
        if(gm.gameOver == true)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator StartDeathTimer()
    {
        yield return new WaitForSeconds(deathTimer);

        if (canDie == true)
        {
            RunAway();
        }
    }

    public void RunAway()
    {
        spawner.slotArr[slotPos].GetComponent<Slot>().isFull = false;
        Instantiate(runPrefab, position, transform.rotation);
        Destroy(gameObject);
    }

    public void Die()
    {
        ShowScore(score, transform.position);
        spawner.slotArr[slotPos].GetComponent<Slot>().isFull = false;
        Instantiate(deathPrefab, position, transform.rotation);
        Destroy(gameObject);

    }

    public void ShowScore(int score, Vector2 pos)
    {
        Vector2 uiPos = Camera.main.WorldToScreenPoint(pos);
        scorePopup.gameObject.GetComponent<TextMeshProUGUI>().text = score.ToString();
        Transform cloneScore = Instantiate(scorePopup, uiPos, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
    }
}
