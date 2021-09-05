using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    internal enum MusicState
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
        private void Awake()
        {
            sources = new Dictionary<string, AudioSource>();

            foreach (NamedClip clip in clips)
            {
                sources.Add(clip.name, InitAudioLoop(clip.clip));
            }
        }

        // Update is called once per frame
        private void Update()
        {

        }

        private IEnumerator FadeVolume(string key, float to, float duration)
        {
            var t = 0f;

            var source = sources[key];
            var start_volume = source.volume;

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
            var source = gameObject.AddComponent<AudioSource>();
        
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
}