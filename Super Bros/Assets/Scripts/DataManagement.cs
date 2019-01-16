﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManagement : MonoBehaviour
{
    public static DataManagement dm;
    public int highScore;

    void Awake()
    {
        if (dm == null)
        {
            DontDestroyOnLoad(gameObject);
            dm = this;
        }
        else if(dm != this)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        BinaryFormatter BinForm = new BinaryFormatter(); // Creates a bin formatter
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat"); // Creates file
        gameData data = new gameData(); // Creates container for data
        data.highscore = highScore;
        BinForm.Serialize(file, data); // Serializes
        file.Close(); // Closes file
    }

    public void LoadData()
    {
        if(File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter BinForm = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            gameData data = (gameData)BinForm.Deserialize(file);
            file.Close();
            highScore = data.highscore;
        }
    }
}

[Serializable]
class gameData
{
    public int highscore;
}
