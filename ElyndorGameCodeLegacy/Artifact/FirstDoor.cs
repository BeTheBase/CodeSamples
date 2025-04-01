using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : MonoBehaviour
{
    public AudioClip AudioClip;
    public AudioSource VoiceLineCompleted;
    
    private void Start()
    {
        EventManager.StartListening("CollectArtifactCompleted", OpenDoor);
    }

    public void OpenDoor()
    {
        Debug.Log("Door is opening...");
        if (VoiceLineCompleted != null)
        {
            VoiceLineCompleted.clip = AudioClip;
            VoiceLineCompleted.Play();
        }

        EventManager.StopListening("CollectArtifactCompleted", OpenDoor);

        gameObject.SetActive(false);
    }
}
