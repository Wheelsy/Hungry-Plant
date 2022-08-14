using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isFull = false;
    private float timer = 0;
    private GameMaster gm;

    private void Awake()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    private void Update()
    {
        if (isFull)
        {
            timer += Time.deltaTime;
            if (gm.SpeedStage == 0)
            {
                if (timer >= 6)
                {
                    isFull = false;
                    timer = 0;
                }
            }
            else if (gm.SpeedStage == 1)
            {
                if (timer >= 5.5f)
                {
                    isFull = false;
                    timer = 0;
                }
            }
            else if (gm.SpeedStage == 2)
            {
                if (timer >= 5f)
                {
                    isFull = false;
                    timer = 0;
                }
            }
            else if (gm.SpeedStage == 3)
            {
                if (timer >= 4.5)
                {
                    isFull = false;
                    timer = 0;
                }
            }
            else if (gm.SpeedStage == 4)
            {
                if (timer >= 4f)
                {
                    isFull = false;
                    timer = 0;
                }
            }
            else if (gm.SpeedStage == 5)
            {
                if (timer >= 3.5f)
                {
                    isFull = false;
                    timer = 0;
                }
            }
            else if (gm.SpeedStage == 6)
            {
                if (timer >= 3)
                {
                    isFull = false;
                    timer = 0;
                }
            }
        }
        else
        {
            timer = 0;
        }
    }
}
