using UnityEngine;
using System.IO;

public class SaveManager
{
    private readonly string _filePath;
    private readonly string _fileName;

    public SaveManager()
    {
        _fileName = "Save.json";
        _filePath = Application.persistentDataPath + "/Saves/";

#if UNITY_EDITOR
        _filePath = Application.dataPath + "/Saves/";
#endif
    }

    public void Save(SaveData data)
    {
        var json = JsonUtility.ToJson(data);

        using (var writer = new StreamWriter(_filePath + _fileName))
        {
            writer.WriteLine(json);
        }
    }

    public SaveData Load()
    {
        string json = "";

        if (File.Exists(_filePath + _fileName) == false)
        {
            Directory.CreateDirectory(_filePath);

            SaveData data = new SaveData();

            Save(data);
        }

        using (var reader = new StreamReader(_filePath + _fileName))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                json += line;
            }

            if(string.IsNullOrEmpty(json))
            {
                return new SaveData();
            }
        }

        return JsonUtility.FromJson<SaveData>(json);
    }
}
