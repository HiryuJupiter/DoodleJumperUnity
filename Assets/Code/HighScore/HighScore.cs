using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class HighScore
{
    //Represent a string using a const to prevent typos
    const string FileName = "/PlayerHighScore.save";

    public static void SaveHighscore (int score)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //Using statement to automatically close FileStream when code goes out of scope
        using (FileStream file = File.Create(Application.persistentDataPath + FileName))
        {
            //Convert the object into a binary file
            formatter.Serialize(file, score);
            Debug.Log("High score saved to: " + Application.persistentDataPath + FileName);
        }
    }

    public static int LoadHighScore ()
    {
        int highscore = 0;
        if (File.Exists(Application.persistentDataPath + FileName))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream file = File.Open(Application.persistentDataPath + FileName, FileMode.Open))
            {
                //Deserialization returns an Object type, so we have to manually cast it.
                highscore = (int)formatter.Deserialize(file);
            }
        }

        return highscore;
    }
}


/*
 public static class HighScore
{
    const string Key_HighScore = "HighScore";

    public static void SaveHighscore (int score)
    {
        //Save to playerpref
        PlayerPrefs.SetInt(Key_HighScore, score);
    }

    public static int LoadHighScore ()
    {
        //Load from player pref
        return PlayerPrefs.GetInt(Key_HighScore, 0);
    }
}
 */