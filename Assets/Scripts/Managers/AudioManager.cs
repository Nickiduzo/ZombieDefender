using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private List<Audio> sounds = new List<Audio>();

    public AudioClip[] grassStepClips;
    public AudioClip[] rockSteps;
    public AudioClip[] attackClips;
    public AudioClip[] hitClips;
    public AudioClip[] characterJump;
    public AudioClip[] rockLand;
    public AudioClip[] groundLand;
    public AudioClip[] dummyHit;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }

        Initialize();
    }

    private void Initialize()
    {
        AddAudioFromArray(grassStepClips, AudioType.GrassStep);
        AddAudioFromArray(rockSteps, AudioType.RockStep);
        AddAudioFromArray(attackClips, AudioType.Attack);
        AddAudioFromArray(hitClips, AudioType.Hit);
        AddAudioFromArray(characterJump, AudioType.Jump);
        AddAudioFromArray(rockLand, AudioType.RockLand);
        AddAudioFromArray(groundLand, AudioType.GroundLand);
        AddAudioFromArray(dummyHit, AudioType.DummyHit);

        foreach (Audio audio in sounds)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.loop = audio.loop;
            audio.source.volume = audio.volume;
        }
    }

    private void AddAudioFromArray(AudioClip[] clips, AudioType type)
    {
        foreach (AudioClip clip in clips)
        {
            Audio audio = new Audio
            {
                name = clip.name,
                clip = clip,
                type = type,
                loop = false,
                volume = 0.1f
            };
            sounds.Add(audio);
        }
    }

    public void Play(string soundName)
    {
        Audio audio = sounds.Find(s => s.name == soundName);

        if(audio != null)
        {
            audio.source.Play();
        }
    }

    public void Play(AudioType type)
    {
        List<Audio> audios = sounds.FindAll(s => s.type == type);
        if (audios.Count != 0)
        {
            int index = Random.Range(0, audios.Count);
            audios[index].source.Play();
        }
    }

    public void Stop(string soundName)
    {
        Audio audio = sounds.Find(s => s.name == soundName);
        
        if(audio != null)
        {
            audio.source.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        foreach (Audio audio in sounds)
        {
            audio.source.volume = volume;
        }
    }
}
