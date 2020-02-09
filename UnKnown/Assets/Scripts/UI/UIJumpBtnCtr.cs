using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIJumpBtnCtr : UIMainIngameButton
{
    bool m_Pressed;
    private void Update()
    {
        if (player == null)
            FindPlayer();
        if (m_Pressed == true)
            player.DoJump();
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
                player.DoJump();
            }
            if (pressed == false)
            {

                player.DoIdle();
            }
        }
    }
}
