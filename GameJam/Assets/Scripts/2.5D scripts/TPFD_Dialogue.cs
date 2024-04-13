using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TPFD_Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textPressKeyToContinue;
    public string[] lines;
    public float textSpeed;
    public GameObject Player;

    public int index;
    public bool HasFinishedTalking;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (index == 0)
        {
            textComponent.text = lines[0];
        }

        Player.GetComponent<TPFD_PlayerController>().isTalking = true;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        textPressKeyToContinue.text = $"Press {Player.GetComponent<TPFD_PlayerController>().Talk} to continue...";

        if (Input.GetKeyDown(Player.GetComponent<TPFD_PlayerController>().Talk))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            HasFinishedTalking = false;
            StartCoroutine(TypeLine());
        }
        else
        {
            Player.GetComponent<TPFD_PlayerController>().isTalking = false;
            index = 0;
            textComponent.text = string.Empty;
            HasFinishedTalking = true;
            gameObject.SetActive(false);

        }
    }
}
