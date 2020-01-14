using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYPlayer : JYActor
{
    private bool isJumping = false;
    private float jumpPower = 160f;
    private Rigidbody2D rigid;
    private Vector3 movement;
    private JYDefines.ActorAniSpriteState playerCurState = JYDefines.ActorAniSpriteState.idle;
    private int playerLifeCount = 3;
    private Vector3 deadPosition = Vector3.zero;
    private bool isDamage = false;
  

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();

        DoIdle();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.A))
            DoMove(false);
        if (Input.GetKeyDown(KeyCode.D))
            DoMove(true);
    }

    private void Jump()
    {
        if (isJumping != true)
        {
            rigid.AddForce(Vector3.up * 160);

            isJumping = true;

            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.jump);
        }
    }
    public override void DoIdle()
    {
        base.DoIdle();
        if (playerCurState != JYDefines.ActorAniSpriteState.idle)
        {
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.idle);
            playerCurState = JYDefines.ActorAniSpriteState.idle;
            isDamage = false;
        }
    }
    public override void DoMove(bool isRight)
    {
        base.DoMove(isRight);
        
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
                

        if (playerCurState != JYDefines.ActorAniSpriteState.run)
        {
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.run);
            playerCurState = JYDefines.ActorAniSpriteState.run;
        }
    }

    public override void DoJump()
    {
        base.DoJump();
        if (isJumping != true)
        {
            rigid.AddForce(Vector3.up * jumpPower);

            isJumping = true;
            if (playerCurState != JYDefines.ActorAniSpriteState.jump)
            {
                SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.jump);
                playerCurState = JYDefines.ActorAniSpriteState.jump;
            }
        }
    }

    public override void DoAttack()
    {
        base.DoAttack();
        if (playerCurState != JYDefines.ActorAniSpriteState.attack)
        {
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.attack);
            playerCurState = JYDefines.ActorAniSpriteState.attack;
        }
    }

    public override void DoDamage(float aDamageValue)
    {
        base.DoDamage(aDamageValue);
        m_Hp -= aDamageValue;
        if (m_Hp <= 0)
        {
            //죽음.
            if (playerLifeCount > 0)
            {
                playerLifeCount--;

                DoDie();
                RespawnPlayer();
            }
            else
            {
                //showResult;
            }
        }
        else
        {
            if (playerCurState != JYDefines.ActorAniSpriteState.damage)
            {
                SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.damage);
                playerCurState = JYDefines.ActorAniSpriteState.damage;
            }
        }
    }

    public override void DoDie()
    {
        base.DoDie();
        if (playerCurState != JYDefines.ActorAniSpriteState.dead)
        {
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.dead);
            playerCurState = JYDefines.ActorAniSpriteState.dead;
        }
    }

    private void RespawnPlayer()
    {
        JYGameManager.instance.DoActorLoad(JYDefines.ActorType.Character, "player", JYGameManager.instance.m_CharacterRoot.position);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            if (isDamage == false)
            {
                DoDamage(30f);
                isDamage = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        isJumping = false;
        if (playerCurState != JYDefines.ActorAniSpriteState.idle)
        {
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.idle);
            playerCurState = JYDefines.ActorAniSpriteState.idle;
        }
        if (other.gameObject.tag == "DeadZone")
        {
            DoDie();
            RespawnPlayer();
            if (playerLifeCount > 0)
            {
                playerLifeCount--;
            }
            else
            {
                //showResult;
            }
        }
    }
}
