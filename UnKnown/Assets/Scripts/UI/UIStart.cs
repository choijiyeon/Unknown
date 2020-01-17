using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStart : MonoBehaviour
{
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
