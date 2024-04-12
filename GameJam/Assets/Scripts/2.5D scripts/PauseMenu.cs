using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public KeyCode pauseKeyCode;
    public static bool isPaused;

    private void Awake()
    {
        InitializePauseMenu();

        pauseMenu.SetActive(false);
        isPaused = false;
    }

    void Start()
    {
        
    }

    void Update()
    {

        InitializePauseMenu();

        if (Input.GetKeyDown(pauseKeyCode))
        {
            if(isPaused)
            {
                ResumeGame();
            }

            if(!isPaused) { 
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        isPaused = false;
    }

    public void onPressingMainMenu()
    {
        StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<TPFD_Playground>().LoadLevel("Main Menu"));
    }

    public void onPressingSettings()
    {
        Debug.Log("Opening settings");
    }

    public void InitializePauseMenu()
    {
        if (pauseKeyCode == KeyCode.None) pauseKeyCode = KeyCode.Escape;
        
    }
}
