using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStart : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1280, 720, true);
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene("Easy");
    }
    public void OnClickStartNormal()
    {
        SceneManager.LoadScene("Normal");
    }
    public void OnClickStartHard()
    {
        SceneManager.LoadScene("Hard");
    }

}
