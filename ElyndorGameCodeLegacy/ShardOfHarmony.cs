using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShardOfHarmony : MonoBehaviour
{
    public MonoBehaviour EnvoEffects;
    public UnityEvent ShakeEvent;
    public AudioClip VoiceLineClip;

    private AudioSource m_AudioSource;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            EventManager.TriggerEvent("Collect Artifact");
            m_AudioSource = other.gameObject.GetComponent<AudioSource>();
            if (VoiceLineClip != null)
            {
                ShakeEvent.Invoke();
                EnvoEffects.enabled=true;
                m_AudioSource.clip = VoiceLineClip;
                m_AudioSource.Play();
            }
            this.gameObject.SetActive(false);
        }
    }
}
