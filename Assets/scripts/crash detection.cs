using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrashDetection : MonoBehaviour
{
    //[SerializeField] float crashDelay = 0.2f;
    [SerializeField] ParticleSystem crashEffect; [SerializeField] AudioClip crashSound; [SerializeField] AudioSource audioSourcecrash;
    [SerializeField] ParticleSystem coineEfect; [SerializeField] AudioClip coinsound;[SerializeField] AudioSource audioSourcecollect;
    private Vector3 respawnPoint;
    private int score = 0;
    public Text scoretext;
    public int lives_count = 3;
    public Text lives_count_text;
  
    private bool hasCrashed = false; 

    void Start()
    {

        if (audioSourcecrash == null)
        {
            audioSourcecrash = GetComponent<AudioSource>();
        }
        respawnPoint = transform.position;
        scoretext.text = score.ToString();
        lives_count_text.text = lives_count.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") && !hasCrashed)  // Check if crash already happened
        {

            hasCrashed = true;  // Set flag to prevent multiple crashes
            FindObjectOfType<PlayerController>().disablecontrols();
            crashEffect.Play();  // Play crash particle effect
            PlayCrashSound();   
            if (lives_count != 0)
            {
                lives_count -= 1;
                Invoke("RestartScene", crashSound.length);
            }
            else if(lives_count==0)
            {
                Invoke("failedscene", crashSound.length);
            }
            lives_count_text.text = lives_count.ToString();
        }
        else if(other.CompareTag("Checkpoint"))
        {
            Debug.Log("checkpoint reached");
            respawnPoint = transform.position;
        }
        else if(other.CompareTag("collectibles") && !hasCrashed)
        {
            
            score += 1;
            PLayCoinSound(); coineEfect.Play();
            other.gameObject.SetActive(false);
            scoretext.text = score.ToString();
            Instantiate(coineEfect, other.transform.position, Quaternion.identity);
        }
    }

    void PlayCrashSound()
    {
        audioSourcecrash.clip = crashSound;
        audioSourcecrash.Play();  // Play the crash sound once
    }
    void PLayCoinSound()
    {
        audioSourcecollect.clip = coinsound;
        audioSourcecollect.Play();
    }

    void RestartScene()
    {
        hasCrashed = false;


        transform.position = new Vector3(respawnPoint.x, -560, transform.position.z);
        transform.rotation = Quaternion.Euler(0, 0, 0); // Ensure Z rotation is reset

    
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;           
            rb.angularVelocity = 0f;        
            rb.Sleep();                             
        }

        FindObjectOfType<PlayerController>().enablecontrols();
    }

    void failedscene()
    {
        pausemenu pauseMenuScript = FindObjectOfType<pausemenu>();
        if (pauseMenuScript != null)
        {
            pauseMenuScript.MissionFailed();
        }
    }
}
