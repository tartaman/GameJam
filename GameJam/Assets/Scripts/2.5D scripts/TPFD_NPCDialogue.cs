using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TPFD_NPCDialogue : MonoBehaviour
{
    public GameObject TPFD_NPC;
    public GameObject Player;
    private Rigidbody Player_RB;
    private TPFD_PlayerController Player_Controller;

    public GameObject JoinWorldPanel;
    
    public GameObject DialogueBox;
    private TPFD_Dialogue TPFD_Dialogue;
    public TextMeshPro TextAboveNPC;
    public float Distance;

    public bool isInRange;

    public GameObject GameManager;
    public TPFD_GameManager TPFD_GameManager;

    [Header("Lines if nothing completed")]
    public string[] lines;

    [Header("Lines if level 1 or 2 is completed")]
    public string[] lines2;

    [Header("Lines if boss is completed")]
    public string[] lines3;

    public float textSpeed;

    [Header("NPC Checks")]
    public bool isForestNPC;
    public bool isWaterNPC;
    public bool isMineNPC;
    public bool isYellowHouseNPC;
    public bool isHospitalHouseNPC;
    public bool isGreenHouseNPC;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        TPFD_Dialogue = DialogueBox.GetComponent<TPFD_Dialogue>();
        Player_RB =  Player.GetComponent<Rigidbody>();
        Player_Controller = Player.GetComponent<TPFD_PlayerController>();
        TPFD_GameManager = GameManager.GetComponent<TPFD_GameManager>();

    }

    void Update()
    {
        isInRange = Vector3.Distance(TPFD_NPC.transform.position, Player.transform.position) <= Distance;
        //Debug.Log($"Distance between player and NPC: {Vector3.Distance(TPFD_NPC.transform.position, Player.transform.position)}");

        DoTheChecks();

        if (isInRange)
        {
            TextAboveNPC.text = $"Press {Player_Controller.Talk} to talk.";
            TextAboveNPC.alpha = 100;
        }
        else
        {
            TextAboveNPC.alpha = 0;
        }

        if (isInRange && Input.GetKeyDown(Player_Controller.Talk) && !JoinWorldPanel.activeSelf)
        {
            Talk();
        }
        
    }

    void Talk()
    {
        DialogueBox.SetActive(true);

        TPFD_Dialogue.textSpeed = textSpeed;

        Player_RB.velocity = Vector3.zero;
    }

    void EnterWorldBox(string WorldName)
    {
        Time.timeScale = 0f;
        JoinWorldPanel.SetActive(true);

        Player.GetComponent<TPFD_PlayerController>().isTalking = true;

        GameObject.Find("Enter world").GetComponent<TextMeshProUGUI>().text = $"Would you like to enter '{WorldName}'?";
    }

    void DoTheChecks()
    {
        if (JoinWorldPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }

        if (!JoinWorldPanel.activeSelf)
        {
            Time.timeScale = 1f;

            if (isForestNPC && !TPFD_GameManager.isForestLevel1Completed && !TPFD_GameManager.isForestLevel2Completed && isInRange)
            {
                TPFD_Dialogue.lines = lines;

                if (TPFD_Dialogue.HasFinishedTalking)
                {
                    TPFD_Dialogue.HasFinishedTalking = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isForest = true;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isWater = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isMine = false;
                    EnterWorldBox("The Forest");
                }
            }

            if (isForestNPC && (TPFD_GameManager.isForestLevel1Completed || TPFD_GameManager.isForestLevel2Completed) && isInRange)
            {
                TPFD_Dialogue.lines = lines2;
                if (TPFD_Dialogue.HasFinishedTalking)
                {
                    TPFD_Dialogue.HasFinishedTalking = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isForest = true;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isWater = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isMine = false;
                    EnterWorldBox("The Forest");
                }
            }

            if (isForestNPC && TPFD_GameManager.isForestBossCompleted)
            {
                TPFD_Dialogue.lines = lines3;
                if (TPFD_Dialogue.HasFinishedTalking)
                {
                    TPFD_Dialogue.HasFinishedTalking = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isForest = true;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isWater = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isMine = false;
                    EnterWorldBox("The Forest");
                }
            }


            if (isWaterNPC && !TPFD_GameManager.isWaterLevel1Completed && !TPFD_GameManager.isWaterLevel2Completed && isInRange)
            {
                TPFD_Dialogue.lines = lines;
                if (TPFD_Dialogue.HasFinishedTalking)
                {
                    TPFD_Dialogue.HasFinishedTalking = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isForest = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isWater = true;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isMine = false;
                    EnterWorldBox("The River");
                }
            }

            if (isWaterNPC && (TPFD_GameManager.isWaterLevel1Completed || TPFD_GameManager.isWaterLevel2Completed) && isInRange)
            {
                TPFD_Dialogue.lines = lines2;
                if (TPFD_Dialogue.HasFinishedTalking)
                {
                    TPFD_Dialogue.HasFinishedTalking = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isForest = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isWater = true;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isMine = false;
                    EnterWorldBox("The River");
                }
            }

            if (isWaterNPC && TPFD_GameManager.isWaterBossCompleted && isInRange)
            {
                TPFD_Dialogue.lines = lines3;
                if (TPFD_Dialogue.HasFinishedTalking)
                {
                    TPFD_Dialogue.HasFinishedTalking = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isForest = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isWater = true;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isMine = false;
                    EnterWorldBox("The River");
                }
            }

            if (isMineNPC && !TPFD_GameManager.isMineLevel1Completed && !TPFD_GameManager.isMineLevel2Completed && isInRange)
            {
                TPFD_Dialogue.lines = lines;
                if (TPFD_Dialogue.HasFinishedTalking)
                {
                    TPFD_Dialogue.HasFinishedTalking = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isForest = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isWater = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isMine = true;
                    EnterWorldBox("The Mine");
                }
            }

            if (isMineNPC && (TPFD_GameManager.isMineLevel1Completed || TPFD_GameManager.isMineLevel2Completed) && isInRange)
            {
                TPFD_Dialogue.lines = lines2;
                if (TPFD_Dialogue.HasFinishedTalking)
                {
                    TPFD_Dialogue.HasFinishedTalking = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isForest = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isWater = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isMine = true;
                    EnterWorldBox("The Mine");
                }
            }

            if (isMineNPC && TPFD_GameManager.isMineBossCompleted && isInRange)
            {
                TPFD_Dialogue.lines = lines3;
                if (TPFD_Dialogue.HasFinishedTalking)
                {
                    TPFD_Dialogue.HasFinishedTalking = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isForest = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isWater = false;
                    JoinWorldPanel.GetComponent<TPFD_EnterWorldBox>().isMine = true;
                    EnterWorldBox("The Mine");
                }
            }

            if (isGreenHouseNPC && (!TPFD_GameManager.isForestLevel1Completed && !TPFD_GameManager.isForestLevel2Completed && !TPFD_GameManager.isWaterLevel1Completed && !TPFD_GameManager.isWaterLevel2Completed && !TPFD_GameManager.isMineLevel1Completed && !TPFD_GameManager.isMineLevel2Completed) && isInRange)
            {
                TPFD_Dialogue.lines = lines;
            }

            if (isGreenHouseNPC && (TPFD_GameManager.isForestLevel1Completed || TPFD_GameManager.isForestLevel2Completed || TPFD_GameManager.isWaterLevel1Completed || TPFD_GameManager.isWaterLevel2Completed || TPFD_GameManager.isMineLevel1Completed || TPFD_GameManager.isMineLevel2Completed) && isInRange)
            {
                TPFD_Dialogue.lines = lines2;
            }

            if (isGreenHouseNPC && (TPFD_GameManager.isForestBossCompleted || TPFD_GameManager.isWaterBossCompleted || TPFD_GameManager.isMineBossCompleted) && isInRange)
            {
                TPFD_Dialogue.lines = lines3;
            }

            if (isYellowHouseNPC && (!TPFD_GameManager.isForestLevel1Completed && !TPFD_GameManager.isForestLevel2Completed && !TPFD_GameManager.isWaterLevel1Completed && !TPFD_GameManager.isWaterLevel2Completed && !TPFD_GameManager.isMineLevel1Completed && !TPFD_GameManager.isMineLevel2Completed) && isInRange)
            {
                TPFD_Dialogue.lines = lines;
            }

            if (isYellowHouseNPC && (TPFD_GameManager.isForestLevel1Completed || TPFD_GameManager.isForestLevel2Completed || TPFD_GameManager.isWaterLevel1Completed || TPFD_GameManager.isWaterLevel2Completed || TPFD_GameManager.isMineLevel1Completed || TPFD_GameManager.isMineLevel2Completed) && isInRange)
            {
                TPFD_Dialogue.lines = lines2;
            }

            if (isYellowHouseNPC && (TPFD_GameManager.isForestBossCompleted || TPFD_GameManager.isWaterBossCompleted || TPFD_GameManager.isMineBossCompleted) && isInRange)
            {
                TPFD_Dialogue.lines = lines3;
            }

            if (isHospitalHouseNPC && (!TPFD_GameManager.isForestLevel1Completed && !TPFD_GameManager.isForestLevel2Completed && !TPFD_GameManager.isWaterLevel1Completed && !TPFD_GameManager.isWaterLevel2Completed && !TPFD_GameManager.isMineLevel1Completed && !TPFD_GameManager.isMineLevel2Completed) && isInRange)
            {
                TPFD_Dialogue.lines = lines;
            }

            if (isHospitalHouseNPC && (TPFD_GameManager.isForestLevel1Completed || TPFD_GameManager.isForestLevel2Completed || TPFD_GameManager.isWaterLevel1Completed || TPFD_GameManager.isWaterLevel2Completed || TPFD_GameManager.isMineLevel1Completed || TPFD_GameManager.isMineLevel2Completed) && isInRange)
            {
                TPFD_Dialogue.lines = lines2;
            }

            if (isHospitalHouseNPC && (TPFD_GameManager.isForestBossCompleted || TPFD_GameManager.isWaterBossCompleted || TPFD_GameManager.isMineBossCompleted) && isInRange)
            {
                TPFD_Dialogue.lines = lines3;
            }
        }

       
    }
}
