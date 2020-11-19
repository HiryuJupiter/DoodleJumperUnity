using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad
{
    const string FileName = "/savedGames.gd";

    public static List<Game>  savedGames = new List<Game>();

    public static void Save (Game game)
    {
        savedGames.Add(game);
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream file = File.Create(Application.persistentDataPath + FileName))
        {
            formatter.Serialize(file, SaveLoad.savedGames);
            file.Close();
        }
    }

    public static void Load ()
    {
        if (File.Exists(Application.persistentDataPath + FileName))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream file = File.Open(Application.persistentDataPath + FileName, FileMode.Open))
            {
                //Deserialize passes back an Object type, so we have to cast it.
                SaveLoad.savedGames = (List<Game>)formatter.Deserialize(file);
                file.Close();
            }
        }
    }
}
