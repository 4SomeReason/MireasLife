using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    public int Index;
    private Dialogues _dialogues;
    private UnityAction _clickAction;
    private Button _button;
    void Start()
    {
        _button = GetComponent<Button>();
        _dialogues = FindObjectOfType<Dialogues>();
        _clickAction = new UnityAction(() =>_dialogues.ChooseButtonAction(Index));
        _button.onClick.AddListener(_clickAction);
    }
}
