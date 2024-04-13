using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [Header("GameManager Persistence")]
    public GameObject gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public void NewScene(string name)
    {
        gameManager.GetComponent<TPFD_GameManager>().isMineLevel1Completed = true;
        SceneManager.LoadScene(name);
    }
}
