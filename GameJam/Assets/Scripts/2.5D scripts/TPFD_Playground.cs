using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPFD_Playground : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode goToScene = KeyCode.M;
    public KeyCode goToMainMap = KeyCode.R;
    public KeyCode TPForest = KeyCode.F;
    public KeyCode TPMain = KeyCode.P;

    [Header("Spawnpoints")]
    public GameObject MainMapSpawnpoint;
    public GameObject ForestSpawnpoint;

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

            Canvas = GameObject.Find("Canvas");
            canvasAnim = Canvas.GetComponent<Animator>();
        }

        ManageTPs();

    }

    void LoadScene()
    {

        if(Input.GetKeyDown(goToScene))
        {
            StartCoroutine(LoadLevel(goToSceneName));
        }

        if (Input.GetKeyDown(goToMainMap))
        {
            StartCoroutine(LoadLevel(goToMainMapName));
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
    }

    IEnumerator LoadLevel(string sceneName)
    {
        canvasAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadTP(Transform t)
    {
        canvasAnim.SetTrigger("Start");
        canvasAnim.SetTrigger("End");

        yield return new WaitForSeconds(transitionTime);

        this.transform.position = t.position;
    }
}
