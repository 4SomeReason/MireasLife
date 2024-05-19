using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

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
        public (string, string) ReturnNode(int id) => (nodes[id - 1].text, nodes[id - 1].image);
    }
    public JsonData data;
    void Awake()
    {
        using StreamReader sr = new StreamReader("GameJSON.json");
        string json = sr.ReadToEnd();
        data = JsonConvert.DeserializeObject<JsonData>(json);
        sr.Close();
        Debug.Log(data.ReturnNode(1).Item1);
        Debug.Log(data.ReturnNode(1).Item2);
        Debug.Log(data.nodes.Count);
    }
    void Update() { }
}