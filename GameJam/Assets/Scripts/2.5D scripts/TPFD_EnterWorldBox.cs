using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPFD_EnterWorldBox : MonoBehaviour
{
    [Header("World Checks")]
    public bool isForest;
    public bool isWater;
    public bool isMine;
    public bool isMainMap;

    [Header("Spawnpoints")]
    public Transform ForestSpawnpoint;
    public Transform WaterSpawnpoint;
    public Transform MineSpawnpoint;
    public Transform MainMapSpawnpoint;

    [Header("Other Data")]
    public GameObject Player;

    private void Awake()
    {
        ForestSpawnpoint = GameObject.Find("ForestSpawnpoint").transform;    
        WaterSpawnpoint = GameObject.Find("WaterSpawnpoint").transform;
        MineSpawnpoint = GameObject.Find("MineSpawnpoint").transform;
        MainMapSpawnpoint = GameObject.Find("MainSpawnpoint").transform;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void EnterLevel()
    {
        this.gameObject.SetActive(true);
        if (isForest)
        {
            Time.timeScale = 1f;
            Player.GetComponent<TPFD_PlayerController>().isTalking = false;
            StartCoroutine(Player.GetComponent<TPFD_Playground>().LoadTP(ForestSpawnpoint.transform));
        }
        else if (isWater)
        {
            Time.timeScale = 1f;
            Player.GetComponent<TPFD_PlayerController>().isTalking = false;
            StartCoroutine(Player.GetComponent<TPFD_Playground>().LoadTP(WaterSpawnpoint.transform));
        }
        else if (isMine)
        {
            Time.timeScale = 1f;
            Player.GetComponent<TPFD_PlayerController>().isTalking = false;
            StartCoroutine(Player.GetComponent<TPFD_Playground>().LoadTP(MineSpawnpoint.transform));
        }
        else if(isMainMap)
        {
            Time.timeScale = 1f;
            Player.GetComponent<TPFD_PlayerController>().isTalking = false;
            StartCoroutine(Player.GetComponent<TPFD_Playground>().LoadTP(MainMapSpawnpoint.transform));
        }
        else
        {
            Time.timeScale = 1f;
            Player.GetComponent<TPFD_PlayerController>().isTalking = false;
            Debug.Log("Error, cannot TP to none.");
        }

    }

    public void CloseBox()
    {
        Debug.Log("Closing Box");
        Time.timeScale = 1f;
        Player.GetComponent<TPFD_PlayerController>().isTalking = false;
        gameObject.SetActive(false);
    }
}
