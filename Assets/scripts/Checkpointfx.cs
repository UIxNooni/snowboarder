using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpointfx : MonoBehaviour
{
    [SerializeField] ParticleSystem checkpoint;
    void OnTriggerEnter2D(Collider2D other)

    {
        if (other.tag == "Player")
        {
            checkpoint.Play();
            GetComponent<AudioSource>().Play();
        }

    }

}
