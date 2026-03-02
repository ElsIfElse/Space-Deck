using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] SoundClips _sounds;
    List<AudioSource> _sourcePool = new();
    Dictionary<AudioType,AudioClip> _clips = new();
    float _unmuteVolume = 0.5f;
    float _scoreVolume = 0.8f;
    List<AudioSource> _allSources = new();
    AudioSource ScoreSource_01;
    AudioSource ScoreSource_02;
    public float ScoreCrossfadeTime;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        CreateDictionaryForClips_2();
        CreateBasePool();
    }

    void CreateScoreSources()
    {
        ScoreSource_01 = gameObject.AddComponent<AudioSource>();
        ScoreSource_01.playOnAwake = false;
        ScoreSource_01.loop = true;
        ScoreSource_01.volume = _scoreVolume;

        ScoreSource_02 = gameObject.AddComponent<AudioSource>();
        ScoreSource_02.playOnAwake = false;
        ScoreSource_02.loop = true;
        ScoreSource_02.volume = _scoreVolume;
    }

    public void Initialize()
    {
        CreateScoreSources();
        SetStartingVolumes();
        
        PlayScore(AudioType.Score_01);
    }
    
    void SetStartingVolumes()
    {
        foreach(AudioSource source in _allSources) source.volume = _unmuteVolume;

        MenuUiManager.Instance.SettingsMenuHandler.SetMusicSlider(_scoreVolume);
        MenuUiManager.Instance.SettingsMenuHandler.SetSfxSlider(_unmuteVolume);
    }

    public void SetScoreVolume(float value)
    {
        _scoreVolume = value;

        ScoreSource_01.volume = _scoreVolume;
        ScoreSource_02.volume = _scoreVolume;
    }

    public void SetSfxVolume(float value)
    {
        _unmuteVolume = value;
        foreach(AudioSource source in _allSources) source.volume = _unmuteVolume;
    }

    void CreateBasePool()
    {
        for(int i = 0; i < 10; i++)
        {
            Enqueue(CreateAudioSource());
        }
    }
    
    void ExtendPool()
    {
        for(int i = 0; i < 10; i++)
        {
            Enqueue(CreateAudioSource());
        }
    }

    AudioSource Dequeue()
    {
        if(_sourcePool.Count == 0) ExtendPool();
        AudioSource source = _sourcePool[0];
        _sourcePool.RemoveAt(0);
        return source;
    }
    void Enqueue(AudioSource source) => _sourcePool.Add(source);

    AudioSource CreateAudioSource()
    {
        GameObject newObj = new GameObject("AudioSource");
        AudioSource newSource = newObj.AddComponent<AudioSource>();

        newSource.playOnAwake = false;
        newSource.loop = false;
        newSource.volume = 0.5f;
        _allSources.Add(newSource);

        return newSource;
    }

    void PlaySound(AudioClip clip, float delay, bool randomPitch)
    {
        AudioSource source = Dequeue();
        source.clip = clip;
        if(randomPitch) source.pitch = Random.Range(0.85f,1.15f);
        else source.pitch = 1f;
        source.PlayDelayed(delay);
        StartCoroutine(WaitForSourceToFinishAndEnqueue(source));
    }

    public void Play(AudioType audioType,float delay = 0,bool randomPitch = false)
    {
        AudioClip clipToPlay;
        clipToPlay = _clips[audioType];
        PlaySound(clipToPlay,delay,randomPitch);
    }

    IEnumerator WaitForSourceToFinishAndEnqueue(AudioSource source)
    {
        yield return new WaitUntil(() => !source.isPlaying);
        Enqueue(source);
    }
    void CreateDictionaryForClips_2()
    {
        for(int i = 0; i < _sounds.soundClips.Count; i++)
        {
            _clips[_sounds.soundClips[i].Sound.AudioType] = _sounds.soundClips[i].Sound.Clip;
        }
    }

    public void PlayScore(AudioType audioType)
    {
        if(ScoreSource_01.isPlaying)
        {
            ScoreSource_01.DOFade(0,ScoreCrossfadeTime).OnComplete(() => ScoreSource_01.Stop());

            ScoreSource_02.volume = 0;
            ScoreSource_02.clip = _clips[audioType];
            ScoreSource_02.Play();
            ScoreSource_02.DOFade(_scoreVolume,ScoreCrossfadeTime);
            
            return;
        } 
        if(ScoreSource_02.isPlaying)
        {
            ScoreSource_02.DOFade(0,ScoreCrossfadeTime).OnComplete(() => ScoreSource_02.Stop());

            ScoreSource_01.volume = 0;
            ScoreSource_01.clip = _clips[audioType];
            ScoreSource_01.Play();
            ScoreSource_01.DOFade(_scoreVolume,ScoreCrossfadeTime);   
            
            return;
        }

        ScoreSource_01.clip = _clips[audioType];
        ScoreSource_01.Play();
        
    }
}