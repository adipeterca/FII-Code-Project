using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGame {

    public static void Save(PlayerStats player)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        // This is the location where the save files will go.   
        string path = Application.persistentDataPath + "/HarapAlb.save";
        // NEEDS DOCUMENTATION ADDED!!!!
        FileStream fileStream = new FileStream(path, FileMode.Create);

        PlayerData dataToBeSaved = new PlayerData(player);

        binaryFormatter.Serialize(fileStream, dataToBeSaved);
        fileStream.Close();
    }

    public static PlayerData Load()
    {
        // This is the location where the save files will go.   
        string path = Application.persistentDataPath + "/HarapAlb.save";

        if (!File.Exists(path))
        {
            Debug.LogError("File not found in" + path);
            return null;
        }
        else
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            PlayerData dataToBeLoaded = binaryFormatter.Deserialize(fileStream) as PlayerData;

            fileStream.Close();
            return dataToBeLoaded;
        }
    }
}
