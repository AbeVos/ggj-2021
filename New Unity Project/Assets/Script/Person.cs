using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum Emotion
{
    Happy,
    Neutral,
    Awkward,
    Sad,
};

[System.Serializable]
public class OnEmotionChanged : UnityEvent<Emotion> {};

[System.Serializable]
public class OnMoodChanged : UnityEvent<int> {};

public class Person : MonoBehaviour
{
    public AudioSet music;

    // Event for performing an emotion.
    public OnEmotionChanged emote_event;

    // Event for changing the mood score.
    public OnMoodChanged mood_event;

    private AudioSet voice;
    private int _mood = 0;
    public int Mood
    {
        get
        {
            return _mood;
        }
        set
        {
            // Debug.Log("Set mood");
            mood_event.Invoke(value);
            _mood = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        voice = GetComponent<AudioSet>();

        if (emote_event == null) emote_event = new OnEmotionChanged();
        if (mood_event == null) mood_event = new OnMoodChanged();

        emote_event.AddListener(ChangeEmotion);

        mood_event.AddListener(ChangeMood);

        Mood = -1;

        music.StartFadeVolume("music_base", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && emote_event != null)
        {
            emote_event.Invoke(Emotion.Neutral);
        }

        if (Input.GetKeyDown("down")) Mood -= 1;
        if (Input.GetKeyDown("up")) Mood += 1;
    }

    public void StartSpeaking(Emotion emotion)
    {
        StopSpeaking();
        switch (emotion)
        {
            case Emotion.Happy:
                voice.StartFadeVolume("happy", 1f, 0.2f);
                break;
            case Emotion.Neutral:
                voice.StartFadeVolume("neutral", 1f, 0.2f);
                break;
            default:
                voice.StartFadeVolume("sad", 1f, 0.2f);
                break;
        }
    }

    public void StopSpeaking()
    {
        voice.StartFadeVolume("happy", 0f, 0.2f);
        voice.StartFadeVolume("neutral", 0f, 0.2f);
        voice.StartFadeVolume("sad", 0f, 0.2f);
    }

    public void ChangeEmotion(Emotion emotion)
    {
        Debug.Log("I feel " + emotion as string);
        StartSpeaking(Emotion.Sad);
    }

    public void ChangeMood(int score)
    {
        if (score <= -3)
        {
            music.StartFadeVolume("music_happy", 0f, 1f);
            music.StartFadeVolume("music_sad", 1f, 1f);
        }
        else if (score < 0)
        {
            music.StartFadeVolume("music_happy", 0f, 1f);
            music.StartFadeVolume("music_sad", 0f, 1f);
        }
        else
        {
            music.StartFadeVolume("music_happy", 1f, 1f);
            music.StartFadeVolume("music_sad", 0f, 1f);
        }

        Debug.Log("Current mood score: " + score as string);
    }
}
