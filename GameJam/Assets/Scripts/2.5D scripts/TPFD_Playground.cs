using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPFD_Playground : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode goToScene = KeyCode.M;
    public KeyCode goToMainMap = KeyCode.R;

    [Header("Scenes")]
    public string goToSceneName;
    public string goToMainMapName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LoadScene();
    }

    void LoadScene()
    {
        if(Input.GetKeyDown(goToScene))
        {
            SceneManager.LoadScene(goToSceneName);
        }

        if (Input.GetKeyDown(goToMainMap))
        {
            SceneManager.LoadScene(goToMainMapName);
        }
    }
}
