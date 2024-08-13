using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    protected readonly FsmComponent m_stateMachine;
    protected bool m_enableUpdate;

    public StateBase(FsmComponent _stateMachine)
    {
        m_stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        m_enableUpdate = true;
    }

    public virtual void ManualUpdate(float _deltaTime)
    {
        if (!m_enableUpdate)
            return;
    }

    public virtual void ManualFixedUpdate(float _deltaTime)
    {
        if (!m_enableUpdate)
            return;
    }

    public virtual void Exit()
    {
        m_enableUpdate = false;
    }

    public abstract bool CheckChangeStateEnable();
    protected void SetAnimationByData(EState _state)
    {
        m_stateMachine.Entity.SkelAnim.skeleton.SetToSetupPose();

        var stateDataList = m_stateMachine.Entity.Data.StateDataList;
        foreach (var data in stateDataList)
        {
            if (data.CurrState != _state)
                continue;

            //Set animation.
            for (int i = 0; i < data.AnimList.Count; i++)
            {
                var anim = data.AnimList[i];
                for (int j = 0; j < m_stateMachine.Entity.Data.AnimDataList.Count; j++)
                {
                    var animData = m_stateMachine.Entity.Data.AnimDataList[j];
                    if (animData.Name != anim.AnimName)
                        continue;

                    Spine.MixBlend blendType = (i == 0) ? Spine.MixBlend.First : Spine.MixBlend.Add;
                    m_stateMachine.Entity.Anim.SetAnimation(anim.AnimName, i, animData.IsLoop, animData.MixDuration, animData.TimeScale, blendType, anim.Alpha);
                    break;
                }
            }

            //Set bone.
            m_stateMachine.Entity.Anim.SetBone(data.AttachBoneList);
            break;
        }
    }
}