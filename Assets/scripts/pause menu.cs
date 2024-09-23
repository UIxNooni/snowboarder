using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{

    public GameObject PauseMenu;
    public GameObject failed;
    public static bool ispaused;
    [SerializeField] public AudioSource[] audioSources;
    private CrashDetection crashDetection;
    private bool isfailed;
    void Start()
    {
        PauseMenu.SetActive(false);
        failed.SetActive(false);
        crashDetection = FindObjectOfType<CrashDetection>();
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(ispaused && !isfailed)
            {
                ResumeGame();
            }
            else if(!isfailed)
            {
                PauseGame();
            }
        }

    }
    
    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        ispaused = true;
        foreach (AudioSource source in audioSources)
        {
            source.Pause();
        }
    }

    public void MissionFailed()
    {
        failed.SetActive(true);
        Time.timeScale = 0f; // Stop time when mission fails
        isfailed = true;
        foreach (AudioSource source in audioSources)
        {
            source.Stop(); // Optionally stop audio on mission fail
        }
    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        ispaused = false;
        foreach (AudioSource source in audioSources)
        {
            source.UnPause();
        }
    }

    public void restartgame()
    {
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1;
    }
    public void gotomainmenu()
    {
        Time.timeScale = 1f;
        foreach (AudioSource source in audioSources)
        {
            source.Stop();
        }
        SceneManager.LoadScene(0);
    }


    public void exitgame()
    {
        Application.Quit();
    }
}
