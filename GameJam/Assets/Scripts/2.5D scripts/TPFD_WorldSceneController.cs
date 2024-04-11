using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPFD_WorldSceneController : MonoBehaviour
{
    [Header("GameManager Reference")]
    private TPFD_GameManager gameManager;

    [Header("World check")]
    public bool isForest;
    public bool isWater;
    public bool isMine;

    [Header("Empty Parent Assets")]
    public GameObject OnCompleteFirstLevel;
    public GameObject OnCompleteSecondLevel;
    public GameObject OnCompleteBoss;

    public GameObject UnCompleteFirstLevel;
    public GameObject UnCompleteSecondLevel;
    public GameObject UnCompleteBoss;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<TPFD_GameManager>();
        OnCompleteFirstLevel = this.transform.GetChild(0).gameObject;
        UnCompleteFirstLevel = this.transform.GetChild(1).gameObject;
        OnCompleteSecondLevel = this.transform.GetChild(2).gameObject;
        UnCompleteSecondLevel = this.transform.GetChild(3).gameObject;
        OnCompleteBoss = this.transform.GetChild(4).gameObject;
        UnCompleteBoss = this.transform.GetChild(5).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        setActive();
        

    }

    void setActive()
    {
        if(isForest)
        {
            OnCompleteFirstLevel.SetActive(gameManager.isForestLevel1Completed);
            UnCompleteFirstLevel.SetActive(!gameManager.isForestLevel1Completed);

            OnCompleteSecondLevel.SetActive(gameManager.isForestLevel2Completed);
            UnCompleteSecondLevel.SetActive(!gameManager.isForestLevel2Completed);

            OnCompleteBoss.SetActive(gameManager.isForestBossCompleted);
            UnCompleteBoss.SetActive(!gameManager.isForestBossCompleted);

            return;
        }

        if (isWater)
        {
            OnCompleteFirstLevel.SetActive(gameManager.isWaterLevel1Completed);
            UnCompleteFirstLevel.SetActive(!gameManager.isWaterLevel1Completed);

            OnCompleteSecondLevel.SetActive(gameManager.isWaterLevel2Completed);
            UnCompleteSecondLevel.SetActive(!gameManager.isWaterLevel2Completed);

            OnCompleteBoss.SetActive(gameManager.isWaterBossCompleted);
            UnCompleteBoss.SetActive(!gameManager.isWaterBossCompleted);

            return;
        }

        if (isMine)
        {
            OnCompleteFirstLevel.SetActive(gameManager.isMineLevel1Completed);
            UnCompleteFirstLevel.SetActive(!gameManager.isMineLevel1Completed);

            OnCompleteSecondLevel.SetActive(gameManager.isMineLevel2Completed);
            UnCompleteSecondLevel.SetActive(!gameManager.isMineLevel2Completed);

            OnCompleteBoss.SetActive(gameManager.isMineBossCompleted);
            UnCompleteBoss.SetActive(!gameManager.isMineBossCompleted);

            return;
        }
        
    }

}
