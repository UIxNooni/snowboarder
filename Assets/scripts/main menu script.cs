using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenuscript : MonoBehaviour
{

    public void playgame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void exitgame()
    {
        Debug.Log("exit clicked");
        Application.Quit();
    }
}
