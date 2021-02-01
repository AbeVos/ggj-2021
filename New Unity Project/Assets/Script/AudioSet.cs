using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MusicState
{
    Full,
    Base,
    Silent,
}

[System.Serializable]
public class NamedClip
{
    public string name;
    public AudioClip clip;
}

public class AudioSet : MonoBehaviour
{
    public NamedClip[] clips;
    public float fading_time;

    public Dictionary<string, AudioSource> sources;

    // Start is called before the first frame update
    void Awake()
    {
        sources = new Dictionary<string, AudioSource>();

        foreach (NamedClip clip in clips)
        {
            sources.Add(clip.name, InitAudioLoop(clip.clip));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeVolume(string key, float to, float duration)
    {
        float t = 0f;

        AudioSource source = sources[key];
        float start_volume = source.volume;

        while (t <= 1f)
        {
            source.volume = Mathf.Lerp(start_volume, to, t);

            t += Time.deltaTime / duration;
            yield return null;
        }

        source.volume = to;
    }

    private AudioSource InitAudioLoop(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>() as AudioSource;
        source.clip = clip;
        source.playOnAwake = true;
        source.loop = true;
        source.volume = 0f;
        source.Play();

        return source;
    }

    public void StartFadeVolume(string key, float to, float duration)
    {
        StartCoroutine(FadeVolume(key, to, duration));
    }
}
