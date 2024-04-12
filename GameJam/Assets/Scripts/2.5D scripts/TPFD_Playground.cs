using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPFD_Playground : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode goToScene = KeyCode.S;
    public KeyCode goToMainMap = KeyCode.R;
    public KeyCode TPMain = KeyCode.P;
    public KeyCode TPForest = KeyCode.F;
    public KeyCode TPWater = KeyCode.W;
    public KeyCode TPMine = KeyCode.M;

    [Header("Spawnpoints")]
    public GameObject MainMapSpawnpoint;
    public GameObject ForestSpawnpoint;
    public GameObject WaterSpawnpoint;
    public GameObject MineSpawnpoint;

    [Header("Scenes")]
    public string goToSceneName;
    public string goToMainMapName;

    [Header("Canvas Data")]
    public GameObject Canvas;
    private Animator canvasAnim;
    public float transitionTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LoadScene();

        if(SceneManager.GetActiveScene().name.Contains("2.5D Map"))
        {
            MainMapSpawnpoint = GameObject.Find("MainSpawnpoint");
            ForestSpawnpoint = GameObject.Find("ForestSpawnpoint");
            WaterSpawnpoint = GameObject.Find("WaterSpawnpoint");
            MineSpawnpoint = GameObject.Find("MineSpawnpoint");

            Canvas = GameObject.Find("Canvas");
            canvasAnim = Canvas.GetComponent<Animator>();
        }

        if(SceneManager.GetActiveScene().name.Contains("Main Menu"))
        {
            Canvas = GameObject.Find("Canvas");
            canvasAnim = Canvas.GetComponent<Animator>();
        }

        ManageTPs();

    }

    void LoadScene()
    {

        if(Input.GetKeyDown(goToScene))
        {
            if(SceneManager.GetActiveScene().buildIndex != SceneManager.GetSceneByName(goToSceneName).buildIndex)
            {
                StartCoroutine(LoadLevel(goToSceneName));

            }

        }

        if (Input.GetKeyDown(goToMainMap))
        {
            Debug.Log($"Load Main map 1 {SceneManager.GetActiveScene().buildIndex} != {SceneManager.GetSceneByName(goToMainMapName).buildIndex}");

            if (SceneManager.GetActiveScene().buildIndex != SceneManager.GetSceneByName(goToMainMapName).buildIndex)
            {
                
                StartCoroutine(LoadLevel(goToMainMapName));
            }
        }
    }

    void ManageTPs()
    {
        if (Input.GetKeyDown(TPForest))
        {
            StartCoroutine(LoadTP(ForestSpawnpoint.transform));
            
        }

        if (Input.GetKeyDown(TPMain))
        {
            StartCoroutine(LoadTP(MainMapSpawnpoint.transform));
        }

        if(Input.GetKeyDown(TPWater))
        {
            StartCoroutine(LoadTP(WaterSpawnpoint.transform));
        }

        if (Input.GetKeyDown(TPMine))
        {
            StartCoroutine(LoadTP(MineSpawnpoint.transform));
        }
    }

    public IEnumerator LoadLevel(string sceneName)
    {
        canvasAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator LoadTP(Transform t)
    {
        canvasAnim.SetTrigger("Start");
        canvasAnim.SetTrigger("End");

        yield return new WaitForSeconds(transitionTime);

        this.transform.position = t.position;
    }
}
