using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using System;

public class LevelPartSpawner : MonoBehaviour
{
    public delegate void OnSpanwLevelPart();
    public static event OnSpanwLevelPart onSpawnClouds;


    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 10f;

    [SerializeField] private PlayerCharacter2D player;

    private Vector3 lastEndPosition;

    private void Start()
    {
        lastEndPosition = new Vector3(player.transform.position.x, player.transform.position.y + 10f);
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            string tag = "a";
            GameObject newLevelPart = LevelPartPooler.Instance.SpawnFromPool(tag, lastEndPosition, Quaternion.identity);
            //onSpawnClouds.Invoke();
            lastEndPosition = new Vector3(newLevelPart.transform.position.x, newLevelPart.transform.position.y + 10f);
        }
    }
}


