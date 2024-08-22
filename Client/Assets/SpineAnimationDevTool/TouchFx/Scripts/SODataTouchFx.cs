using Darkside;
using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EFxState
{
    Idle,
    PressIdle,
    PressDown,
    PressUp,
}

[Serializable, CreateAssetMenu(fileName = "TouchFxData@NewData", menuName = "[ScriptableObject]/Data/TouchFx", order = 2)]
public class SODataTouchFx : ScriptableObject
{
    public SerializedDictionary<EFxState, GameObject> ObjFxDict = new();

    [Button]
    public void ResetFxData()
    {
        ObjFxDict.Clear();
        for (int i = 0; i < Enum.GetValues(typeof(EFxState)).Length; i++)
            ObjFxDict.Add((EFxState)i, null);
    }
}