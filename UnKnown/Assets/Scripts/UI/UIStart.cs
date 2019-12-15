using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStart : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("Game");
    }
	
}
