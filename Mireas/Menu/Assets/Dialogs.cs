using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.UIElements;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Dialogs : MonoBehaviour
{
    public JsonParse.JsonData jp;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    void Start()
    {
        lines = new string[105];
        jp = GameObject.FindGameObjectWithTag("JsonParse").GetComponent<JsonParse>().data;
        textComponent.text = string.Empty;
        for (int i = 1; i <= 105; i++)
        {
            lines[i-1] = jp.ReturnNode(i).Item1;
        }
        StartDialogue();
    }
    void Update()
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
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else { gameObject.SetActive(false); }
    }
}