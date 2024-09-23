using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem surfeffect;
    [SerializeField] AudioClip surf;
    [SerializeField] AudioSource audioSource; // Separate AudioSource for surfing audio
    [SerializeField] float groundCheckDelay = 0.1f; // Time buffer to ensure constant ground contact
    private bool isGrounded = false;
    private Coroutine groundExitCoroutine;

    void Start()
    {
        // Ensure we have the correct AudioSource if not assigned in the inspector
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded)
            {
                isGrounded = true;
                surfeffect.Play();

                // Play the surfing audio if it's not already playing
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = surf;
                    audioSource.Play();
                }

                // Cancel any pending coroutine for stopping audio
                if (groundExitCoroutine != null)
                {
                    StopCoroutine(groundExitCoroutine);
                    groundExitCoroutine = null;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (isGrounded)
            {
                isGrounded = false;

                // Delay stopping the audio and particle effect to prevent sudden stops
                groundExitCoroutine = StartCoroutine(StopAudioAfterDelay(groundCheckDelay));
            }
        }
    }

    IEnumerator StopAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        surfeffect.Stop();

        // Stop the surfing audio after a delay
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
