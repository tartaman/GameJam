using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TPFD_LevelFlags : MonoBehaviour
{
    [Header("GameManager Reference")]
    private TPFD_GameManager gameManager;

    [Header("Enter Level Box")]
    public GameObject EnterLevelBox;
    public GameObject EnterLevelYesButton;
    public GameObject EnterLevelNoButton;

    [Header("Values for interaction")]
    public float distance;
    public bool isInRange;
    public GameObject Player;
    public TPFD_PlayerController playerController;
    public TextMeshPro textAboveFlag;

    [Header("Level Flag Assets")]
    public GameObject Flag;
    public GameObject LevelParticlesCompleted;
    public GameObject LevelParticlesNotCompleted;

    [Header("World")]
    public bool isForest;
    public bool isWater;
    public bool isMine;

    [Header("Level")]
    public bool isLevel1;
    public bool isLevel2;
    public bool isBoss;
    public bool isMainMap;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<TPFD_GameManager>();
        Player = GameObject.FindGameObjectWithTag("Player").gameObject;
        playerController = Player.GetComponent<TPFD_PlayerController>();

        Flag = this.transform.GetChild(0).gameObject;
        LevelParticlesNotCompleted = this.transform.GetChild(1).gameObject;
        LevelParticlesCompleted = this.transform.GetChild(2).gameObject;
        textAboveFlag = this.transform.GetChild(3).gameObject.GetComponent<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 0; i < this.transform.childCount; i++)
        //{
        //    Debug.Log($"Child {i} of {this.transform.gameObject.name}: {this.transform.GetChild(i).name}");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        CheckForLevel();
        CheckForRange();
    }

    void CheckForLevel()
    {
        /*
         Three states:
            1. Level cannot be completed because previous level hasn't been cleared.
            2. Level can be completed because previous level has been cleared.
            3. Level has been cleared.
         */

        if (isForest)
        {
            //1rst Level
                //2nd State
            if (isLevel1 && !gameManager.isForestLevel1Completed)
            {
                //Debug.Log($"Printing 2ndState particles {this.gameObject.name}");
                LevelParticlesNotCompleted.SetActive(true);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

                //3rd State
            if (isLevel1 && gameManager.isForestLevel1Completed)
            {
                //Debug.Log($"Printing 3rdState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(true);
                return;
            }

            //2nd Level
                //1rst State
            if (isLevel2 && !gameManager.isForestLevel1Completed)
            {
                //Debug.Log($"Printing 1rstState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

                //2nd State
            if (isLevel2 && gameManager.isForestLevel1Completed && !gameManager.isForestLevel2Completed)
            {
                //Debug.Log($"Printing 2ndState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(true);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

                //3rd state
            if (isLevel2 && gameManager.isForestLevel2Completed)
            {
                //Debug.Log($"Printing 3rdState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(true);
                return;
            }

            //Boss Level
                //1rst State
            if (isBoss && !gameManager.isForestLevel1Completed || !gameManager.isForestLevel2Completed)
            {
                //Debug.Log($"Printing 1rstState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

                //2nd State
            if (isBoss && gameManager.isForestLevel1Completed && gameManager.isForestLevel2Completed && !gameManager.isForestBossCompleted)
            {
                //Debug.Log($"Printing 2ndState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(true);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

                //3rd state
            if (isBoss && gameManager.isForestLevel1Completed && gameManager.isForestLevel2Completed && gameManager.isForestBossCompleted)
            {
                //Debug.Log($"Printing 3rdState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(true);
                return;
            }
        }

        if (isWater)
        {
            //1rst Level
            //2nd State
            if (isLevel1 && !gameManager.isWaterLevel1Completed)
            {
                //Debug.Log($"Printing 2ndState particles {this.gameObject.name}");
                LevelParticlesNotCompleted.SetActive(true);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

            //3rd State
            if (isLevel1 && gameManager.isWaterLevel1Completed)
            {
                //Debug.Log($"Printing 3rdState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(true);
                return;
            }

            //2nd Level
            //1rst State
            if (isLevel2 && !gameManager.isWaterLevel1Completed)
            {
                //Debug.Log($"Printing 1rstState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

            //2nd State
            if (isLevel2 && gameManager.isWaterLevel1Completed && !gameManager.isWaterLevel2Completed)
            {
                //Debug.Log($"Printing 2ndState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(true);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

            //3rd state
            if (isLevel2 && gameManager.isWaterLevel2Completed)
            {
                //Debug.Log($"Printing 3rdState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(true);
                return;
            }

            //Boss Level
            //1rst State
            if (isBoss && !gameManager.isWaterLevel1Completed || !gameManager.isWaterLevel2Completed)
            {
                //Debug.Log($"Printing 1rstState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

            //2nd State
            if (isBoss && gameManager.isWaterLevel1Completed && gameManager.isWaterLevel2Completed && !gameManager.isWaterBossCompleted)
            {
                //Debug.Log($"Printing 2ndState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(true);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

            //3rd state
            if (isBoss && gameManager.isWaterLevel1Completed && gameManager.isWaterLevel2Completed && gameManager.isWaterBossCompleted)
            {
                //Debug.Log($"Printing 3rdState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(true);
                return;
            }
        }

        if (isMine)
        {
            //1rst Level
            //2nd State
            if (isLevel1 && !gameManager.isMineLevel1Completed)
            {
                //Debug.Log($"Printing 2ndState particles {this.gameObject.name}");
                LevelParticlesNotCompleted.SetActive(true);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

            //3rd State
            if (isLevel1 && gameManager.isMineLevel1Completed)
            {
                //Debug.Log($"Printing 3rdState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(true);
                return;
            }

            //2nd Level
            //1rst State
            if (isLevel2 && !gameManager.isMineLevel1Completed)
            {
                //Debug.Log($"Printing 1rstState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

            //2nd State
            if (isLevel2 && gameManager.isMineLevel1Completed && !gameManager.isMineLevel2Completed)
            {
                //Debug.Log($"Printing 2ndState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(true);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

            //3rd state
            if (isLevel2 && gameManager.isMineLevel2Completed)
            {
                //Debug.Log($"Printing 3rdState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(true);
                return;
            }

            //Boss Level
            //1rst State
            if (isBoss && !gameManager.isMineLevel1Completed || !gameManager.isMineLevel2Completed)
            {
                //Debug.Log($"Printing 1rstState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

            //2nd State
            if (isBoss && gameManager.isMineLevel1Completed && gameManager.isMineLevel2Completed && !gameManager.isMineBossCompleted)
            {
                //Debug.Log($"Printing 2ndState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(true);
                LevelParticlesCompleted.SetActive(false);
                return;
            }

            //3rd state
            if (isBoss && gameManager.isMineLevel1Completed && gameManager.isMineLevel2Completed && gameManager.isMineBossCompleted)
            {
                //Debug.Log($"Printing 3rdState particles {this.gameObject.name}");

                LevelParticlesNotCompleted.SetActive(false);
                LevelParticlesCompleted.SetActive(true);
                return;
            }
        }
    }

    void CheckForRange()
    {
        isInRange = Vector3.Distance(this.transform.position, Player.transform.position) <= distance;
        //Debug.Log($"Distance from {this.gameObject.name} to Player: {Vector3.Distance(this.transform.position, Player.transform.position)}");

        if(isInRange)
        {
            textAboveFlag.text = $"Press {playerController.Talk} to interact.";

            if (isMainMap)
            {
                textAboveFlag.text = $"Press {playerController.Talk} to go back to main map.";
            }

            textAboveFlag.alpha = 100;
        }
        else
        {
            textAboveFlag.alpha = 0;
        }

        if (isInRange && Input.GetKeyDown(playerController.Talk))
        {
            AskToEnterLevel();
        }
    }

    void LoadLevel1()
    {
        SceneManager.LoadScene("Nivel1Carbon");
    }

    void EnterLevelDialogue(string WorldName, int MessageIndex)
    {
        string notAvailable = $"{WorldName} is not available yet...";
        string Available = $"Would you like to challenge {WorldName}?";

        if (MessageIndex >= EnterLevelBox.GetComponent<TPFD_EnterLevel>().messages.Length)
        {
            MessageIndex = 0;
        }

        EnterLevelBox.SetActive(true);

        Time.timeScale = 0f;

        Player.GetComponent<TPFD_PlayerController>().isTalking = true;

        if(WorldName.Contains("Mine") && isLevel1)
        {
            MessageIndex = 1;
            GameObject.Find("Enter level").GetComponent<TextMeshProUGUI>().text = Available;
            EnterLevelNoButton.SetActive(true);
            EnterLevelYesButton.transform.localPosition = new Vector3(800, -700);

            EnterLevelYesButton.GetComponent<Button>().onClick.AddListener(LoadLevel1);
        }
        else
        {
            MessageIndex = 0;
            GameObject.Find("Enter level").GetComponent<TextMeshProUGUI>().text = notAvailable;
            EnterLevelNoButton.SetActive(false);
            EnterLevelYesButton.transform.localPosition = new Vector3(-6, -706);
        }
        GameObject.Find("Subtext level").GetComponent<TextMeshProUGUI>().text = EnterLevelBox.GetComponent<TPFD_EnterLevel>().messages[MessageIndex];

    }

    void AskToEnterLevel()
    {

        if (isForest)
        {
            if (isLevel1 && LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("Entering Forest Level 1");

                EnterLevelDialogue("Forest Lv 1", 0);
            }

            if (isLevel1 && LevelParticlesCompleted.activeSelf)
            {
                Debug.Log("You have already cleared this level.");
            }

            if (isLevel2 && LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("Entering Forest Level 2");

                EnterLevelDialogue("Forest Lv 2", 0);
            }

            if (isLevel2 && !LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("You must clear Forest Level 1 before entering this level");

                EnterLevelDialogue("Forest Lv 2", 0);
            }

            if (isLevel2 && LevelParticlesCompleted.activeSelf)
            {
                Debug.Log("You have already cleared this level.");

                EnterLevelDialogue("Forest Lv 2", 0);
            }

            if (isBoss && LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("Entering Forest Boss");

                EnterLevelDialogue("Forest Boss", 0);
            }

            if (isBoss && !LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("You must clear Forest Level 1 and 2 before challenging the boss");

                EnterLevelDialogue("Forest Boss", 0);
            }

            if (isBoss && LevelParticlesCompleted.activeSelf)
            {
                Debug.Log("You have already cleared this world.");
            }
        }

        if (isWater)
        {
            if (isLevel1 && LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("Entering Water Level 1");

                EnterLevelDialogue("Water Lv 1", 0);
            }

            if (isLevel1 && LevelParticlesCompleted.activeSelf)
            {
                Debug.Log("You have already cleared this level.");

                EnterLevelDialogue("Water Lv 1", 0);
            }

            if (isLevel2 && LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("Entering Water Level 2");

                EnterLevelDialogue("Water Lv 2", 0);
            }

            if (isLevel2 && !LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("You must clear Water Level 1 before entering this level");

                EnterLevelDialogue("Water Lv 2", 0);
            }

            if (isLevel2 && LevelParticlesCompleted.activeSelf)
            {
                Debug.Log("You have already cleared this level.");

                EnterLevelDialogue("Water Lv 2", 0);
            }

            if (isBoss && LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("Entering Water Boss");

                EnterLevelDialogue("Water Boss", 0);
            }

            if (isBoss && !LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("You must clear Water Level 1 and 2 before challenging the boss");

                EnterLevelDialogue("Water Boss", 0);
            }

            if (isBoss && LevelParticlesCompleted.activeSelf)
            {
                Debug.Log("You have already cleared this world.");

                EnterLevelDialogue("Water Boss", 0);
            }
        }

        if (isMine)
        {
            if (isLevel1 && LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("Entering Mine Level 1");
                EnterLevelDialogue("Mine Lv 1", 1);
            }

            if (isLevel1 && LevelParticlesCompleted.activeSelf)
            {
                Debug.Log("You have already cleared this level.");
                EnterLevelDialogue("Mine Lv 1", 1);
            }

            if (isLevel2 && LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("Entering Mine Level 2");
                EnterLevelDialogue("Mine Lv 2", 0);
            }

            if (isLevel2 && !LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("You must clear Mine Level 1 before entering this level");
                EnterLevelDialogue("Mine Lv 2", 0);
            }

            if (isLevel2 && LevelParticlesCompleted.activeSelf)
            {
                Debug.Log("You have already cleared this level.");
                EnterLevelDialogue("Mine Lv 2", 0);
            }

            if (isBoss && LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("Entering Mine Boss");
                EnterLevelDialogue("Mine Boss", 0);
            }

            if (isBoss && !LevelParticlesNotCompleted.activeSelf)
            {
                Debug.Log("You must clear Mine Level 1 and 2 before challenging the boss");
                EnterLevelDialogue("Mine Boss", 0);
            }

            if (isBoss && LevelParticlesCompleted.activeSelf)
            {
                Debug.Log("You have already cleared this world.");
                EnterLevelDialogue("Mine Boss", 0);
            }
        }

        if (isMainMap)
        {
            StartCoroutine(Player.GetComponent<TPFD_Playground>().LoadTP(Player.GetComponent<TPFD_Playground>().MainMapSpawnpoint.transform));
        }
    }

}
