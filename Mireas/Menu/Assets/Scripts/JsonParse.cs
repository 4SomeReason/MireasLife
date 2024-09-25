using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System;

public class JsonParse : MonoBehaviour
{
    [System.Serializable]
    public class Node
    {
        public int id { get; set; }
        public string text { get; set; }
        public string image { get; set; }
    }

    [System.Serializable]
    public class Link
    {
        public int id { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public string text { get; set; }
    }

    [System.Serializable]
    public class JsonData
    {
        public List<Node> nodes = new List<Node>();
        public List<Link> links = new List<Link>();

        public (string, string) ReturnNode(int id) => (nodes[id - 1].text, nodes[id - 1].image);

        /// <summary>
        /// Возвращает список ссылок, стартующих из заданного узла.
        /// </summary>
        /// <param name="startNodeId">ID стартового узла.</param>
        /// <returns>Список ссылок.</returns>
        public List<Link> GetLinksFromNode(int startNodeId)
        {
            return links.Where(link => link.start == startNodeId).ToList();
        }

        /// <summary>
        /// Возвращает конкретный узел по его ID.
        /// </summary>
        /// <param name="id">ID узла.</param>
        /// <returns>Объект Node или null, если не найден.</returns>
        public Node GetNodeById(int id)
        {
            return nodes.Find(n => n.id == id);
        }
    }

    public JsonData data;

    void Awake()
    {
        LoadJson();
    }

    /// <summary>
    /// Загружает и десериализует JSON-файл.
    /// </summary>
    void LoadJson()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "GameJSON.json");

        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                data = JsonConvert.DeserializeObject<JsonData>(json);
                Debug.Log("JSON data successfully loaded.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error reading JSON file: {e.Message}");
            }
        }
        else
        {
            Debug.LogError($"JSON file not found at path: {path}");
        }
    }

    void Update()
    {
        // Здесь можно добавить дополнительную логику, если это необходимо.
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Linq;

public class JsonParse : MonoBehaviour
{
    public class Node
    {
        public int id { get; set; }
        public string text { get; set; }
        public string image { get; set; }
    }
    public class Link
    {
        public int id { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public string text { get; set; }
    }
    public class JsonData
    {
        public List<Node> nodes = new List<Node>();
        public List<Link> links = new List<Link>();
        //public (string, string) ReturnNode(int id) => (nodes[id - 1].text, nodes[id - 1].image);
        public (string, string) ReturnNode(int id)
        {
            Node node = nodes.Find(n => n.id == id);
            if (node != null)
            {
                return (node.text, node.image);
            }
            else
            {
                Debug.LogError($"Node with ID {id} not found.");
                return (string.Empty, string.Empty);
            }
        }
        public List<Link> GetLinksFromNode(int startNodeId)
        {
            return links.Where(link => link.start == startNodeId).ToList();
        }
        public Node GetNodeById(int id)
        {
            return nodes.Find(n => n.id == id);
        }
    }
    public JsonData data;
    void Awake()
    {
        string path = $"{Application.streamingAssetsPath}/GameJSON.json";
        using StreamReader sr = new StreamReader(path);
        string json = sr.ReadToEnd();
        data = JsonConvert.DeserializeObject<JsonData>(json);
        sr.Close();
    }
    void Update() { }
}*/