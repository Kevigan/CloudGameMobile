using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Main;

    public SoundResources resource;

    AudioSource background;

    [SerializeField]
    [Range(0,1)]
    private float volume;
    public AudioClip actuallClip;
    [SerializeField]
    [Range(0, 1)]
    private float volumeBackground;

    private List<AudioSource> allSFXsources = new List<AudioSource>();

    private void Awake()
    {
        if (Main == null)
        {
            Main = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Main != this)
        {
            Destroy(gameObject);
        }
        background = gameObject.AddComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        List<AudioSource> finishedPlaying = new List<AudioSource>();

        foreach (AudioSource source in allSFXsources)
        {
            if (!source.isPlaying)
            {
                finishedPlaying.Add(source);
            }
        }

        foreach (AudioSource source in finishedPlaying)
        {
            allSFXsources.Remove(source);
            Destroy(source);
        }
    }

    public void PlayNewSound(AudioClip clip, bool loop = false)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = loop;
        source.volume = volume;
        source.Play();
        allSFXsources.Add(source);
    }
    public void PlayNewBackgorund(AudioClip clip, bool loop = false)
    {
        background.clip = null;
        background.clip = clip;
        background.loop = loop;
        background.volume = volumeBackground;
        background.Play();
    }

    public void ChooseBackGroundMusic(BackGroundSound type, bool loop = false)
    {
        AudioClip clip = null;
        switch (type)
        {
            case BackGroundSound.backGround:
                clip = resource.backgroundMusic;
                break;
            case BackGroundSound.levelFinished:
                clip = resource.levelFinished;
                break;
            case BackGroundSound.StartMenuBackground:
                clip = resource.StartMenuBackground;
                break;
        }
        PlayNewBackgorund(clip, loop);
    }

    public void ChooseSound(SoundType type, bool loop = false)
    {
        AudioClip clip = null;
        switch (type)
        {
            case SoundType.playerJump:
                clip = resource.playerJump;
                break;
            case SoundType.collect:
                clip = resource.collect;
                break;
            case SoundType.enemyHit:
                clip = resource.enemyHit;
                break;
            case SoundType.deathSound:
                clip = resource.deathSound;
                break;
            
        }
        PlayNewSound(clip);
    }
}

public enum SoundType
{
    playerJump,
    collect,
    enemyHit,
    deathSound,
}
public enum BackGroundSound
{
    backGround,
    levelFinished,
    StartMenuBackground

}