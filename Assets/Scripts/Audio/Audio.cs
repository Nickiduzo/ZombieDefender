using UnityEngine;

[System.Serializable]
public class Audio 
{
    public string name;

    public AudioType type;

    [Range(0f, 1f)] public float volume;
    [Range(0f, 1f)] public float pitch;
    public AudioClip clip;

    public bool loop;

    [HideInInspector] public AudioSource source;
}

public enum AudioType
{
    RockStep,
    GrassStep,
    Attack,
    Hit,
    Jump,
    Music
}
