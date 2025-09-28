using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class BeatData
{
    public Material material;
    public float rarity;
    public bool healing;
    public bool damaging;
}

[Serializable]
public class LevelData
{
    public string levelName;
    public int difficulty;
    public AudioClip backgroundMusic;
    public TextAsset beatmap;
    public List<BeatData> beats;
    public Sprite backgroundImage;
}

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public bool IsPaused { get; } = false;
    public bool IsPlaying { get; } = false;
    [SerializeField] public List<LevelData> levels;

    private void Awake()
    {
        var objs = GameObject.FindGameObjectsWithTag($"Manager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    
    private void StartLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Count)
        {
            Debug.LogError("Invalid level index");
            return;
        }

        var level = levels[levelIndex];
        // Load level data and initialize the game state
        Debug.Log($"Starting level: {level.levelName} with difficulty {level.difficulty}");

        SceneManager.LoadScene("Level");
        Debug.Log("Scene loaded");
        
        var levelManager = FindObjectOfType<LevelManager>();
        levelManager.SetupLevel(level);
    }
    private bool isPaused =  false;
    public bool IsPaused { get { return isPaused; } }
    private bool isPlaying = false;
    public bool IsPlaying { get { return isPlaying; } }
    [SerializeField] public List<LevelData> levels;
    
}
