using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UILeftMoveBtnCtr : UIMainIngameButton
{
    bool m_Pressed;
    private void Update()
    {
        if (player == null)
            FindPlayer();
        if (m_Pressed == true)
            player.DoMove(false);
    }

    void OnPress(bool pressed)
    {
        if (enabled && NGUITools.GetActive(gameObject))
        {
            m_Pressed = pressed;
            if (player == null)
                FindPlayer();


            if (pressed)
            {
                player.DoMove(false);
            }
            if( pressed == false)
            {

                player.DoIdle();
            }
        }
    }
}
