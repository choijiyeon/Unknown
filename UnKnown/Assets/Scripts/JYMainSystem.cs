using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYMainSystem : MonoBehaviour
{
    //싱글턴
    static JYMainSystem _instance = null;
    public static JYMainSystem Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = FindObjectOfType<JYMainSystem>();
            }
            return _instance;
        }
    }

    public enum GameState
    {
        Title,
        Ingame,
    }
    public GameState m_gameState;

    private void Awake()
    {
        m_gameState = GameState.Title;
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

