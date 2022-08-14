using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //What we want to spawn
    public Transform flyPrefab;
    public Transform butterflyPrefab;
    public Transform beePrefab;
    public Transform ladybugPrefab;
    public Transform stagBeetlePrefab;
    public Transform queenBeePrefab;
    public Transform queenFlyPrefab;
    //Array to keep track of spawned enemies
    public GameObject[] slotArr = new GameObject[9];
    public float spawnTimer;

    private float spawnRepeatRate = 0.4f;
    private int previousSpawn = 10;
    private bool times2Spawn = false;

    public float SpawnRepeatRate { get => spawnRepeatRate; set => spawnRepeatRate = value; }
    public bool Times2Spawn { get => times2Spawn; set => times2Spawn = value; }

    private void ResetVariables()
    {
          spawnRepeatRate = 0.4f;
          previousSpawn = 10;
    }

    public void Spawn()
    {
        int slotPos = Random.Range(0, 9);

        if (SlotCheck(slotPos) == false && previousSpawn != slotPos)
        {
            previousSpawn = slotPos;

            int spawnChance = Random.Range(1, 100);
            
            if (spawnChance >= 30)
            {
                Transform newFly = Instantiate(flyPrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newFly.GetComponent<Enemy>().slotPos = slotPos;
            }
            else if (spawnChance >= 20 && spawnChance < 30)
            {
                Transform newBee = Instantiate(beePrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newBee.GetComponent<Enemy>().slotPos = slotPos;
            }
            else if(spawnChance >= 14 && spawnChance < 20)
            {
                Transform newButterfly = Instantiate(butterflyPrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newButterfly.GetComponent<Enemy>().slotPos = slotPos;
            }
            else if (spawnChance >= 10 && spawnChance < 14)
            {
                Transform newQueenBee = Instantiate(queenBeePrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newQueenBee.GetComponent<Enemy>().slotPos = slotPos;
            }
            else if (spawnChance >= 6 && spawnChance < 10)
            {
                Transform newQueenFly = Instantiate(queenFlyPrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newQueenFly.GetComponent<Enemy>().slotPos = slotPos;
            }
            else if (spawnChance >= 4 && spawnChance < 6)
            {
                Transform newStagBeetle = Instantiate(stagBeetlePrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newStagBeetle.GetComponent<Enemy>().slotPos = slotPos;
            }
            else if (spawnChance == 1)
            {
                Transform newLadybug = Instantiate(ladybugPrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newLadybug.GetComponent<Enemy>().slotPos = slotPos;
            }
            slotArr[slotPos].GetComponent<Slot>().isFull = true;
        }   
    }

    public void ClearSlots()
    {
        Enemy[] enemies = FindObjectsOfType(typeof(Enemy)) as Enemy[];

        foreach(Enemy e in enemies)
        {
            Destroy(e);
        }

        for (int i = 0; i < slotArr.Length; i++)
        {
            slotArr[i].GetComponent<Slot>().isFull = false;
        }
    }

    public void FillEmptySlotsWithBees()
    {
        for(int i = 0; i < slotArr.Length; i++)
        {
            if (SlotCheck(i) == false )
            {
                slotArr[i].GetComponent<Slot>().isFull = true;
                previousSpawn = i;
                Transform newBee = Instantiate(beePrefab, slotArr[i].transform.position, Quaternion.identity);
                newBee.GetComponent<Enemy>().slotPos = i;
            }
        }
        RestartSpawn();
    }

    public void FillEmptySlotsWithFlies()
    {
        for (int i = 0; i < slotArr.Length; i++)
        {
            if (SlotCheck(i) == false)
            {
                slotArr[i].GetComponent<Slot>().isFull = true;
                previousSpawn = i;
                Transform newFly = Instantiate(flyPrefab, slotArr[i].transform.position, Quaternion.identity);
                newFly.GetComponent<Enemy>().slotPos = i;
            }
        }
        RestartSpawn();
    }

    private bool SlotCheck(int slot)
    {
        bool check = false;

        if (slotArr[slot].GetComponent<Slot>().isFull == true)
        {
            check = true;
        }

        return check;
    }

    public void CancelSpawn()
    {
        CancelInvoke();
    }

    void RestartSpawn()
    {
        InvokeRepeating("Spawn", spawnTimer, SpawnRepeatRate);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void OnEnable()
    {
        ClearSlots();
        if (GameMaster.playAgainClicked)
        {
            ResetVariables();
        }
        InvokeRepeating("Spawn", spawnTimer, SpawnRepeatRate);
    }
}

