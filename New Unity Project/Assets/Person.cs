using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum Emotion
{
    Laughing,
    Happy,
    Neutral,
    Awkward,
    Sad,
};

[System.Serializable]
public class OnEmotionChanged : UnityEvent<Emotion> {};

public class Person : MonoBehaviour
{
    // Event for performing an emotion.
    public OnEmotionChanged emote_event;

    // Event for changing the mood score.
    public UnityEvent mood_event;

    public int Mood
    {
        get; set;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (emote_event == null)
            emote_event = new OnEmotionChanged();

        Debug.Log("Current mood score: " + Mood as string);
        emote_event.AddListener(ChangeEmotion);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && emote_event != null)
        {
            emote_event.Invoke(Emotion.Laughing);
        }
    }

    void ChangeEmotion(Emotion emotion)
    {
        Debug.Log("I feel " + emotion as string);
    }
}
