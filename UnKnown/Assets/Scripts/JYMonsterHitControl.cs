using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYMonsterHitControl : MonoBehaviour
{
    public GameObject jyMonster;
    private JYMonster monsterSc;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            monsterSc = jyMonster.GetComponent<JYMonster>();
            if (monsterSc.isDamage == false )
            {
                monsterSc.isDamage = true;
                JYGameManager.instance.isPlayerAttack = true;
                //monsterSc.DoDamage(30f);
                monsterSc.DoDie();
            }
        }
    }
}
