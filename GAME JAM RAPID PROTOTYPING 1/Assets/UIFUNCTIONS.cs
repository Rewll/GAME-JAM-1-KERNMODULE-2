using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFUNCTIONS : MonoBehaviour
{
    public GameObject helpScreen;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void HelpScreen()
    {
        helpScreen.SetActive(true);
    }

    public void goBack()
    {
        helpScreen.SetActive(false);
    }
}
