using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

[Serializable]
public class Saved
{
    public int firstStageTime= 0, secondStageTime = 0, tStageTime =0 , fStageTime = 0;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public GameObject board;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public Saved saved = new Saved();
    private string path;
    // Start is called before the first frame update
    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "score.json");
        Load();
        board.GetComponent<Board>().ScoreUpdate();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(saved);
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saved = JsonConvert.DeserializeObject<Saved>(json);
        }
        else
        {
            Save();
        }
    }
}
