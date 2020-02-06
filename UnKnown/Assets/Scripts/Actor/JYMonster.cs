using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYMonster : JYActor
{
    [SerializeField]
    public MonsterType monsterType;
    public bool isDamage = false;

    private float monMovePower = 0.2f;
    private JYDefines.ActorAniSpriteState monsterCurState = JYDefines.ActorAniSpriteState.idle;
    private MoveDirection curDirection = MoveDirection.RIGHT;
    private Transform dirLeft;
    private Transform dirRight;
    private Transform attackerDirLeft;
    private Transform attackerDirRight;
    private Transform invincivilityDirLeft;
    private Transform invincivilityDirRight;
    private GameObject traceTarget;
    private bool isAttack = false;
    private bool isDead = false;

    enum MoveDirection
    {
        LEFT,
        RIGHT,
    }
    public enum MonsterType
    {
        MOVER,
        ATTACKER,
        INVINCIVILITY,
    }
    void Update()
    {
        switch(monsterType)
        {
            case MonsterType.MOVER:
            case MonsterType.INVINCIVILITY:
                Move();
                break;
            case MonsterType.ATTACKER:
                {
                    if(traceTarget == null)
                        traceTarget = this.transform.parent.parent.Find("CharacterRoot/player(Clone)").gameObject;

                    if (traceTarget != null)
                    {
                        Vector3 playerPos = traceTarget.transform.position;
                        float distance = Vector3.Distance(playerPos, this.transform.position);
                        if (distance <= 0.5f)
                        {
                            isAttack = true;
                            DoAttack();
                        }
                        else
                        {
                            isAttack = false;
                            Move();
                        }
                    }
                }
                break;
        }
    }
    private void Start()
    {
        traceTarget = this.transform.parent.parent.Find("CharacterRoot/player(Clone)").gameObject;

        switch (monsterType)
        {
            case MonsterType.MOVER:
                {
                    for (int i = 0; i < JYGameManager.instance.m_MonsterPos.Length; i++)
                    {
                        if (this.gameObject.name == i.ToString())
                        {
                            dirLeft = JYGameManager.instance.m_MonsterPos[i].gameObject.transform.Find("Left").transform;
                            dirRight = JYGameManager.instance.m_MonsterPos[i].gameObject.transform.Find("Right").transform;
                        }
                    }
                }
                break;
            case MonsterType.ATTACKER:
                {
                    for (int i = 0; i < JYGameManager.instance.m_AttackMonsterPos.Length; i++)
                    {
                        if (this.gameObject.name == i.ToString())
                        {
                            attackerDirLeft = JYGameManager.instance.m_AttackMonsterPos[i].gameObject.transform.Find("Left").transform;
                            attackerDirRight = JYGameManager.instance.m_AttackMonsterPos[i].gameObject.transform.Find("Right").transform;
                        }
                    }
                }
                break;
            case MonsterType.INVINCIVILITY:
                {
                    for (int i = 0; i < JYGameManager.instance.m_InvincibilityMonsterPos.Length; i++)
                    {
                        if (this.gameObject.name == i.ToString())
                        {
                            invincivilityDirLeft = JYGameManager.instance.m_InvincibilityMonsterPos[i].gameObject.transform.Find("Left").transform;
                            invincivilityDirRight = JYGameManager.instance.m_InvincibilityMonsterPos[i].gameObject.transform.Find("Right").transform;
                        }
                    }
                }
                break;
        }

    }
    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (monsterCurState == JYDefines.ActorAniSpriteState.dead) return;
        if (isAttack == true && isDamage == true && isDead == true) return;
        if (curDirection == MoveDirection.RIGHT)
        {
            moveVelocity = Vector3.right;
            DoMove(false);
        }
        else if (curDirection == MoveDirection.LEFT)
        {
            moveVelocity = Vector3.left;
            DoMove(true);
        }

        transform.position += moveVelocity * monMovePower * 0.11f;

       
        switch (monsterType)
        {
            case MonsterType.MOVER:
                {
                    if (transform.position.x <= dirLeft.position.x)
                        curDirection = SetDirection(curDirection);
                    if (transform.position.x >= dirRight.position.x)
                        curDirection = SetDirection(curDirection);
                }
                break;
            case MonsterType.ATTACKER:
                {
                    if (transform.position.x <= attackerDirLeft.position.x)
                        curDirection = SetDirection(curDirection);
                    if (transform.position.x >= attackerDirRight.position.x)
                        curDirection = SetDirection(curDirection);
                }
                break;
            case MonsterType.INVINCIVILITY:
                {
                    if (transform.position.x <= invincivilityDirLeft.position.x)
                        curDirection = SetDirection(curDirection);
                    if (transform.position.x >= invincivilityDirRight.position.x)
                        curDirection = SetDirection(curDirection);
                }
                break;
        }
    }

    
    public override void DoIdle()
    {
        base.DoIdle();
        if (monsterCurState != JYDefines.ActorAniSpriteState.idle)
        {
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.idle);
            monsterCurState = JYDefines.ActorAniSpriteState.idle;
            ChangeCurMonsterState();
        }
    }
    public override void DoMove(bool isRight)
    {
        if (isDamage == true && isDead == true) return;
        UISprite sprite = this.gameObject.transform.Find(JYDefines.ActorAniSpriteState.run.ToString()).GetComponent<UISprite>();
        if (sprite != null)
        {
            if (isRight == true)
                sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            else
                sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (monsterCurState != JYDefines.ActorAniSpriteState.run)
        {
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.run);
            monsterCurState = JYDefines.ActorAniSpriteState.run;
            ChangeCurMonsterState();
        }
    }
    private MoveDirection SetDirection(MoveDirection direction)
    {
        if (direction == MoveDirection.LEFT)
            return MoveDirection.RIGHT;
        else if (direction == MoveDirection.RIGHT)
            return MoveDirection.LEFT;
        else
            return MoveDirection.RIGHT;
    }
    public override void DoAttack()
    {
        base.DoAttack();
        if (monsterCurState != JYDefines.ActorAniSpriteState.attack)
        {
            UISprite sprite = this.gameObject.transform.Find(JYDefines.ActorAniSpriteState.attack.ToString()).GetComponent<UISprite>();
            if (sprite != null)
            {
                if (curDirection == MoveDirection.RIGHT)
                    sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                else
                    sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.attack);
            monsterCurState = JYDefines.ActorAniSpriteState.attack;
            ChangeCurMonsterState();
        }
    }
    public override void DoDamage(float aDamageValue)
    {
        base.DoDamage(aDamageValue);
        if (this.gameObject.tag == "Monster")
        {
            DoDie();
        }
        else
        {
            m_Hp -= aDamageValue;
            StartCoroutine("DamageChangeColor");

            if (m_Hp <= 0)
            {
                //죽음.
                DoDie();
            }
            else
            {
                if (monsterCurState != JYDefines.ActorAniSpriteState.damage)
                {
                    SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.damage);

                    monsterCurState = JYDefines.ActorAniSpriteState.damage;
                    ChangeCurMonsterState();
                    Invoke("ChangeDamageState", 1.5f);
                }
            }
        }
    }

    private void ChangeDamageState()
    {
        isDamage = false;
    }
    public override void DoDie()
    {
        isDead = true;
        base.DoDie();
        if (monsterCurState != JYDefines.ActorAniSpriteState.dead)
        {
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.dead);
            monsterCurState = JYDefines.ActorAniSpriteState.dead;
            ChangeCurMonsterState();

            Invoke("GoDestroy", 1.5f);
        }
    }

    private void GoDestroy()
    {
        Destroy(this.gameObject);
    }
    IEnumerator DamageChangeColor()
    {
        int countTime = 0;

        UISprite sprite = this.gameObject.transform.Find(JYDefines.ActorAniSpriteState.damage.ToString()).GetComponent<UISprite>();
        while (countTime < 6)
        {
            if (countTime % 2 == 0)
                sprite.alpha = 0.2f;
            else
                sprite.alpha = 1f;

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        sprite.alpha = 1f;

        yield return new WaitForSeconds(1f);

        isDamage = false;

        yield return null;
    }
    public void ChangeCurMonsterState()
    {
        JYGameManager.instance.AttackMonsterCurState = monsterCurState;
    }
}
