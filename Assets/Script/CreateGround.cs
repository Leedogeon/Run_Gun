using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CreateGround : MonoBehaviour
{
    public Transform Player;

    private float NextZ = 50f;
    private float SpawnCount = 0f;
    public List<GameObject> Ground = new List<GameObject>();
    private float MaxGorund = 0;

    private void Start()
    {
        GameObject StartBase = Ground[0];
        NewGround();
    }
    // Update is called once per frame
    void Update()
    {
        if(Player.position.z > NextZ*SpawnCount)
        {
            SpawnCount++;
            GameObject StartBase = Ground[0];
            NewGround();
        }
    }

    GameObject SelectGround()
    {
        System.Random random = new System.Random();
        int randomIndex = random.Next(0, Ground.Count);
        return Ground[randomIndex];
    }
    void NewGround()
    {
        GameObject NewGround = SelectGround();
        Instantiate(NewGround, new Vector3(0, 0, NextZ * SpawnCount * 2 + 30f), Quaternion.identity);
    }
}
