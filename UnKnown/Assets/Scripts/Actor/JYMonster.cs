using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYMonster : JYActor
{
    private Rigidbody2D rigid;
    private Vector3 movement;
    private bool isTracing;
    private float movePower = 0.2f;
    private JYDefines.ActorAniSpriteState monsterCurState = JYDefines.ActorAniSpriteState.idle;
    private MoveDirection curDirection = MoveDirection.RIGHT;
    private Transform dirLeft;
    private Transform dirRight;

    enum MoveDirection
    {
        LEFT,
        RIGHT,

    }
    void Update()
    {
        Move();
    }
    private void Start()
    {
        switch (this.gameObject.name)
        {
            case "0":
                {
                    dirLeft = JYGameManager.instance.m_MonsterPos[0].gameObject.transform.Find("Left").transform;
                    dirRight = JYGameManager.instance.m_MonsterPos[0].gameObject.transform.Find("Right").transform;
                }
                break;
            case "1":
                {
                    dirLeft = JYGameManager.instance.m_MonsterPos[1].gameObject.transform.Find("Left").transform;
                    dirRight = JYGameManager.instance.m_MonsterPos[1].gameObject.transform.Find("Right").transform;
                }
                break;
            case "2":
                {
                    dirLeft = JYGameManager.instance.m_MonsterPos[2].gameObject.transform.Find("Left").transform;
                    dirRight = JYGameManager.instance.m_MonsterPos[2].gameObject.transform.Find("Right").transform;
                }
                break;
            case "3":
                {
                    dirLeft = JYGameManager.instance.m_MonsterPos[3].gameObject.transform.Find("Left").transform;
                    dirRight = JYGameManager.instance.m_MonsterPos[3].gameObject.transform.Find("Right").transform;
                }
                break;
            case "4":
                {
                    dirLeft = JYGameManager.instance.m_MonsterPos[4].gameObject.transform.Find("Left").transform;
                    dirRight = JYGameManager.instance.m_MonsterPos[4].gameObject.transform.Find("Right").transform;
                }
                break;
        }
    }
    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

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

        transform.position += moveVelocity * movePower * Time.deltaTime;

        if(transform.position.x <= dirLeft.position.x)
            curDirection = SetDirection(curDirection);
        if (transform.position.x >= dirRight.position.x)
            curDirection = SetDirection(curDirection);
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
            {
                sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
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

    //void OnTriggerEnter2D(Collider2D other)
    //{

    //    if (other.gameObject.tag == "Player")
    //    {
    //        traceTarget = other.gameObject;

    //        StopCoroutine("ChangeMovement");
    //    }
    //}
    //void OnTriggerStay2D(Collider2D other)
    //{

    //    if (other.gameObject.tag == "Player")
    //    {
    //        isTracing = true;


    //    }


    //}
    //void OnTriggerExit2D(Collider2D other)
    //{

    //    if (other.gameObject.tag == "Player")
    //    {
    //        isTracing = false;
    //        Move();
    //    }
    //}
}
