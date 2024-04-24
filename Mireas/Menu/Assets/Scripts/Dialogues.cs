using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;
using Ink.Runtime;

public class Dialogues : MonoBehaviour
{
    private Story _currentStory; //см. часть 1
    private TextAsset _inkJson;

    private GameObject _dialoguePanel;
    private TextMeshProUGUI _dialogueText;

    private GameObject _chooseButtonPanel;
    [SerializeField] private GameObject chooseButton;
    private List<TextMeshProUGUI> _choiceText = new();

    public bool DialogPlay{ get; private set; }

    public void Construct(DialogInstaller di)
    {
        _inkJson= di.inkJson;
        _dialoguePanel= di.dialoguePanel;
        _dialogueText= di.dialogueText;
        _chooseButtonPanel= di.chooseButtonPanel;
        chooseButton = di.chooseButton;
    }
    private void Awake()
    {
        _currentStory = new Story(_inkJson.text);
    }
    void Start()
    {
        StartDialogues();
    }
    public void StartDialogues()
    {
        DialogPlay = true;
        _dialoguePanel.SetActive(true);
        ContinueStory();
    }
    public void ContinueStory()
    {
        
        if (_currentStory.canContinue)
        {
            ShowDialogue();
            ShowChoiceButtons();
        }
        else
        {
            ExitDialogue();
        }
    }
    private void ShowDialogue()
    {
        _dialogueText.text = _currentStory.Continue();
    }
    private void ShowChoiceButtons()
    {
        List<Choice> currentChoices = _currentStory.currentChoices;
        _chooseButtonPanel.SetActive(currentChoices.Count != 0);
        if (currentChoices.Count <= 0) { return; }
        for (int i = 0; i < currentChoices.Count; i++)
        {
            GameObject choise = Instantiate(chooseButton);
            choise.GetComponent<ButtonAction>().Index = i;
            choise.transform.SetParent(_chooseButtonPanel.transform);

            TextMeshProUGUI choiseText = choise.GetComponentInChildren<TextMeshProUGUI>();
            choiseText.text = currentChoices[i].text;
            _choiceText.Add(choiseText);
        }
    }
    public void ChooseButtonAction(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
    public void ExitDialogue()
    {
        DialogPlay = false;
        _dialoguePanel.SetActive(false);
    }
}
