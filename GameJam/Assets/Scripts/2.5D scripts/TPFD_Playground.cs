using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPFD_Playground : MonoBehaviour
{

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
    public GameObject JoinWorldPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        JoinWorldPanel.SetActive(false);
    }
}
