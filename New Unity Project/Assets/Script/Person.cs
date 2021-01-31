using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnEmotionChanged : UnityEvent<string> { };

[System.Serializable]
public class OnMoodChanged : UnityEvent<int> { };

public class Person : MonoBehaviour
{
    public AudioSet music;
    public Animator animator;

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
            mood_event.Invoke(value);
            _mood = value;
        }
    }

    private string _current_emotion = "neutral";
    public string CurrentEmotion
    {
        get
        {
            return _current_emotion;
        }
        set
        {
            emote_event.Invoke(value);
            _current_emotion = value;
        }
    }

    void Awake()
    {
        voice = GetComponent<AudioSet>() as AudioSet;
        animator = GetComponentInChildren<Animator>();

        if (emote_event == null) emote_event = new OnEmotionChanged();
        if (mood_event == null) mood_event = new OnMoodChanged();
    }

    void Start()
    {
        music.StartFadeVolume("music_base", 1f, 1f);

        Mood = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("down")) Mood -= 1;
        if (Input.GetKeyDown("up")) Mood += 1;

        var voiceSourcesPlaying = 0;
        for (int i = 0; i < voice.sources.Count; i++)
        {
            if (voice.sources.ElementAt(i).Value.volume > 0.1f)
            {
                voiceSourcesPlaying++;
            }
            if (i == voice.sources.Count - 1)
            {
                if (voiceSourcesPlaying > 0) animator.SetBool("IsTalking", true);
                else animator.SetBool("IsTalking", false);
            }
        }
    }

    public void StartSpeaking()
    {
        StopSpeaking();
        switch (_current_emotion)
        {
            case "happy":
                voice.StartFadeVolume("happy", 1f, voice.fading_time);
                break;
            case "neutral":
                voice.StartFadeVolume("neutral", 1f, voice.fading_time);
                break;
            case "sad":
                voice.StartFadeVolume("sad", 1f, voice.fading_time);
                break;
            default:
                break;
        }
    }

    public void StopSpeaking()
    {
        voice.StopAllCoroutines();
        voice.StartFadeVolume("happy", 0f, voice.fading_time);
        voice.StartFadeVolume("neutral", 0f, voice.fading_time);
        voice.StartFadeVolume("sad", 0f, voice.fading_time);
    }

    public void ChangeEmotion(string emotion)
    {
        Debug.Log("I feel " + emotion as string);
        // StartSpeaking(Emotion.Sad);
    }

    [Yarn.Unity.YarnCommand("change_mood")]
    public void ChangeMood(string value)
    {
        int int_value = int.Parse(value);
        Mood += int_value;

        if (Mood <= -3)
        {
            music.StartFadeVolume("music_happy", 0f, 1f);
            music.StartFadeVolume("music_sad", 1f, 1f);
        }
        else if (Mood < 0)
        {
            music.StartFadeVolume("music_happy", 0f, 1f);
            music.StartFadeVolume("music_sad", 0f, 1f);
        }
        else
        {
            music.StartFadeVolume("music_happy", 1f, 1f);
            music.StartFadeVolume("music_sad", 0f, 1f);
        }

        Debug.Log("Current mood score: " + Mood as string);
    }
}
