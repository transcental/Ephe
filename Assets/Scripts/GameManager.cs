using System;
using System.Collections;
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
    public bool IsPlaying { get; set; } = false;
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
    
    public void StartLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Count)
        {
            Debug.LogError("Invalid level index");
            return;
        }

        var level = levels[levelIndex];
        
        StartCoroutine(LoadLevelAndSetup(level));
    }
    
    private IEnumerator LoadLevelAndSetup(LevelData level)
    {
        var asyncOp = SceneManager.LoadSceneAsync("Level");
        while (!asyncOp.isDone)
            yield return null;

        Debug.Log("Scene loaded");

        var levelManagerGameObject = GameObject.Find("EventSystem");
        if (levelManagerGameObject == null)
        {
            Debug.LogError("EventSystem not found in the scene.");
            yield break;
        }
        var levelManager = levelManagerGameObject.GetComponent<LevelManager>();
        if (levelManager == null)
        {
            Debug.LogError("LevelManager not found in the scene.");
            yield break;
        }
        Debug.Log("GameManager: Found LevelManager.");
        levelManager.SetupLevel(level);
    }
    
}

