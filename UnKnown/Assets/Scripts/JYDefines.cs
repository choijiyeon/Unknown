using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYDefines
{
    public const string mainCamera = "MainCamera";

    public enum Layer
    {
        PLAYER = 8,
        MONSER = 9,
        BULLET = 10,
    }

    public enum ActorState
    {
        Idle = 0,
        Walk = 13,
        Damage = 7,
        Attack = 9,
        Die
    }
    public enum ActorAniSpriteState
    {
        idle = 0,
        run,
        damage,
        attack,
        dead,
        jump
    }

    public enum ActorType
    {
        None,
        Character,
        Monster,
    }

    public enum UISection
    {
        None,
        UIStart,
        UIMain,
        UIResult,
    }
    public enum UISectionFun
    {
        None,
        InitUIData,
        RemoveLife,
        ShowResult,
    }
}
