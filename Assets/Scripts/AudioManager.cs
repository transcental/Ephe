using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindAnyObjectByType<AudioManager>();
                if (_instance) return _instance;
                GameObject singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<AudioManager>();
                _instance.name = _instance.GetType().ToString();
            }
            return _instance;
        }
    }

    public static event Action<AudioClip> OnSoundFinished;

    private AudioSource _audioSource;

    private void Awake()
    {
        Debug.Log("AudioManager Awake");
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (_instance == null)
        {
            _instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        
        _audioSource.volume = 0.5f;
    }

    public void PlaySound(AudioClip clip)
    {
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource is not initialized.");
            return;
        }

        if (clip == null)
        {
            Debug.LogError("AudioClip is null.");
            return;
        }

        _audioSource.PlayOneShot(clip);
        
        if (clip != null && this.gameObject.activeInHierarchy)
        {
            StartCoroutine(WaitForSoundToEnd(clip));
        }
    }

    private System.Collections.IEnumerator WaitForSoundToEnd(AudioClip clip)
    {
        if (!clip) yield break;
        yield return new WaitForSeconds(clip.length);
        OnSoundFinished?.Invoke(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource is not initialized.");
            return;
        }

        if (clip == null)
        {
            Debug.LogError("AudioClip is null.");
            return;
        }

        _audioSource.clip = clip;
        _audioSource.loop = true;
        _audioSource.Play();
    }
}