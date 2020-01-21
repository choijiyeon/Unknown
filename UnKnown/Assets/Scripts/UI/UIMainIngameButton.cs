using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainIngameButton : MonoBehaviour
{
    protected JYPlayer player;
    public GameObject uiRoot;

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
