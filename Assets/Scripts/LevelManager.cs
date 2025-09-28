using System;
using UnityEngine;

[Serializable]
public class BeatmapJsonData
{
    public BeatmapDataItem[] beatmapData;
}

[Serializable]
public class BeatmapDataItem
{
    public string timestamp;
    public string type;
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject topBeatSpawner;
    [SerializeField] private GameObject bottomBeatSpawner;
    [SerializeField] private GameObject topCannon;
    [SerializeField] private GameObject bottomCannon;
    private GameManager _gameManager;
    private int _score = 0;
    private int health = 5;
    private bool _playing = false;
    
    
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
            return;
        }
        Debug.Log("LevelManager: Found GameManager.");
    }
    
    
    public void SetupLevel(LevelData levelData)
    {
        Debug.Log("LevelManager: Setting up level...");
        
    }

    private void FixedUpdate()
    {
        if (!_playing && !_gameManager.IsPaused) { return; }
        
        

    }

}
