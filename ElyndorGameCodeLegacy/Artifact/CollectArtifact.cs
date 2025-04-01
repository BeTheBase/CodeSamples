using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectArtifact : MonoBehaviour
{
    public AudioClip ArtifactClip;
    private AudioSource m_AudioSource;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            EventManager.TriggerEvent("Collect Artifact");
            m_AudioSource = other.gameObject.GetComponent<AudioSource>();
            if (ArtifactClip != null)
            {
                m_AudioSource.clip = ArtifactClip;
                m_AudioSource.Play();
            }
            this.gameObject.SetActive(false);
        }
    }
}
