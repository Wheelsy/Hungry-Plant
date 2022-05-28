using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    private bool alreadyClicked = false;
    public GameObject donateButton;

    public void DonateExplain()
    {
        if (alreadyClicked == false)
        {
            donateButton.SetActive(true);
            alreadyClicked = true;
        }
        else
        {
            donateButton.SetActive(false);
            alreadyClicked = false;
        }
    }

    public void DontateConfirmed()
    {
        Application.OpenURL("https://www.paypal.com/donate?business=UXUUM7DDB2FYY&currency_code=AUD");
    }
}
