using System.Collections;
using System.Collections.Generic;
using UIContents;
using UnityEngine;

public class JYPlayer : JYActor
{
    private bool isJumping = false;
    private float jumpPower = 160f;
    private Rigidbody2D rigid;
    public JYDefines.ActorAniSpriteState playerCurState = JYDefines.ActorAniSpriteState.idle;
    private int savedplayerLifeCount;
    private bool isDamage = false;
    private bool isLeft = false;
    private bool isDestroy = false;
    private bool isUnrivaled = false;
    private bool isMoveland = false;
    private GameObject moveLandObj;

    private Vector3 accVlaue = Vector3.zero;
    private Vector3 curPos = Vector3.zero;
    private Vector3 prevPos = Vector3.zero;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        DoIdle();
        if(JYGameManager.instance.currentMode == JYDefines.CurrentMode.easy)
            moveLandObj = this.transform.parent.parent.Find("Stage01/Field01/Bg_Down/Moveland").gameObject;
        else if (JYGameManager.instance.currentMode == JYDefines.CurrentMode.normal)
            moveLandObj = this.transform.parent.parent.Find("Stage02/Field05/Bg_Down/Land2").gameObject;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            Jump();
    }

    private void FixedUpdate()
    {
        Move();

        if(JYGameManager.instance.isPlayerAttack == true)
        {
            Vector2 dieVelocity = new Vector2(1f, 0.9f);

            if (isLeft == true)
                dieVelocity = new Vector2(-1f, 0.9f);
            else
                dieVelocity = new Vector2(1f, 0.9f);

            rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

            JYGameManager.instance.isPlayerAttack = false;
        }

        if (moveLandObj != null)
        {
            curPos = moveLandObj.transform.position;
            accVlaue = (curPos - prevPos) / Time.deltaTime;
            prevPos = curPos;
        }
        if(isMoveland == true)
        {
            this.gameObject.transform.position += accVlaue * Time.deltaTime;
        }
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
        isMoveland = false;
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
            isJumping = true;
            rigid.AddForce(Vector3.up * 160);

            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.jump);
        }
    }
    public override void DoIdle()
    {
        base.DoIdle();
        if (playerCurState != JYDefines.ActorAniSpriteState.idle)
        {
            UISprite sprite = this.gameObject.transform.Find(JYDefines.ActorAniSpriteState.idle.ToString()).GetComponent<UISprite>();
            if (sprite != null)
            {
                if (isLeft == true)
                    sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                else
                    sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            }
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.idle);
            playerCurState = JYDefines.ActorAniSpriteState.idle;
            ChangeCurPlayerState();
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
        isMoveland = false;
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
            isJumping = true;
            rigid.AddForce(Vector3.up * jumpPower);

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
        if (isUnrivaled == true) return;
        base.DoDamage(aDamageValue);

        Vector2 dieVelocity = new Vector2(1f, 0.9f);

        if (isLeft == true)
            dieVelocity = new Vector2(1f, 0.9f);
        else
            dieVelocity = new Vector2(-1f, 0.9f);

        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

        StartCoroutine("DamageChangeColor");

        //죽음.
        if ((JYGameManager.instance.playerLifeCount -1) > 0)
        {
            isUnrivaled = true;
            DoDie();
            //RespawnPlayer();
            UpdatePlayerLife();

            if (playerCurState != JYDefines.ActorAniSpriteState.damage)
            {
                SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.damage);
                playerCurState = JYDefines.ActorAniSpriteState.damage;
                ChangeCurPlayerState();
                Invoke("ChangeDamageState", 2.5f);
            }
        }
        else
        {
            JYUIManager.Instance.Notify(JYDefines.UISectionFun.ShowResult, false);
        }
    }
    private void ChangeDamageState()
    {
        isDamage = false;
        isUnrivaled = false;
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
        if(isDestroy == true)
            Destroy(this.gameObject);
    }

    IEnumerator DamageChangeColor()
    {
        int countTime = 0;

        UISprite sprite = this.gameObject.transform.Find(JYDefines.ActorAniSpriteState.idle.ToString()).GetComponent<UISprite>();
        UISprite spritemove = this.gameObject.transform.Find(JYDefines.ActorAniSpriteState.run.ToString()).GetComponent<UISprite>();
        while (countTime < 6)
        {
            if (countTime % 2 == 0)
            {
                sprite.alpha = 0.2f;
                spritemove.alpha = 0.2f;
            }
            else
            {
                sprite.alpha = 1f;
                spritemove.alpha = 1f;
            }

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        sprite.alpha = 1f;
        spritemove.alpha = 1f;

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
        if (count == 0) savedplayerLifeCount = 5;
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
                isUnrivaled = true;
            }
        }
        else if (other.gameObject.tag == "AttackMonsterAttack" && JYGameManager.instance.AttackMonsterCurState == JYDefines.ActorAniSpriteState.attack)
        {
            if (isDamage == false)
            {
                DoDamage(30f);
                isDamage = true;
                isUnrivaled = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.transform.localPosition.y <= (this.transform.transform.localPosition.y + 1.5f)
            && other.gameObject.tag != "Monster" && other.gameObject.tag != "AttackMonster")
        {
            isJumping = false;
        }

        if (playerCurState != JYDefines.ActorAniSpriteState.idle)
        {
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.idle);
            playerCurState = JYDefines.ActorAniSpriteState.idle;
        }
        if (other.gameObject.tag == "DeadZone")
        {
            isDestroy = true;
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
        else if(other.gameObject.tag == "Moveland")
        {
            isMoveland = true;
            moveLandObj = other.gameObject;
        }
    }
}
