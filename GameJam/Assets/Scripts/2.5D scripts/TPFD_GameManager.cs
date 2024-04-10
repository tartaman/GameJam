using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class TPFD_GameManager : MonoBehaviour
{

    [Header("Game Manager Persistence")]
    [SerializeField] public static TPFD_GameManager gameManagerInstance;
    [SerializeField] public GameObject WorldProps;
    Transform[] objs;


    [Header("Level Complete")]
    public bool isForestLevel1Completed;
    public bool isForestLevel2Completed;
    public bool isForestBossCompleted;

    public bool isWaterLevel1Completed;
    public bool isWaterLevel2Completed;
    public bool isWaterBossCompleted;

    public bool isMineLevel1Completed;
    public bool isMineLevel2Completed;
    public bool isMineBossCompleted;

    [Header("Empty Parents Uncomplete Forest Level")]
    [SerializeField] public GameObject ForestAssets1rstUncompleteLevelParent;
    [SerializeField] public GameObject ForestAssets2ndUncompleteLevelParent;
    [SerializeField] public GameObject ForestAssetsUncompleteBossParent;

    [Header("Empty Parents Complete Forest Level")]
    [SerializeField] public GameObject ForestAssets1rstLevelParent;
    [SerializeField] public GameObject ForestAssets2ndLevelParent;
    [SerializeField] public GameObject ForestAssetsParent;

    [Header("Empty Parents Uncomplete Water Level")]
    [SerializeField] public GameObject WaterAssets1rstUncompleteLevelParent;
    [SerializeField] public GameObject WaterAssets2ndUncompleteLevelParent;
    [SerializeField] public GameObject WaterAssetsUncompleteBossParent;

    [Header("Empty Parents Complete Water Level")]
    [SerializeField] public GameObject WaterAssets1rstLevelParent;
    [SerializeField] public GameObject WaterAssets2ndLevelParent;
    [SerializeField] public GameObject WaterAssetsParent;

    [Header("Empty Parents Uncomplete Mine Level")]
    [SerializeField] public GameObject MineAssets1rstUncompleteLevelParent;
    [SerializeField] public GameObject MineAssets2ndUncompleteLevelParent;
    [SerializeField] public GameObject MineAssetsUncompleteBossParent;

    [Header("Empty Parents Complete Mine Level")]
    [SerializeField] public GameObject MineAssets1rstLevelParent;
    [SerializeField] public GameObject MineAssets2ndLevelParent;
    [SerializeField] public GameObject MineAssetsParent;

    private GameObject returnGameObjectByName(string Name)
    {
        foreach(Transform t in objs)
        {
            if(t.gameObject.name == Name)
            {
                return t.gameObject;
            }
        }

        return null;
    }

    private void assignAssetsToGameManager()
    {
        ForestAssets1rstLevelParent = returnGameObjectByName("ForestAssets1rstLevelParent");
        ForestAssets2ndLevelParent = returnGameObjectByName("ForestAssets2ndLevelParent");
        ForestAssetsParent = returnGameObjectByName("ForestAssetsParent");

        ForestAssets1rstUncompleteLevelParent = returnGameObjectByName("ForestAssets1rstUncompleteLevelParent");
        ForestAssets2ndUncompleteLevelParent = returnGameObjectByName("ForestAssets2ndUncompleteLevelParent");
        ForestAssetsUncompleteBossParent = returnGameObjectByName("ForestAssetsUncompleteBossParent");

        WaterAssets1rstLevelParent = returnGameObjectByName("WaterAssets1rstLevelParent");
        WaterAssets2ndLevelParent = returnGameObjectByName("WaterAssets2ndLevelParent");
        WaterAssetsParent = returnGameObjectByName("WaterAssetsParent");

        WaterAssets1rstUncompleteLevelParent = returnGameObjectByName("WaterAssets1rstUncompleteLevelParent");
        WaterAssets2ndUncompleteLevelParent = returnGameObjectByName("WaterAssets2ndUncompleteLevelParent");
        WaterAssetsUncompleteBossParent = returnGameObjectByName("WaterAssetsUncompleteBossParent");

        MineAssets1rstLevelParent = returnGameObjectByName("MineAssets1rstLevelParent");
        MineAssets2ndLevelParent = returnGameObjectByName("MineAssets2ndLevelParent");
        MineAssetsParent = returnGameObjectByName("MineAssetsParent");

        MineAssets1rstUncompleteLevelParent = returnGameObjectByName("MineAssets1rstUncompleteLevelParent");
        MineAssets2ndUncompleteLevelParent = returnGameObjectByName("MineAssets2ndUncompleteLevelParent");
        MineAssetsUncompleteBossParent = returnGameObjectByName("MineAssetsUncompleteBossParent");
    }

    private void setMapLogic()
    {

        //Forest
        ForestAssets1rstLevelParent.SetActive(isForestLevel1Completed);
        ForestAssets1rstUncompleteLevelParent.SetActive(!isForestLevel1Completed);

        ForestAssets2ndLevelParent.SetActive(isForestLevel2Completed);
        ForestAssets2ndUncompleteLevelParent.SetActive(!isForestLevel2Completed);

        ForestAssetsParent.SetActive(isForestBossCompleted);
        ForestAssetsUncompleteBossParent.SetActive(!isForestBossCompleted);


        //Water
        WaterAssets1rstLevelParent.SetActive(isWaterLevel1Completed);
        WaterAssets1rstUncompleteLevelParent.SetActive(!isWaterLevel1Completed);

        WaterAssets2ndLevelParent.SetActive(isWaterLevel2Completed);
        WaterAssets2ndUncompleteLevelParent.SetActive(!isWaterLevel2Completed);

        WaterAssetsParent.SetActive(isWaterBossCompleted);
        WaterAssetsUncompleteBossParent.SetActive(!isWaterBossCompleted);

        //Mine
        MineAssets1rstLevelParent.SetActive(isMineLevel1Completed);
        MineAssets1rstUncompleteLevelParent.SetActive(!isMineLevel1Completed);

        MineAssets2ndLevelParent.SetActive(isMineLevel2Completed);
        MineAssets2ndUncompleteLevelParent.SetActive(!isMineLevel2Completed);

        MineAssetsParent.SetActive(isMineBossCompleted);
        MineAssetsUncompleteBossParent.SetActive(!isMineBossCompleted);
    }

    private void Awake()
    {

        if(gameManagerInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        gameManagerInstance = this;
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        if(SceneManager.GetActiveScene().name == "2.5D Map")
        {
            WorldProps = GameObject.Find("World Props");

            objs = WorldProps.GetComponentsInChildren<Transform>(true);

            assignAssetsToGameManager();
            setMapLogic();
        }

    }

    private void Start()
    {
        
    }
}
