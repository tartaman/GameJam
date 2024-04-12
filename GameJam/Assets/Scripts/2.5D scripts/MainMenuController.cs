using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public TPFD_Playground playground;
    

    public void onPressingPlay()
    {
        Time.timeScale = 1f;
        StartCoroutine(playground.LoadLevel("2.5D Map"));
    }

    public void onPressingSettings()
    {
        Debug.Log("Opening Settings Menu");
    }

    public void onPressingCredits()
    {
        Debug.Log("Opening Credits menu");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
