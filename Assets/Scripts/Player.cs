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
    public bool beetleBuff = false;
  
    private int points = 0;
    private int flyScore = 3;
    private int beeScore = 1;
    private int butterflyScore = 10;
    private int ladybugScore = 5;
    private int stagBeetleScore = 1;
    private int queenBeeScore = 50;
    private int queenFlyScore = 5;
  
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
                    if(hit.collider.CompareTag("Fly") || hit.collider.CompareTag("StagBeetle") || hit.collider.CompareTag("Bee") || hit.collider.CompareTag("Butterfly") || hit.collider.CompareTag("Ladybug") || hit.collider.CompareTag("QueenFly") || hit.collider.CompareTag("QueenBee"))
                    {
                        StartCoroutine(PlayAudio());
                        UpdateStats(hit.collider.tag);
                    }

                    if (hit.collider.CompareTag("StagBeetle"))
                    {
                        if (!beetleBuff)
                        {
                            gm.GetComponent<Flash>().StartFlash();
                            beetleBuff = true;
                        }
                    }

                    if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[0].transform.position) < 0.01f)
                    {
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingBottom1");
                        Eat(hit.transform.tag);
                        StartCoroutine(CancelBottomAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[1].transform.position) < 0.01f)
                    {
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingBottom2");
                        Eat(hit.transform.tag);
                        StartCoroutine(CancelBottomAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[2].transform.position) < 0.01f)
                    {
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingBottom3");
                        Eat(hit.transform.tag);
                        StartCoroutine(CancelBottomAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[3].transform.position) < 0.01f)
                    {
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingMiddle1");
                        Eat(hit.transform.tag);
                        StartCoroutine(CancelMiddleAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[4].transform.position) < 0.01f)
                    {
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingMiddle2");
                        Eat(hit.transform.tag);
                        StartCoroutine(CancelMiddleAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[5].transform.position) < 0.01f)
                    {
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingMiddle3");
                        Eat(hit.transform.tag);
                        StartCoroutine(CancelMiddleAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[6].transform.position) < 0.01f)
                    {
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingTop1");
                        Eat(hit.transform.tag);
                        StartCoroutine(CancelTopAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[7].transform.position) < 0.01f)
                    {
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingTop2");
                        Eat(hit.transform.tag);
                        StartCoroutine(CancelTopAttack(hit.transform.gameObject.tag));
                    }
                    else if (Vector2.Distance(hit.transform.position, spawner.GetComponent<Spawner>().slotArr[8].transform.position) < 0.01f)
                    {
                        hit.transform.gameObject.GetComponent<Enemy>().canDie = false;
                        anim.SetTrigger("AttackingTop3");
                        Eat(hit.transform.tag);
                        StartCoroutine(CancelTopAttack(hit.transform.gameObject.tag));
                    }
                }
            }
        }
    }

    private void Eat(string bug)
    {
        switch (bug)
        {
            case "Fly":
                if (beetleBuff)
                {
                    Points += (flyScore * 2);
                }
                else
                {
                    Points += flyScore;
                }
                pointsText.text = Points.ToString();
                gm.IncreaseSlider(0.03f);
                break;
            case "Bee":
                if (beetleBuff)
                {
                    Points += (beeScore * 2);
                }
                else
                {
                    Points += flyScore;
                }
                pointsText.text = Points.ToString();
                break;
            case "Butterfly":
                if (beetleBuff)
                {
                    Points += (butterflyScore * 2);
                }
                else
                {
                    Points += butterflyScore;
                }
                pointsText.text = Points.ToString();
                gm.IncreaseSlider(0.10f);
                break;
            case "QueenBee":
                spawner.CancelSpawn();
                if (beetleBuff)
                {
                    Points += (queenBeeScore * 2);
                }
                else
                {
                    Points += queenBeeScore;
                }
                pointsText.text = Points.ToString();
                spawner.FillEmptySlotsWithBees();
                break;
            case "QueenFly":
                spawner.CancelSpawn();
                if (beetleBuff)
                {
                    Points += (queenFlyScore * 2);
                }
                else
                {
                    Points += queenFlyScore;
                }
                pointsText.text = Points.ToString();
                spawner.FillEmptySlotsWithFlies();
                break;
            case "Ladybug":
                if (beetleBuff)
                {
                    Points += (ladybugScore * 2);
                }
                else
                {
                    Points += ladybugScore;
                }
                pointsText.text = Points.ToString();
                gm.IncreaseSlider(1f);
                break;
            case "StagBeetle":
                if (beetleBuff)
                {
                    Points += (stagBeetleScore * 2);
                }
                else
                {
                    Points += stagBeetleScore;
                }
                pointsText.text = Points.ToString();
                gm.IncreaseSlider(0.05f);
                break;
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
            case "StagBeetle":
                gm.stagBeetlesEaten += 1;
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
            yield return new WaitForSeconds(0.2f);
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
            yield return new WaitForSeconds(0.3f);
            anim.SetTrigger("Idle");
            canAttack = true;
        }

        yield return null;
    }

    IEnumerator CancelTopAttack(string tag)
    {
        if (tag == "Bee" || tag == "QueenBee")
        {
            StartCoroutine(Stun(0.45f));          
        }
        else
        {
            canAttack = false;
            yield return new WaitForSeconds(0.4f);
            anim.SetTrigger("Idle");
            canAttack = true;
        }

        yield return null;
    }

    IEnumerator Stun(float time)
    {
        canAttack = false;

        yield return new WaitForSeconds(time);

        anim.SetBool("Stunned", true);

        yield return new WaitForSeconds(1.5f);

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
        if (collision.gameObject.CompareTag("Fly") || collision.gameObject.CompareTag("Butterfly") || collision.gameObject.CompareTag("Bee") || collision.gameObject.CompareTag("QueenBee") || collision.gameObject.CompareTag("QueenFly") || collision.gameObject.CompareTag("Ladybug") || collision.gameObject.CompareTag("StagBeetle"))
        {
            collision.gameObject.GetComponent<Enemy>().Die();       
        }        
    }
}
