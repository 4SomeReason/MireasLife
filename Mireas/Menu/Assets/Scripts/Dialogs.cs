using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static JsonParse;
using System.Reflection;

public class Dialogs : MonoBehaviour
{
    [Header("References")]
    public JsonParse jsonParse; // Ссылка на компонент JsonParse
    public TextMeshProUGUI textComponent; // UI текст для отображения диалога
    public Image characterImage; // UI изображение для персонажа
    public GameObject optionsPanel; // Панель для отображения кнопок выбора
    public GameObject optionButtonPrefab; // Префаб кнопки выбора
    public float textSpeed = 0.05f;

    private int currentNodeId; // Текущий ID узла
    public bool isTextDisplaying = false;

    /*
        lines = new string[105];
        jp = GameObject.FindGameObjectWithTag("JsonParse").GetComponent<JsonParse>().data;
        textComponent.text = string.Empty;
        for (int i = 1; i <= 105; i++) { lines[i-1] = jp.ReturnNode(i).Item1; }
        StartDialogue(); 
    */

    /*
    if links.null currentNodeID + 1;
    else 
    */

    IEnumerator DisplayText()
    {
        yield return new WaitForSeconds(textSpeed);
        isTextDisplaying = false;
    }
    void StartDisplayText()
    {
        isTextDisplaying = true;
        StartCoroutine(DisplayText());
    }

    void Start()
    {
        if (jsonParse == null)
        {
            jsonParse = FindObjectOfType<JsonParse>();
            if (jsonParse == null)
            {
                Debug.LogError("JsonParse component not found in the scene.");
                return;
            }
        }
        textComponent.text = string.Empty;
        currentNodeId = 1; // Начальный узел (можно сделать динамическим)
        StartDialogue(currentNodeId);
    }

    /// <summary>
    /// Запускает диалог с заданного узла.
    /// </summary>
    /// <param name="nodeId">ID узла, с которого начать.</param>
    void StartDialogue(int nodeId)
    {
        var nodeData = jsonParse.data.GetNodeById(nodeId);
        if (nodeData == null)
        {
            Debug.LogError($"Node ID {nodeId} does not exist.");
            return;
        }

        StartDisplayText();
        // Отображаем текст
        StartCoroutine(TypeLine(nodeData.text));

        // Отображаем изображение, если указано
        if (!string.IsNullOrEmpty(nodeData.image) && characterImage != null)
        {
            Sprite sprite = Resources.Load<Sprite>($"Images/{nodeData.image}");
            if (sprite != null)
            {
                characterImage.sprite = sprite;
                characterImage.enabled = true;
            }
            else
            {
                Debug.LogWarning($"Image '{nodeData.image}' not found in Resources/Images.");
                characterImage.enabled = false;
            }
        }

        // Получаем ссылки (варианты выбора) для текущего узла
        List<JsonParse.Link> links = jsonParse.data.GetLinksFromNode(nodeId);

        if (links.Count > 0) { DisplayOptions(links); }
        else { DisplayEndOption(); }
    }

    /// <summary>
    /// Анимированное отображение текста диалога.
    /// </summary>
    /// <param name="line">Текст диалога.</param>
    /// <returns>Coroutine.</returns>
    IEnumerator TypeLine(string line)
    {
        isTextDisplaying = true;
        textComponent.text = "";
        foreach (char c in line.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTextDisplaying = false;
    }

    /*
        IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
            isTextDisplaying = false;
        }
    } 
    */

    /// <summary>
    /// Отображает варианты выбора на экране.
    /// </summary>
    /// <param name="links">Список ссылок (вариантов выбора).</param>
    void DisplayOptions(List<JsonParse.Link> links)
    {
        optionsPanel.SetActive(true);

        // Удаляем старые кнопки
        foreach (Transform child in optionsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Создаём кнопки для каждого варианта
        foreach (var link in links)
        {
            GameObject buttonObj = Instantiate(optionButtonPrefab, optionsPanel.transform);
            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText != null)
            {
                buttonText.text = link.text;
            }

            // Добавляем обработчик события нажатия
            button.onClick.AddListener(() => OnOptionSelected(link.end));
        }
    }

    /// <summary>
    /// Обрабатывает выбор варианта игроком.
    /// </summary>
    /// <param name="nextNodeId">ID следующего узла.</param>
    void OnOptionSelected(int nextNodeId)
    {
        optionsPanel.SetActive(false);
        StartDialogue(nextNodeId);
    }

    /// <summary>
    /// Отображает кнопку завершения диалога, если нет вариантов выбора.
    /// </summary>
    void DisplayEndOption()
    {
        optionsPanel.SetActive(true);

        // Удаляем старые кнопки
        foreach (Transform child in optionsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Создаём кнопку "Конец диалога"
        GameObject buttonObj = Instantiate(optionButtonPrefab, optionsPanel.transform);
        Button button = buttonObj.GetComponent<Button>();
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

        if (buttonText != null)
        {
            buttonText.text = "Продолжить";
        }

        // Добавляем обработчик события нажатия
        button.onClick.AddListener(() => EndDialogue());
    }

    /// <summary>
    /// Завершает диалог.
    /// </summary>
    void EndDialogue()
    {
        // Скрываем панель вариантов
        optionsPanel.SetActive(false);

        // Дополнительная логика при завершении диалога (например, переход к следующей сцене)
        Debug.Log("Диалог завершен.");
        gameObject.SetActive(false); // Пример завершения диалога
    }
}



/*using System.Collections;
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
    public GameObject optionsPanel;
    public GameObject optionButtonPrefab;
    private int currentNodeId; // Текущий ID узла

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
        List<JsonParse.Link> links = jp.data.GetLinksFromNode(nodeId);
        if (links.Count > 0)
        {
            DisplayOptions(links);
        }
        else
        {
            // Если вариантов выбора нет, можно завершить диалог или сделать что-то другое
            DisplayEndOption();
        }
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
    void DisplayOptions(List<JsonParse.Link> links)
    {
        optionsPanel.SetActive(true);

        // Удаляем старые кнопки
        foreach (Transform child in optionsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Создаём кнопки для каждого варианта
        foreach (var link in links)
        {
            GameObject buttonObj = Instantiate(optionButtonPrefab, optionsPanel.transform);
            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText != null)
            {
                buttonText.text = link.text;
            }

            // Добавляем обработчик события нажатия
            button.onClick.AddListener(() => OnOptionSelected(link.end));
        }
    }
    void OnOptionSelected(int nextNodeId)
    {
        optionsPanel.SetActive(false);
        StartDialogue(nextNodeId);
    }
    void DisplayEndOption()
    {
        optionsPanel.SetActive(true);

        // Удаляем старые кнопки
        foreach (Transform child in optionsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Создаём кнопку "Конец диалога"
        GameObject buttonObj = Instantiate(optionButtonPrefab, optionsPanel.transform);
        Button button = buttonObj.GetComponent<Button>();
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

        if (buttonText != null)
        {
            buttonText.text = "Продолжить";
        }

        // Добавляем обработчик события нажатия
        button.onClick.AddListener(() => EndDialogue());
    }
    void EndDialogue()
    {
        // Скрываем панель вариантов
        optionsPanel.SetActive(false);

        // Дополнительная логика при завершении диалога (например, переход к следующей сцене)
        Debug.Log("Диалог завершен.");
        gameObject.SetActive(false); // Пример завершения диалога
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
}*/