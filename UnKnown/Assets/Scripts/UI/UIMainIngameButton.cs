using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainIngameButton : MonoBehaviour
{
    protected JYPlayer player;
    public GameObject uiRoot;
    public GameObject[] lifeObj;

    private void Start()
    {
        lifeObj = new GameObject[3];
        
    }
    public void RemoveLife(int lifeCnt)
    {
        for (int i = 0; i < lifeObj.Length; i++)
        {
            if (i <= (lifeCnt - 1))
                lifeObj[i].SetActive(true);
            else
                lifeObj[i].SetActive(false);
        }
    }

    protected void FindPlayer()
    {
        player = uiRoot.transform.Find("CharacterRoot/player(Clone)").GetComponent<JYPlayer>();
    }
   
    public void CallJumpBtn()
    {
        if (player == null)
            FindPlayer();
        player.DoJump();
    }

}
