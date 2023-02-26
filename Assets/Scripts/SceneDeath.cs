using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDeath : MonoBehaviour
{
    public void OnPointerClick()
    {
        ReloadLevel();
        Player.alive = true;
    }

    void Update()
    {
      
    }

    public void Quit()
    {
        Debug.Log("teste");
        Application.Quit();
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}   
