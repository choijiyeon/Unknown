//전체적인 인게임 시스템.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYGameManager : MonoBehaviour
{
    //-------------------------------------------------------------------------
    //싱글턴
    //-------------------------------------------------------------------------
    static JYGameManager m_Instance = null;

    public static JYGameManager instance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = FindObjectOfType<JYGameManager>();
            }
            return m_Instance;
        }
    }

    //-------------------------------------------------------------------------
    //캐릭터 루트.
    public Transform m_CharacterRoot = null;
    public Transform m_MonsterRoot = null;
    public GameObject[] m_MonsterPos = new GameObject[4];
    public Transform m_AttackMonsterRoot = null;
    public GameObject[] m_AttackMonsterPos = new GameObject[1];

    //-------------------------------------------------------------------------
    //몬스터 생성 컨트롤러.
    //public JJgMonsterData m_MonsterCreateCtrl = null;
    int m_Wave = 0;

    //-------------------------------------------------------------------------
    //현재 게임 상태
    public enum JJgGameState
    {
        None,
        Waiting,
        Start,
        End,
    }
    private JJgGameState m_CurrentState;

    //-------------------------------------------------------------------------
    //현재 게임 시작 관련 변수
    private float m_MonsterSpawnTime = 5f;
    private bool m_firstMonsterCreate = false;
    //-------------------------------------------------------------------------
    [HideInInspector]
    public JYPlayer m_MainActor = null; //유저.
    public int playerLifeCount = 3;

    public JYDefines.ActorAniSpriteState PlayerCurState = JYDefines.ActorAniSpriteState.idle;
    public JYDefines.ActorAniSpriteState AttackMonsterCurState = JYDefines.ActorAniSpriteState.idle;

    void Start()
    {
        JYMainSystem.Instance.m_gameState = JYMainSystem.GameState.Ingame;
        GameStart();
        GameLoading();
    }

    void GameLoading()
    {
        DoActorLoad(JYDefines.ActorType.Character, "player", m_CharacterRoot.position);
        for (int i = 0; i < m_MonsterPos.Length; i++)
        {
            DoActorLoad(JYDefines.ActorType.Monster, "monster", m_MonsterPos[i].gameObject.transform.position, i);
        }
        for (int i = 0; i < m_AttackMonsterPos.Length; i++)
        {
            DoActorLoad(JYDefines.ActorType.AttackMonster, "monster01", m_AttackMonsterPos[i].gameObject.transform.position, i);
        }
    }

    void GameStart()
    {
        m_CurrentState = JJgGameState.Start;
    }

    void GameOver()
    {
        m_CurrentState = JJgGameState.End;

    }

    public void DoActorLoad(JYDefines.ActorType aActorType, string aResName, Vector3 aPos, int index = 0)
    {
        //액터 생성하기.
        //(임시)
        switch (aActorType)
        {
            case JYDefines.ActorType.Character:
                {
                    GameObject go = Resources.Load("Character/" + aResName) as GameObject;
                    if (null != go)
                    {
                        GameObject actor = GameObject.Instantiate(go, aPos, Quaternion.identity, m_CharacterRoot);
                        JYPlayer character = actor.AddComponent<JYPlayer>();
                        m_MainActor = character;
                        character.DoCreate();

                    }
                }
                break;
            case JYDefines.ActorType.Monster:
                {
                    GameObject go = Resources.Load("Character/" + aResName) as GameObject;
                    if (null != go)
                    {
                        GameObject actor = GameObject.Instantiate(go, aPos, Quaternion.identity, m_MonsterRoot);
                        actor.name = index.ToString();
                        JYMonster character = actor.AddComponent<JYMonster>();
                        character.monsterType = JYMonster.MonsterType.MOVER;
                        character.DoCreate();
                    }
                }
                break;
            case JYDefines.ActorType.AttackMonster:
                {
                    GameObject go = Resources.Load("Character/" + aResName) as GameObject;
                    if (null != go)
                    {
                        GameObject actor = GameObject.Instantiate(go, aPos, Quaternion.identity, m_AttackMonsterRoot);
                        actor.name = index.ToString();
                        JYMonster character = actor.AddComponent<JYMonster>();
                        character.monsterType = JYMonster.MonsterType.ATTACKER;
                        character.DoCreate();
                    }
                }
                break;
        }
    }


    //-------------------------------------------------------------------------
    //게임 상태에 관련된 함수
    public JJgGameState GetCurrentState()
    {
        return m_CurrentState;
    }

    public void ChangeCurrentGameState(JJgGameState currentState)
    {
        m_CurrentState = currentState;
    }
}
