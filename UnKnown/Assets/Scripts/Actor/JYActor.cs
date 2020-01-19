using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYActor : MonoBehaviour
{
    protected JYDefines.ActorState m_ActorState = JYDefines.ActorState.Idle;
    protected float m_Rot = 0;
    public float m_Speed = 0.03f;

    private float movePower = 0.7f;
    //-------------------------------------------------------------------------
    protected JYDefines.ActorType m_ActorType = JYDefines.ActorType.None;
    int m_Level = 0;
    public float m_Defence = 0;
    public float m_Attack = 0;
    public float m_Hp = 90;
    protected enum UseType
    {
        None,
        UI,
        Ingame,
    }
    protected UseType m_useType = UseType.None;

    protected virtual void Awake()
    {
    }

    private void Update()
    {


        //DoMove();
    }

    public virtual void DoMove(bool isRight)
    {
        Vector3 moveVelocity = Vector3.zero;

        if (isRight == true)
            moveVelocity = Vector3.right;
        else if (isRight == false)
            moveVelocity = Vector3.left;

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    public virtual void DoCreate()
    {
        switch (JYMainSystem.Instance.m_gameState)
        {
            case JYMainSystem.GameState.Title:
                m_useType = UseType.UI;
                return;
            case JYMainSystem.GameState.Ingame:
                m_useType = UseType.Ingame;
                break;
        }
    }

    public virtual void DoIdle()
    {

    }

    public virtual void DoWalk()
    {

    }
    public virtual void DoJump()
    {

    }

    public virtual void DoAttack()
    {
        m_ActorState = JYDefines.ActorState.Attack;
    }

    public virtual void DoDamage(float aDamageValue)
    {
        if (m_ActorState == JYDefines.ActorState.Damage && m_ActorState == JYDefines.ActorState.Die)
            return;
    }

    IEnumerator SetState(JYDefines.ActorState aActorState)
    {
        m_ActorState = aActorState;

        if (m_ActorState != JYDefines.ActorState.Damage)
        {
            yield break;
        }

       // AnimatorStateInfo asi = m_ActorAnimator.GetCurrentAnimatorStateInfo((int)m_ActorState);
       // yield return new WaitForSeconds(asi.length);

        m_ActorState = JYDefines.ActorState.Idle;
       // m_ActorAnimator.SetInteger("animation", (int)m_ActorState);
    }

    public virtual void DoDie()
    {
        m_ActorState = JYDefines.ActorState.Die;
        //m_ActorAnimator.SetInteger("animation", (int)m_ActorState);
        Destroy(gameObject);
    }

    public void Call_DoIdle()
    {
        Debug.Log("Call_DoIdle");
        m_ActorState = JYDefines.ActorState.Idle;
        //m_ActorAnimator.SetInteger("animation", (int)m_ActorState);
    }

    public void SetACurrentAniSprite(GameObject targetObj, JYDefines.ActorAniSpriteState state)
    {
        UISprite[] sprites = targetObj.transform.GetComponentsInChildren<UISprite>();
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].gameObject.SetActive(false);
        }

        UISprite sprite = targetObj.transform.Find(state.ToString()).GetComponent<UISprite>();
        if (sprite != null )
            sprite.gameObject.SetActive(true);
    }


}
