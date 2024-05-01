using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Diagnostics;

[System.Serializable]
public class Node
{
    public int id;
    public string text;
    public Sprite image;
}

public class GameManager : MonoBehaviour
{
    public Button saveButton;
    public Button loadButton;
    public UnityEngine.UI.Text storyText;
    public UnityEngine.UI.Image storyImage;

    private List<Node> nodes = new List<Node>();
    private int currentNodeId;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadNodesFromJSON();
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        saveButton.onClick.AddListener(SaveGame);
        loadButton.onClick.AddListener(LoadGame);
        LoadNode(currentNodeId);
    }

    public void SaveGame()
    {
        GameData data = new GameData(currentNodeId);
        SaveSystem.SaveGame(data);
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();
        if (data != null)
        {
            currentNodeId = data.currentNodeId;
            LoadNode(currentNodeId);
        }
    }

    void LoadNode(int nodeId)
    {
        Node node = nodes.Find(n => n.id == nodeId);
        if (node != null)
        {
            storyText.text = node.text;
            storyImage.sprite = node.image;
        }
        else
        {
            UnityEngine.Debug.LogError("Node with id " + nodeId + " not found.");
        }
    }

    void LoadNodesFromJSON()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("nodes");
        NodesData nodesData = JsonUtility.FromJson<NodesData>(jsonFile.text);

        foreach (NodeData nodeData in nodesData.nodes)
        {
            Sprite image = Resources.Load<Sprite>(nodeData.imageName);
            Node node = new Node
            {
                id = nodeData.id,
                text = nodeData.text,
                image = image
            };
            nodes.Add(node);
        }
    }
}

[System.Serializable]
public class NodesData
{
    public List<NodeData> nodes;
}

[System.Serializable]
public class NodeData
{
    public int id;
    public string text;
    public string imageName;
}


