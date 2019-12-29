using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYPlayer : JYActor
{
    private bool isJumping = false;
    private float jumpPower = 85f;
    private Rigidbody2D rigid;
    private Vector3 movement;
    private JYDefines.ActorAniSpriteState playerCurState = JYDefines.ActorAniSpriteState.idle;

  

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
       // DoMove();
    }

    private void Move()
    {
        //Vector3 moveVelocity = Vector3.zero;

        //if (Input.GetAxisRaw("Horizontal") < 0)
        //    moveVelocity = Vector3.left;
        //else if (Input.GetAxisRaw("Horizontal") > 0)
        //    moveVelocity = Vector3.right;

        //transform.position += moveVelocity * movePower * Time.deltaTime;
        //mainCamera.position += transform.position;
    }

    private void Jump()
    {
        if (isJumping != true)
        {
            rigid.AddForce(Vector3.up * jumpPower);

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
    private void OnCollisionEnter2D()
    {
        isJumping = false;
        if (playerCurState != JYDefines.ActorAniSpriteState.idle)
        {
            SetACurrentAniSprite(this.gameObject, JYDefines.ActorAniSpriteState.idle);
            playerCurState = JYDefines.ActorAniSpriteState.idle;
        }
    }
}
