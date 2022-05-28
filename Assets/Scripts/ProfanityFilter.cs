using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfanityFilter : MonoBehaviour
{
    public TMP_InputField inFieldText;
    private string myString;
    [SerializeField]
    private string [] profanities;

    public void ChangeString(string stringIn)
    {
        myString = stringIn;
        ProfanityParser();
    }

    void ProfanityParser()
    {
        for(int i = 0; i < profanities.Length; i++)
        {
            if (myString.ToLower().Contains(profanities[i]))
            {
                for(int j = 0; j < myString.Length; j++)
                {
                    if(myString.ToLower()[j] == profanities[i][0])
                    {
                        string temp = myString.Substring(j, profanities[i].Length);

                        if(temp.ToLower() == profanities[i])
                        {
                            myString = myString.Remove(j, profanities[i].Length);

                            if(myString != null)
                            {
                                inFieldText.text = myString.ToString();
                            }
                            else
                            {
                                inFieldText.text = "";
                            }

                            return;
                        }
                    }
                }
            }
        }
    }
}
