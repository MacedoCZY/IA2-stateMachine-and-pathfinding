using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastScene : MonoBehaviour
{
    public void OnPointerClick()
    {
        ReloadToFrt();
    }

    public void Quit()
    {
        Debug.Log("teste");
        Application.Quit();
    }

    void ReloadToFrt()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
