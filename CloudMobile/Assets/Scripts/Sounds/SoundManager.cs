using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Main;

    public SoundResources resource;

    [SerializeField]
    [Range(0,1)]
    private float volume;
    public AudioClip actuallClip;

    private List<AudioSource> allSFXsources = new List<AudioSource>();

    private void Awake()
    {
        if (Main == null)
        {
            Main = this;
        }
        else if (Main != this)
        {
            Destroy(gameObject);
        }
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
            case SoundType.levelFinished:
                clip = resource.levelFinished;
                break;
            case SoundType.deathSound:
                clip = resource.deathSound;
                break;
            case SoundType.backGround:
                clip = resource.backgroundMusic;
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
    levelFinished,
    deathSound,
    backGround
}
