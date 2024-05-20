using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.UIElements;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Dialogs : MonoBehaviour
{
    bool isTextDisplaying = false;
    public JsonParse.JsonData jp;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private float displaySpeed = 1f;
    private int index;
    IEnumerator DisplayText()
    {
        yield return new WaitForSeconds(displaySpeed);
        isTextDisplaying = false;
    }
    void StartDisplayText()
    {
        isTextDisplaying = true;
        StartCoroutine(DisplayText());
    }
    void Start()
    {
        lines = new string[105];
        jp = GameObject.FindGameObjectWithTag("JsonParse").GetComponent<JsonParse>().data;
        textComponent.text = string.Empty;
        for (int i = 1; i <= 105; i++) { lines[i-1] = jp.ReturnNode(i).Item1; }
        StartDialogue();
    }
    void Update()
    {
        if (!isTextDisplaying) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index]) { NextLine(); }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }        
    }
    void StartDialogue()
    {
        isTextDisplaying = true;
        index = 0;
        StartDisplayText();
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
            isTextDisplaying = false;
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else { gameObject.SetActive(false); }
    }
}