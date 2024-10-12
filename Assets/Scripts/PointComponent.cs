using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointComponent : MonoBehaviour
{
    private ParticleSystem explosionEffect;
    private MeshRenderer cubeMeshRenderer;

    void Start()
    {
        // Get the Particle System component attached to the object
        explosionEffect = GetComponentInChildren<ParticleSystem>();
        cubeMeshRenderer = GetComponent<MeshRenderer>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Increment the player's score
            GameManager.Instance.IncrementScore();

            if (explosionEffect != null)
            {
                explosionEffect.Play();
            }

            if (cubeMeshRenderer != null)
            {
                cubeMeshRenderer.enabled = false; // Makes the cube invisible
            }

            // Destroy the object
            Destroy(gameObject, explosionEffect.main.duration);
        }
    }
}
