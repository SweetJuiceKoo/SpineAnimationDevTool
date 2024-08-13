using EasyButtons;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public enum EState
{
    None,
    Idle,
    MousePress,
    MouseRelease,
}

[Serializable]
public class AnimData
{
    [ReadOnly] public string Name;

    [Space]
    public bool IsLoop;
    [Range(0, 1f)] public float MixDuration = 0.2f;
    [Range(0, 2f)] public float TimeScale = 1f;
    public Spine.Animation Animation;
}

[Serializable]
public class StateData
{
    [ReadOnly] public string StateName;

    [Header("State")]
    [ReadOnly] public EState CurrState;

    [Header("Animation")]
    public List<StateAnimData> AnimList;
    public List<string> AttachBoneList;
}

[Serializable]
public class StateAnimData
{
    public string AnimName;
    [Range(0, 1f)] public float Alpha = 1f;
}

[Serializable, CreateAssetMenu(fileName = "EntityData@NewData", menuName = "[ScriptableObject]/Data/Entity", order = 1)]
public class SODataEntity : ScriptableObject
{
    public SkeletonDataAsset SkelDataAsset;
    public List<AnimData> AnimDataList = new();
    public List<StateData> StateDataList = new();

    [Button]
    public void SetAnimData()
    {
        AnimDataList.Clear();
        if (SkelDataAsset == null)
            return;

        var itemList = SkelDataAsset.GetSkeletonData(true).Animations.Items;
        foreach (var item in itemList)
        {
            var animData = new AnimData();
            animData.Name = item.Name;
            animData.Animation = item;
            AnimDataList.Add(animData);
        }
    }

    [Button]
    public void SetStateData()
    {
        StateDataList.Clear();
        for (int i = 1; i < Enum.GetValues(typeof(EState)).Length; i++)
        {
            var state = (EState)i;
            var stateData = new StateData();
            stateData.StateName = state.ToString();
            stateData.CurrState = state;

            StateDataList.Add(stateData);
        }
    }
}