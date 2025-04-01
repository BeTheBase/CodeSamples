using GardenCanvasPuzzle_Systems_Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    ConceptArtSlideshowManager conceptArtSlideshowManager;
    IntroNarrativeManager narrativeManager;
    FadeManager fadeManager;
    GameManager gameManager;

    public void Start()
    {
        conceptArtSlideshowManager = GetComponent<ConceptArtSlideshowManager>();
        narrativeManager = GetComponent<IntroNarrativeManager>();
        fadeManager = GetComponent<FadeManager>();
        gameManager = GetComponent<GameManager>();

        EventManager.StartListening("IntroSceneDone", IntroDone);
        EventManager.StartListening("CompletedIntroCutscene", CompletedIntroCutscene);
        StartIntroScene();
    }

    public void StartIntroScene()
    {
       
        fadeManager.FadeOut();
        conceptArtSlideshowManager.StartSlideshow();
        narrativeManager.BeginNarrative();
    }

    public void CompletedIntroCutscene()
    {
        EventManager.StopListening("CompletedIntroCutscene", CompletedIntroCutscene);

        gameManager.StartGame();
    }

    public void IntroDone()
    {
        fadeManager.FadeIn();
        conceptArtSlideshowManager.conceptArtDisplay.gameObject.SetActive(false);
        narrativeManager.NarrativeTextDialog.gameObject.SetActive(false);
        EventManager.StopListening("IntroSceneDone", IntroDone);

        StartCoroutine(StartNextScene());
    }

    public IEnumerator StartNextScene(float waitTime = 2f)
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadSceneAsync(1);
    }
}
