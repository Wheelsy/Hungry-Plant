using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System.Drawing;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Spawner spawner;
    public TextMeshProUGUI pointsText;
    public GameObject energyBar;
    public GameMaster gm;
    public AudioSource chompAudio;
    public bool canAttack = true;
  
    private int points = 0;
  

    public int Points { get => points; set => points = value; }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;  i < Input.touches.Length; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began && canAttack == true)
            {
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero, 10f);

                if (hit != false && hit.collider != null)
                {
                    if(hit.collider.CompareTag("Fly") || hit.collider.CompareTag("Bee") || hit.collider.CompareTag("Butterfly") || hit.collider.CompareTag("Ladybug") || hit.collider.CompareTag("QueenFly") || hit.collider.CompareTag("QueenBee"))
                    {
                        StartCoroutine(PlayAudio());
                        UpdateStats(hit.collider.tag);
                    }

                    if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[0].transform.position) < 0.01f)
                    {
                        Debug.Log("slot1");
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingBottom1");
                        
                        if (hit.transform.gameObject.CompareTag("Fly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.075f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Butterfly"))
                        {
                            Points += 15;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.15f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Ladybug"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(1f);
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenBee"))
                        {
                            Points += 25;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithBees();
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenFly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithFlies();
                        }
                        StartCoroutine(CancelBottomAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[1].transform.position) < 0.01f)
                    {
                        Debug.Log("slot2");
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingBottom2");

                        if (hit.transform.gameObject.CompareTag("Fly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.075f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Butterfly"))
                        {
                            Points += 15;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.15f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Ladybug"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(1f);
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenBee"))
                        {
                            Points += 25;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithBees();
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenFly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithFlies();
                        }
                        StartCoroutine(CancelBottomAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[2].transform.position) < 0.01f)
                    {
                        Debug.Log("slot3");
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingBottom3");

                        if (hit.transform.gameObject.CompareTag("Fly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.075f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Butterfly"))
                        {
                            Points += 15;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.15f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Ladybug"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(1f);
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenBee"))
                        {
                            Points += 25;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithBees();
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenFly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithFlies();
                        }
                        StartCoroutine(CancelBottomAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[3].transform.position) < 0.01f)
                    {
                        Debug.Log("slot4");
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingMiddle1");

                        if (hit.transform.gameObject.CompareTag("Fly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.075f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Butterfly"))
                        {
                            Points += 15;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.15f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Ladybug"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(1f);
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenBee"))
                        {
                            Points += 25;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithBees();
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenFly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithFlies();
                        }
                        StartCoroutine(CancelMiddleAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[4].transform.position) < 0.01f)
                    {
                        Debug.Log("slot5");
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingMiddle2");

                        if (hit.transform.gameObject.CompareTag("Fly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.075f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Butterfly"))
                        {
                            Points += 15;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.15f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Ladybug"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(1f);
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenBee"))
                        {
                            Points += 25;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithBees();
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenFly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithFlies();
                        }
                        StartCoroutine(CancelMiddleAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[5].transform.position) < 0.01f)
                    {
                        Debug.Log("slot6");
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingMiddle3");                      

                        if (hit.transform.gameObject.CompareTag("Fly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.075f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Butterfly"))
                        {
                            Points += 15;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.15f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Ladybug"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(1f);
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenBee"))
                        {
                            Points += 25;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithBees();
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenFly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithFlies();
                        }
                        StartCoroutine(CancelMiddleAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[6].transform.position) < 0.01f)
                    {
                        Debug.Log("slot7");
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingTop1");

                        if (hit.transform.gameObject.CompareTag("Fly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.075f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Butterfly"))
                        {
                            Points += 15;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.15f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Ladybug"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(1f);
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenBee"))
                        {
                            Points += 25;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithBees();
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenFly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithFlies();
                        }
                        StartCoroutine(CancelTopAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[7].transform.position) < 0.01f)
                    {
                        Debug.Log("slot8");
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingTop2");

                        if (hit.transform.gameObject.CompareTag("Fly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.075f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Butterfly"))
                        {
                            Points += 15;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.15f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Ladybug"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(1f);
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenBee"))
                        {
                            Points += 25;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithBees();
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenFly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithFlies();
                        }
                        StartCoroutine(CancelTopAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[8].transform.position) < 0.01f)
                    {
                        Debug.Log("slot9");
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingTop3");                

                        if (hit.transform.gameObject.CompareTag("Fly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.075f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Butterfly"))
                        {
                            Points += 15;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(0.15f);
                        }
                        else if (hit.transform.gameObject.CompareTag("Ladybug"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            gm.IncreaseSlider(1f);
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenBee"))
                        {
                            Points += 25;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithBees();
                        }
                        else if (hit.transform.gameObject.CompareTag("QueenFly"))
                        {
                            Points += 5;
                            pointsText.text = Points.ToString();
                            spawner.FillEmptySlotsWithFlies();
                        }
                        StartCoroutine(CancelTopAttack(hit.transform.gameObject.tag));
                    }
                }
            }
        }
    }

    void UpdateStats(string tag)
    {
        switch (tag)
        {
            case "Fly":
                gm.flysEaten += 1;
                break;
            case "Bee":
                gm.beesEaten += 1;
                break;
            case "Butterfly":
                gm.butterflysEaten += 1;
                break;
            case "QueenBee":
                gm.queenBeesEaten += 1;
                break;
            case "QueenFly":
                gm.queenFlysEaten += 1;
                break;
            case "Ladybug":
                gm.ladybugsEaten += 1;
                break;
        }
    }

    IEnumerator CancelBottomAttack(string tag)
    {
        if (tag == "Bee" || tag == "QueenBee")
        {
            canAttack = false;
            StartCoroutine(Stun(0.4f));
        }
        else
        {
            canAttack = false;
            yield return new WaitForSeconds(0.35f);
            anim.SetTrigger("Idle");
            canAttack = true;
        }
        
        yield return null;
    }

    IEnumerator CancelMiddleAttack(string tag)
    {
        if (tag == "Bee" || tag == "QueenBee")
        {
            StartCoroutine(Stun(0.5f));
        }
        else
        {
            canAttack = false;
            yield return new WaitForSeconds(0.45f);
            anim.SetTrigger("Idle");
            canAttack = true;
        }

        yield return null;
    }

    IEnumerator CancelTopAttack(string tag)
    {
        if (tag == "Bee" || tag == "QueenBee")
        {
            StartCoroutine(Stun(0.6f));          
        }
        else
        {
            canAttack = false;
            yield return new WaitForSeconds(0.55f);
            anim.SetTrigger("Idle");
            canAttack = true;
        }

        yield return null;
    }

    IEnumerator Stun(float time)
    {
        Debug.Log("stunned");
        canAttack = false;

        yield return new WaitForSeconds(time);

        anim.SetBool("Stunned", true);

        yield return new WaitForSeconds(2f);

        canAttack = true;
        anim.SetBool("Stunned", false);
        anim.SetTrigger("Idle");
    }

    IEnumerator PlayAudio()
    {
        chompAudio.PlayOneShot(chompAudio.clip);

        yield return new WaitForSeconds(0.51f);

        chompAudio.Stop();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.CompareTag("Fly") || collision.gameObject.CompareTag("Butterfly") || collision.gameObject.CompareTag("Bee") || collision.gameObject.CompareTag("QueenBee") || collision.gameObject.CompareTag("QueenFly") || collision.gameObject.CompareTag("Ladybug"))
        {
            collision.gameObject.GetComponent<Enemy>().Die();       
        }        
    }
}
