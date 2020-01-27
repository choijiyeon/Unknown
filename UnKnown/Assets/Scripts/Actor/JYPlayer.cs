using System.Collections;
using System.Collections.Generic;
using UIContents;
using UnityEngine;

public class JYPlayer : JYActor
{
    private bool isJumping = false;
    private float jumpPower = 160f;
    private Rigidbody2D rigid;
    private Vector3 movement;
    public JYDefines.ActorAniSpriteState playerCurState = JYDefines.ActorAniSpriteState.idle;
    private int savedplayerLifeCount;
    private Vector3 deadPosition = Vector3.zero;
    private bool isDamage = false;
    private bool isLeft = false;
  

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
            UISprite sprite = this.gameObject.transform.Find(JYDefines.ActorAniSpriteState.jump.ToString()).GetComponent<UISprite>();
            if (sprite != null)
            {
                if (isLeft == true)
                    sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                else
                    sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            }
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
            isLeft = false;
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.idle);
            playerCurState = JYDefines.ActorAniSpriteState.idle;
            ChangeCurPlayerState();
            isDamage = false;
        }
    }
    public override void DoMove(bool isRight)
    {
        if (isDamage == true) return;
        base.DoMove(isRight);

        UISprite sprite = this.gameObject.transform.Find(JYDefines.ActorAniSpriteState.run.ToString()).GetComponent<UISprite>();
        if (sprite != null)
        {
            if (isRight == true)
            {
                isLeft = false;
                sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                isLeft = true;
                sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }
                

        if (playerCurState != JYDefines.ActorAniSpriteState.run)
        {
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.run);
            playerCurState = JYDefines.ActorAniSpriteState.run;
            ChangeCurPlayerState();
        }
    }

    public override void DoJump()
    {
        base.DoJump();
        if (isJumping != true)
        {
            UISprite sprite = this.gameObject.transform.Find(JYDefines.ActorAniSpriteState.jump.ToString()).GetComponent<UISprite>();
            if (sprite != null)
            {
                if (isLeft == true)
                    sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                else
                    sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            }
            rigid.AddForce(Vector3.up * jumpPower);

            isJumping = true;
            if (playerCurState != JYDefines.ActorAniSpriteState.jump)
            {
                SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.jump);
                playerCurState = JYDefines.ActorAniSpriteState.jump;
                ChangeCurPlayerState();
            }
        }
    }

    public override void DoAttack()
    {
        base.DoAttack();
        if (playerCurState != JYDefines.ActorAniSpriteState.attack)
        {
            UISprite sprite = this.gameObject.transform.Find(JYDefines.ActorAniSpriteState.attack.ToString()).GetComponent<UISprite>();
            if (sprite != null)
            {
                if (isLeft == true)
                    sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                else
                    sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.attack);
            playerCurState = JYDefines.ActorAniSpriteState.attack;
            ChangeCurPlayerState();
        }
    }

    public override void DoDamage(float aDamageValue)
    {
        base.DoDamage(aDamageValue);
        m_Hp -= aDamageValue;

        Vector2 dieVelocity = new Vector2(-1.5f, 1f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

        StartCoroutine("DamageChangeColor");

        if (m_Hp <= 0)
        {
            //죽음.
            if ((JYGameManager.instance.playerLifeCount -1) > 0)
            {
                DoDie();
                RespawnPlayer();
                UpdatePlayerLife();
            }
            else
            {
                JYUIManager.Instance.Notify(JYDefines.UISectionFun.ShowResult, false);
            }
        }
        else
        {
            if (playerCurState != JYDefines.ActorAniSpriteState.damage)
            {
                SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.damage);
                playerCurState = JYDefines.ActorAniSpriteState.damage;
                ChangeCurPlayerState();
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
            ChangeCurPlayerState();
        }
    }

    IEnumerator DamageChangeColor()
    {
        int countTime = 0;

        UISprite sprite = this.gameObject.transform.Find(JYDefines.ActorAniSpriteState.idle.ToString()).GetComponent<UISprite>();
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

        isDamage = false;

        yield return null;
    }

    private void UpdatePlayerLife()
    {
        JYGameManager.instance.playerLifeCount--;
        JYUIManager.Instance.Notify(JYDefines.UISectionFun.RemoveLife, JYGameManager.instance.playerLifeCount);
        //JYUIManager.Instance.Notify(JYDefines.UISectionFun.RemoveLife, playerLifeCount);

    }
    private int SaveCurLifeCount(int count)
    {
        if (count == 0) savedplayerLifeCount = 3;
        savedplayerLifeCount = count;
        return savedplayerLifeCount;
    }
    private void RespawnPlayer()
    {
        JYGameManager.instance.DoActorLoad(JYDefines.ActorType.Character, "player", JYGameManager.instance.m_CharacterRoot.position);
    }

    public void ChangeCurPlayerState()
    {
        JYGameManager.instance.PlayerCurState = playerCurState;
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
        else if (other.gameObject.tag == "AttackMonster" && JYGameManager.instance.AttackMonsterCurState == JYDefines.ActorAniSpriteState.attack)
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
            if ((JYGameManager.instance.playerLifeCount - 1) > 0)
            {
                UpdatePlayerLife();
            }
            else
            {
                JYUIManager.Instance.Notify(JYDefines.UISectionFun.ShowResult, false);
            }
        }
        else if (other.gameObject.tag == "ClearZone")
        {
            JYUIManager.Instance.Notify(JYDefines.UISectionFun.ShowResult, true) ;
        }
    }
}
