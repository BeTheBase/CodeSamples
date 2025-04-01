using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Babylonian
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource audioSource;
        public static AudioManager instance;

        private void Awake()
        {
            audioSource.time = 340f;
            audioSource.volume = 0;
            StartCoroutine(FadeTrack());

            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator FadeTrack()
        {
            float timeTOFade = 0.34f;
            float timeElapsed = 0f;

            while(audioSource.volume < 1)
            {
                audioSource.volume = Mathf.Lerp(0, 1, 3 * Time.deltaTime);
                timeElapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
            
        }

        private void LateUpdate()
        {
            if (audioSource.time > 420)
            {
                StartCoroutine(FadeTrack());

                audioSource.time = 340f;
            }
        }
    }
}
