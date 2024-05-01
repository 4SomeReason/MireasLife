using UnityEngine;
using UnityEngine.UI;

public class SaveButtonHandler : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnSaveButtonClick);
    }

    public void OnSaveButtonClick()
    {
        GameManager.instance.SaveGame();
        Debug.Log("Что-то");
    }
}

