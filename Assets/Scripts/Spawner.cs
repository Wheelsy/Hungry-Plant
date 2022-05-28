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
    public Transform queenBeePrefab;
    public Transform queenFlyPrefab;
    //Array to keep track of spawned enemies
    public GameObject[] slotArr = new GameObject[9];
    public float spawnDelay;
    public float spawnTimer;

    private int previousSpawn = 10;

    void Awake()    
    {
        InvokeRepeating("Spawn", spawnTimer, spawnDelay);     
    }

    public void RestartSpawning()
    {
        InvokeRepeating("Spawn", spawnTimer, spawnDelay);
    }

    public void Spawn()
    {
        int slotPos = Random.Range(0, 9);

        if (SlotCheck(slotPos) == false && previousSpawn != slotPos)
        {
            previousSpawn = slotPos;

            int spawnChance = Random.Range(1, 100);
            
            if (spawnChance >= 24)
            {
                Transform newFly = Instantiate(flyPrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newFly.GetComponent<Enemy>().slotPos = slotPos;
            }
            else if (spawnChance >= 14 && spawnChance < 24)
            {
                Transform newBee = Instantiate(beePrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newBee.GetComponent<Enemy>().slotPos = slotPos;
            }
            else if(spawnChance >= 8 && spawnChance < 14)
            {
                Transform newButterfly = Instantiate(butterflyPrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newButterfly.GetComponent<Enemy>().slotPos = slotPos;
            }
            else if (spawnChance >= 4 && spawnChance < 8)
            {
                Transform newQueenBee = Instantiate(queenBeePrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newQueenBee.GetComponent<Enemy>().slotPos = slotPos;
            }
            else if (spawnChance >= 2 && spawnChance < 4)
            {
                Transform newQueenFly = Instantiate(queenFlyPrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newQueenFly.GetComponent<Enemy>().slotPos = slotPos;
            }
            else if (spawnChance == 1)
            {
                Transform newLadybug = Instantiate(ladybugPrefab, slotArr[slotPos].transform.position, Quaternion.identity);
                newLadybug.GetComponent<Enemy>().slotPos = slotPos;
            }

            slotArr[slotPos].GetComponent<Slot>().isFull = true;
        }              
    }

    public void FillEmptySlotsWithBees()
    {
        for(int i = 0; i < slotArr.Length; i++)
        {
            if (SlotCheck(i) == false && previousSpawn != i)
            {
                slotArr[i].GetComponent<Slot>().isFull = true;
                previousSpawn = i;
                Transform newBee = Instantiate(beePrefab, slotArr[i].transform.position, Quaternion.identity);
                newBee.GetComponent<Enemy>().slotPos = i;
            }
        }
    }

    public void FillEmptySlotsWithFlies()
    {
        for (int i = 0; i < slotArr.Length; i++)
        {
            if (SlotCheck(i) == false && previousSpawn != i)
            {
                slotArr[i].GetComponent<Slot>().isFull = true;
                previousSpawn = i;
                Transform newFly = Instantiate(flyPrefab, slotArr[i].transform.position, Quaternion.identity);
                newFly.GetComponent<Enemy>().slotPos = i;
            }
        }
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

    void OnDisable()
    {
        CancelInvoke();
    }
}

