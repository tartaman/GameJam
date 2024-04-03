using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPFD_GameManager : MonoBehaviour
{
    [Header("Level passed")]
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
    [SerializeField] GameObject ForestAssets1rstUncompleteLevelParent;
    [SerializeField] GameObject ForestAssets2ndUncompleteLevelParent;
    [SerializeField] GameObject ForestAssetsUncompleteBossParent;

    [Header("Empty Parents Complete Forest Level")]
    [SerializeField] GameObject ForestAssets1rstLevelParent;
    [SerializeField] GameObject ForestAssets2ndLevelParent;
    [SerializeField] GameObject ForestAssetsParent;

    [Header("Empty Parents Uncomplete Water Level")]
    [SerializeField] GameObject WaterAssets1rstUncompleteLevelParent;
    [SerializeField] GameObject WaterAssets2ndUncompleteLevelParent;
    [SerializeField] GameObject WaterAssetsUncompleteBossParent;

    [Header("Empty Parents Complete Water Level")]
    [SerializeField] GameObject WaterAssets1rstLevelParent;
    [SerializeField] GameObject WaterAssets2ndLevelParent;
    [SerializeField] GameObject WaterAssetsParent;

    [Header("Empty Parents Uncomplete Mine Level")]
    [SerializeField] GameObject MineAssets1rstUncompleteLevelParent;
    [SerializeField] GameObject MineAssets2ndUncompleteLevelParent;
    [SerializeField] GameObject MineAssetsUncompleteBossParent;

    [Header("Empty Parents Complete Mine Level")]
    [SerializeField] GameObject MineAssets1rstLevelParent;
    [SerializeField] GameObject MineAssets2ndLevelParent;
    [SerializeField] GameObject MineAssetsParent;

    void Start()
    {
        
    }

    void Update()
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
}
