using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static string directory = "/saveData";
    public static string fileName = "MyData.txt";

    public static void SavePlayer(IndexData saveData)
    {
        string dir = Application.persistentDataPath + directory;

        if(!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
            Debug.Log("Save file created");
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(dir + fileName, json);
            Debug.Log(json);
        }
        else if(Directory.Exists(dir))
        {
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(dir + fileName, json);
            Debug.Log(json);
        }        
    }

    public static IndexData LoadPlayer()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        IndexData saveData = new IndexData();

        if(File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            saveData = JsonUtility.FromJson<IndexData>(json);

            Debug.Log("Save file found and loaded");
        }
        else
        {
            Debug.Log("Save file does not exist");
        }
        return saveData;
    }
}