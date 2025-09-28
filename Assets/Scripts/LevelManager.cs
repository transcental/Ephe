using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BeatmapJsonData
{
    public BeatmapDataItem[] beatmapData;
    
    public static BeatmapJsonData CreateFromJson(string jsonString)
    {
        return JsonUtility.FromJson<BeatmapJsonData>(jsonString);
    }
}

[Serializable]
public class BeatmapDataItem
{
    public string timestamp;
    public string type;
    public string direction;
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject topBeatSpawner;
    [SerializeField] private GameObject bottomBeatSpawner;
    [SerializeField] private GameObject topCannon;
    [SerializeField] private GameObject bottomCannon;
    [SerializeField] private Image cameraImage;
    private GameManager _gameManager;
    private int _score = 0;
    private int health = 5;
    private bool _playing = false;
    
    private BeatmapJsonData _beatmapData;
    private AudioClip _song;
    private List<BeatData> _beats;
    private AudioSource _audioSource;
    private AudioSource _audioSource1;
    private bool _hasStartedLevel = false;



    private void Awake()
    {
        _audioSource1 = gameObject.AddComponent<AudioSource>();
        _audioSource = GetComponent<AudioSource>();
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
        var jsonDataAsset = levelData.beatmap;
        if (jsonDataAsset == null)
        {
            Debug.LogError("LevelManager: Beatmap JSON data is null.");
            return;
        }
        Debug.Log("LevelManager: Beatmap JSON data loaded.");
        var jsonString = jsonDataAsset.text;
        if (string.IsNullOrEmpty(jsonString))
        {
            Debug.LogError("LevelManager: Beatmap JSON string is empty.");
            return;
        }
        Debug.Log("LevelManager: Beatmap JSON string is valid.");
        
        _beatmapData = BeatmapJsonData.CreateFromJson(jsonString);
        if (_beatmapData == null || _beatmapData.beatmapData == null || _beatmapData.beatmapData.Length == 0)
        {
            Debug.LogError("LevelManager: Failed to parse beatmap JSON data.");
            return;
        }
        Debug.Log("LevelManager: Beatmap JSON data parsed successfully.");
        _song = levelData.backgroundMusic;
        
        if (_song == null)
        {
            Debug.LogError("LevelManager: Background music is null.");
            return;
        }
        Debug.Log("LevelManager: Background music loaded successfully.");
        _beats = levelData.beats;
        Debug.Log("LevelManager: Level setup complete.");
        
        cameraImage.sprite = levelData.backgroundImage;
        
        _playing = true;
    }

    private void Update()
    {
        Debug.Log("Updating");
        if (!_playing && !_gameManager.IsPaused) { return; }
        if (!_hasStartedLevel)
        {
            StartLevelPlay();
            _hasStartedLevel = true;
        }
    }
    
    private void StartLevelPlay()
    {
        if (!_song || !_audioSource) return;

        _audioSource.clip = _song;
        _audioSource.Play();
        _gameManager.IsPlaying = true;
        Debug.Log("LevelManager: Started playing music.");

        StartCoroutine(SpawnBeats());
    }
    
    private System.Collections.IEnumerator SpawnBeats()
    {
        foreach (var beat in _beatmapData.beatmapData)
        {
            if (!_playing) yield break;

            if (float.TryParse(beat.timestamp, out var beatTime))
            {
                var waitTime = beatTime - _audioSource.time;
                if (waitTime > 0)
                {
                    yield return new WaitForSeconds(waitTime);
                }

                SpawnBeat(beat);
            }
            else
            {
                Debug.LogError($"LevelManager: Invalid timestamp format: {beat.timestamp}");
            }
        }
    }
    
    private void SpawnBeat(BeatmapDataItem beat)
    {
        Debug.Log("LevelManager: Spawning beat...");
        GameObject spawner = beat.type == "top" ? topBeatSpawner : bottomBeatSpawner;
        GameObject cannon = beat.type == "top" ? topCannon : bottomCannon;

        if (spawner == null || cannon == null)
        {
            Debug.LogError("LevelManager: Spawner or Cannon is null.");
            return;
        }

        var beatData = _beats.FirstOrDefault(b => b.damaging && beat.direction == "left") ??
                       _beats.FirstOrDefault(b => b.healing && beat.direction == "right");

        if (beatData == null)
        {
            Debug.LogError("LevelManager: No matching BeatData found.");
            return;
        }

        var beatObject = Instantiate(spawner, spawner.transform.position, spawner.transform.rotation);

        var beatRenderer = beatObject.GetComponent<Renderer>();
        if (beatRenderer)
        {
            beatRenderer.material = beatData.material;
        }
        else
        {
            Debug.LogError("LevelManager: Beat component not found on spawned object.");
        }

        var mover = beatObject.AddComponent<BeatMover>();
        mover.Init(beat.direction, _song.length);
    }

    
    public void AdjustScore(int amount)
    {
        _score += amount;
        Debug.Log($"Score adjusted by {amount}. New score: {_score}");
    }
}
