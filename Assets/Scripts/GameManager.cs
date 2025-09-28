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
    public bool IsPaused { get; } = false;
    public bool IsPlaying { get; } = false;
    [SerializeField] private List<LevelData> levels;
}
