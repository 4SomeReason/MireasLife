using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static void SaveGame(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "game.json");
        File.WriteAllText(path, json);
    }

    public static GameData LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "game.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        return null;
    }
}

[System.Serializable]
public class GameData
{
    public int currentNodeId;

    public GameData(int currentNodeId)
    {
        this.currentNodeId = currentNodeId;
    }
}

