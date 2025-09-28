using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BeatmapData
{
    public Sprite sprite;
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
    public List<BeatmapData> beats;
    public Sprite backgroundImage;
}

public class GameManager : MonoBehaviour
{
    public int score = 0;
    private bool isPaused =  false;
    public bool IsPaused { get { return isPaused; } }
    private bool isPlaying = false;
    public bool IsPlaying { get { return isPlaying; } }
    [SerializeField] public List<LevelData> levels;
    
}
