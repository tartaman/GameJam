using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void NewScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
