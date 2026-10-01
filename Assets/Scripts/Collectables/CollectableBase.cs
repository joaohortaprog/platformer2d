using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{

    public string compareTag = "Player";
    public ParticleSystem collectParticleSystem;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void Awake()
    {
        if (collectParticleSystem != null)
        {
            collectParticleSystem.transform.SetParent(null);
        }

        if (audioSource != null)
        {
            audioSource.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        OnCollect();
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        collectParticleSystem.Play();
        if (audioSource != null) audioSource.Play();
    }
}



