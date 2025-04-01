using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator LevelTransition;

    public float LevelTransitionTime = 1f;

    public void LoadSpecificLevel(string name)
    {
        StartCoroutine(LoadLevel(name));
    }

    public void LoadSpecificLevel(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    public void LoadLevelAsynNetwork(int index)
    {
        NetEnterWorld enterWorld = new NetEnterWorld(index);
        if (ClientBehaviour.Instance != null) ClientBehaviour.Instance.SendToServer(enterWorld);
        StartCoroutine(LoadLevel(index));
    }

    public void LoadNextLevelButtonClick()
    {
        //The server or client pressed "play" so we send a message to the other to also "play"
        NetEnterWorld enterWorld = new NetEnterWorld();
        if (ClientBehaviour.Instance != null) ClientBehaviour.Instance.SendToServer(enterWorld);
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        //als er een verbinding is moet bij de client ook deze void worden afgevuurd 

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextIndex >=SceneManager.sceneCountInBuildSettings)
        {
            nextIndex = 0;
        }
        StartCoroutine(LoadLevel(nextIndex));
    }

    public void RestartGame()
    {
        Destroy(FindObjectOfType<HighScoreManager>());
        Destroy(FindObjectOfType<Babylonian.AudioManager>());
        StartCoroutine(LoadLevel(0));
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        LevelTransition.SetTrigger("Start");

        yield return new WaitForSeconds(LevelTransitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    private IEnumerator LoadLevel(string name)
    {
        LevelTransition.SetTrigger("Start");

        yield return new WaitForSeconds(LevelTransitionTime);

        SceneManager.LoadScene(name);
    }
}
