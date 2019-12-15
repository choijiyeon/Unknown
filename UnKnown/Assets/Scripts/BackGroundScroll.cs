using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    public Transform BackGround1;
    public Transform BackGround2;
    public Transform cam;

    private bool wichone = true;
    private float currenth = 1;

	void Update ()
    {
		if(currenth < cam.position.x)
        {
            if (wichone)
                BackGround1.localPosition = new Vector3((BackGround1.localPosition.x + 3), -0.5f, 0);
            else
                BackGround2.localPosition = new Vector3((BackGround2.localPosition.x + 3), -0.5f, 0);

            currenth += 1;

            wichone = !wichone;
        }
        if(currenth > cam.position.x + 1)
        {
            if (wichone)
                BackGround2.localPosition = new Vector3((BackGround2.localPosition.x - 3), -0.5f, 0);
            else
                BackGround1.localPosition = new Vector3((BackGround1.localPosition.x - 3), -0.5f, 0);

            currenth -= 1;

            wichone = !wichone;
        }
	}
}
