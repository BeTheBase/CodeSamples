using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GardenCanvasPuzzle_Systems_Game
{
        public class GameManager : MonoBehaviour
        {
            public GameManager Instance;
            public int StartGameIndex = 2;

            public float BackGroundAudioPitch = 0.9f;
            public float BackGroundAudioVolume = 0.5f;
            FadeManager fadeManager;

        private AudioSource m_AudioSource;

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
        }

        private void Start()
        {
            EventManager.StartListening("Collect Artifact", SwitchBackGroundAudioPitch);
            fadeManager.FadeOut();
        }

        public void StartGame()
            {
                LoadLevel(StartGameIndex);
            }

            public void QuitGame()
            {
            fadeManager.FadeOut();

            Application.Quit();
            }

        private void OnEnable()
        {
            fadeManager = GetComponent<FadeManager>();
            m_AudioSource = GetComponent<AudioSource>();
            // Subscribe to the SceneManager events
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable()
        {
            // Unsubscribe to prevent memory leaks
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Scene is loaded, trigger fade-in
            fadeManager.FadeOut();
        }

        private void OnSceneUnloaded(Scene scene)
        {
            // Scene is unloading, trigger fade-out (if needed)
            fadeManager.FadeIn();
        }

        public void LoadLevel(int sceneIndex)
        {
            // Call this to load a new scene with fade-out and fade-in
            LoadSceneWithFade(sceneIndex);
        }

        private void LoadSceneWithFade(int sceneIndex)
        {
            // Trigger fade-out animation
            fadeManager.FadeOut();

            // Load the new scene
            SceneManager.LoadSceneAsync(sceneIndex);
        }

        public void SwitchBackGroundAudioPitch()
        {
            m_AudioSource.pitch = BackGroundAudioPitch;
            m_AudioSource.volume = BackGroundAudioVolume;
            EventManager.StopListening("Collect Artifact", SwitchBackGroundAudioPitch);
        }

        private void CollectArtifact()
        {

        }
    }

}
