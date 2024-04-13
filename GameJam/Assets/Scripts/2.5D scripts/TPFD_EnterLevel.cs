using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPFD_EnterLevel : MonoBehaviour
{
    [Header("Data")]
    public GameObject Player;

    public string[] messages;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void EnterLevel(string SceneLevelName)
    {
        Time.timeScale = 1.0f;
        Player.GetComponent<TPFD_PlayerController>().isTalking = false;
        SceneManager.LoadScene(SceneLevelName);

        gameObject.SetActive(false);
    }

    public void CloseBox()
    {
        //Debug.Log("Closing Box");
        Time.timeScale = 1f;
        Player.GetComponent<TPFD_PlayerController>().isTalking = false;
        gameObject.SetActive(false);
    }
}
