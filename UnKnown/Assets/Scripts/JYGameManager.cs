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

    private Vector3[] m_monsterCreatePos = new Vector3[5];
    private const float m_mosterPos = 7f;

    void Start()
    {
        JYMainSystem.Instance.m_gameState = JYMainSystem.GameState.Ingame;
        GameStart();
        GameLoading();
    }

    void Update()
    {
        if (m_CurrentState == JJgGameState.Start && m_firstMonsterCreate == false)
        {
            m_MonsterSpawnTime -= Time.deltaTime;
            if ((int)m_MonsterSpawnTime <= 0)
            {
                DoCraeteMonsterByWave(1);
                m_firstMonsterCreate = true;
            }
        }
    }

    void GameLoading()
    {
        //JYUIManager.Instance.Initialize();
        DoActorLoad(JYDefines.ActorType.Character, "player", new Vector3(-1.5f, 0f,0f));
    }

    void GameStart()
    {
        m_CurrentState = JJgGameState.Start;
    }

    void GameOver()
    {
        m_CurrentState = JJgGameState.End;

    }

    public void DoActorLoad(JYDefines.ActorType aActorType, string aResName, Vector3 aPos)
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
                    GameObject go = Resources.Load("Monster/" + aResName) as GameObject;
                    if (null != go)
                    {
                        GameObject actor = GameObject.Instantiate(go, aPos, Quaternion.identity, m_MonsterRoot);
                        //JYMonster character = actor.AddComponent<JYMonster>();
                        //character.DoCreate();
                    }
                }
                break;
        }
    }

    //-------------------------------------------------------------------------
    //몬스터 생성 및 포지션 셋팅
    public void CreateMonsterPosition()
    {
        //나중에 맵 사이즈를 계산해서 임의로 포지션 값 받아와서 처리( 내 캐릭터랑 겹치지 않도록) 
    }

    public void DoCraeteMonsterByWave(int aWave)
    {
        //if (m_MonsterCreateCtrl.m_MonsterCreateDataList.Count >= aWave)
        //{
        //    JYMonsterData.MonsterCreateData createData = m_MonsterCreateCtrl.m_MonsterCreateDataList[aWave - 1];
        //    if (null != createData)
        //    {
        //        foreach (JYMonsterData.MonsterCreateData.PosMonsterData posMonsterData in createData.m_PosMonsterDataList)
        //        {
        //            for (int i = 0; i < posMonsterData.m_MonsterCount; i++)
        //            {
        //                //포지션 기준으로 랜덤하게 뿌리기
        //                float randomPos = Random.Range(1f, m_mosterPos);
        //                DoActorLoad(JYDefines.ActorType.Monster, posMonsterData.m_MonsterResName,
        //                    new Vector3(createData.m_CreatePos.position.x + randomPos, createData.m_CreatePos.position.y, createData.m_CreatePos.position.z + randomPos));
        //            }
        //        }
        //    }
        //}
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
