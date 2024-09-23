using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishline : MonoBehaviour
{
    [SerializeField] float finishdelay =1f;
    [SerializeField] ParticleSystem finisheffect;
    void OnTriggerEnter2D(Collider2D other)

    {
        if (other.tag == "Player")
        {
            finisheffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke("newlevel", finishdelay);
        }

    }
   void newlevel()
    {
        SceneManager.LoadScene(0);
    }
}
