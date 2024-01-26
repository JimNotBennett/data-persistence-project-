using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Save Data")]
    public string playerName;
    public int highScore;

    [Header("Current Game")]
    public string currentName;
    public int currentScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int highScore;
    }


    public void SaveHighScore()
    {
        SaveData data = new SaveData();

        data.playerName = playerName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }


    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            highScore = data.highScore;
        }
    }

}
