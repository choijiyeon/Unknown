﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYMonster : JYActor
{
    public MonsterType monsterType;
    private Rigidbody2D rigid;
    private Vector3 movement;
    private bool isTracing;
    private float movePower = 0.2f;
    private JYDefines.ActorAniSpriteState monsterCurState = JYDefines.ActorAniSpriteState.idle;
    private MoveDirection curDirection = MoveDirection.RIGHT;
    private Transform dirLeft;
    private Transform dirRight;
    private Transform attackerDirLeft;
    private Transform attackerDirRight;
    private GameObject traceTarget;
    private int movementFlag = 0;

    private bool isAttack = false;

    enum MoveDirection
    {
        LEFT,
        RIGHT,
    }
    public enum MonsterType
    {
        MOVER,
        ATTACKER,
    }
    void Update()
    {
        switch(monsterType)
        {
            case MonsterType.MOVER:
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
        }

    }
    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        //if(isAttack == true && monsterType == MonsterType.ATTACKER)
        //{
        //    Vector3 playerPos = traceTarget.transform.position;

        //    if (playerPos.x < transform.position.x)
        //        curDirection = MoveDirection.LEFT;
        //    else if (playerPos.x > transform.position.x)
        //        curDirection = MoveDirection.RIGHT;

        //    DoAttack();
        //}
        if (isAttack == true) return;
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

        transform.position += moveVelocity * movePower * 0.11f;

       
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
        }
    }

    
    public override void DoIdle()
    {
        base.DoIdle();
        if (monsterCurState != JYDefines.ActorAniSpriteState.idle)
        {
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.idle);
            monsterCurState = JYDefines.ActorAniSpriteState.idle;
        }
    }
    public override void DoMove(bool isRight)
    {
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
        }
    }
    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        isAttack = true;
    //        traceTarget = other.gameObject;
    //    }
    //}
    //void OnCollisionStay2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        isAttack = true;
    //        traceTarget = other.gameObject;
    //    }


    //}
    //void OnCollisionExit2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        isAttack = false;
    //        traceTarget = other.gameObject;
    //    }
    //}
}
