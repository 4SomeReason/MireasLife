using UnityEngine;
using UnityEngine.UI;

public class LoadButtonHandler : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnLoadButtonClick);
    }

    public void OnLoadButtonClick()
    {
        GameManager.instance.LoadGame();
        Debug.Log("Что-то загружает");
    }
}
