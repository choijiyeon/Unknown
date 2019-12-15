using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYActor : MonoBehaviour
{

    //애니메이터
    public Animator m_ActorAnimator = null;
    protected JYDefines.ActorState m_ActorState = JYDefines.ActorState.Idle;
    protected float m_Rot = 0;
    public float m_Speed = 0.03f;
    public Transform m_SkillParent = null;
    //-------------------------------------------------------------------------
    protected JYDefines.ActorType m_ActorType = JYDefines.ActorType.None;
    int m_Level = 0;
    public float m_Defence = 0;
    public float m_Attack = 0;
    public float m_Hp = 100;
    protected enum UseType
    {
        None,
        UI,
        Ingame,
    }
    protected UseType m_useType = UseType.None;

    protected virtual void Awake()
    {
        m_ActorAnimator = this.GetComponentInChildren<Animator>();
        if (null == m_ActorAnimator)
        {
            Debug.LogError("ActorAnimator is null!!");
        }
    }

    private void Update()
    {


        //DoMove();
    }

    public virtual void DoMove()
    {
        float keyHorizontal = Input.GetAxis("Horizontal");
        float keyVertical = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * 1f * Time.smoothDeltaTime * keyHorizontal, Space.World);
        transform.Translate(Vector3.forward * 1f * Time.smoothDeltaTime * keyVertical, Space.World);
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

    public virtual void DoAttack()
    {
        m_ActorState = JYDefines.ActorState.Attack;
        m_ActorAnimator.SetInteger("animation", (int)m_ActorState);
    }

    public virtual void DoDamage(float aDamageValue)
    {
        if (m_ActorState == JYDefines.ActorState.Damage && m_ActorState == JYDefines.ActorState.Die)
            return;

        m_Hp -= aDamageValue;
        if (m_Hp <= 0)
        {
            //죽음.
            DoDie();
        }
        else
        {
            //m_ActorState = JYDefines.ActorState.Damage;
            //m_ActorAnimator.SetInteger("animation", (int)m_ActorState);
            StopCoroutine("SetState");
            StartCoroutine("SetState", JYDefines.ActorState.Damage);
        }
    }

    IEnumerator SetState(JYDefines.ActorState aActorState)
    {
        m_ActorState = aActorState;
        m_ActorAnimator.SetInteger("animation", (int)m_ActorState);

        if (m_ActorState != JYDefines.ActorState.Damage)
        {
            yield break;
        }

        AnimatorStateInfo asi = m_ActorAnimator.GetCurrentAnimatorStateInfo((int)m_ActorState);
        yield return new WaitForSeconds(asi.length);

        m_ActorState = JYDefines.ActorState.Idle;
        m_ActorAnimator.SetInteger("animation", (int)m_ActorState);
    }

    public virtual IEnumerator DoDie()
    {
        m_ActorState = JYDefines.ActorState.Die;
        m_ActorAnimator.SetInteger("animation", (int)m_ActorState);
        yield return null;
        DestroyObject(gameObject);
    }

    public void Call_DoIdle()
    {
        Debug.Log("Call_DoIdle");
        m_ActorState = JYDefines.ActorState.Idle;
        m_ActorAnimator.SetInteger("animation", (int)m_ActorState);
    }
}
