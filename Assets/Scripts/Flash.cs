using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Flash : MonoBehaviour
{
    public Image img;
    private Player p;
    public GameObject x2;
    private float timer = 0;

    private void Start()
    {
        p = gameObject.GetComponent<GameMaster>().player;
    }

    public void StartFlash()
    {
        InvokeRepeating("FlashScreen", 0, 0.25f);
        x2.gameObject.SetActive(true);
    }

    private void FlashScreen()
    {
        timer += 0.25f;
        if(timer > 4)
        {
            CancelInvoke();
            x2.SetActive(false);
            timer = 0;
            img.enabled = false;
            p.beetleBuff = false;
            return;
        }

        if (img.enabled)
        {
            img.enabled = false;
        }
        else
        {
            img.enabled = true;
        }
    }
}
