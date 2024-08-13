using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmComponent
{
    #region[Field]
    [SerializeField, ReadOnly] private EState m_currState;
    [SerializeField, ReadOnly] private readonly Dictionary<EState, StateBase> m_behaviourDict = new();
    private StateBase m_currBehaviour;

    public EState CurrState => m_currState;
    public StateBase CurrBehaviour => m_currBehaviour;
    public SpineEntity Entity;
    #endregion

    #region[Cycle]
    public FsmComponent(SpineEntity _entity)
    {
        Entity = _entity;
        SetBehaviour(_entity.Data);
        ChangeState(EState.Idle, true);

        //Entity.SkelAnim.AnimationState.Complete += AnimationState_Complete;
    }

    public void ManualUpdate(float _deltaTime)
    {
        m_currBehaviour?.ManualUpdate(_deltaTime);
    }

    public void ManualFixedUpdate(float _deltaTime)
    {
        m_currBehaviour?.ManualFixedUpdate(_deltaTime);
    }
    #endregion

    #region[Method]
    public void ChangeState(EState _nextState, bool _forceChange = false)
    {
        if (m_currState == _nextState)
            return;

        if (_forceChange == false)
        {
            //Check change state enable.
            if (m_currBehaviour?.CheckChangeStateEnable() == false)
                return;
        }

        //Check next state exist.
        if (m_behaviourDict.TryGetValue(_nextState, out var value) == false)
            return;

        //Exit current state.
        m_currBehaviour?.Exit();

        //Set next state.
        m_currState = _nextState;
        m_currBehaviour = value;

        //Start next state.
        m_currBehaviour?.Enter();
    }

    private void SetBehaviour(SODataEntity _data)
    {
        if (_data == null)
            return;

        foreach (var state in _data.StateDataList)
        {
            switch (state.CurrState)
            {
                case EState.Idle:
                    m_behaviourDict.TryAdd(state.CurrState, new StateIdle(this));
                    break;
                case EState.MousePress:
                    m_behaviourDict.TryAdd(state.CurrState, new StateMousePress(this));
                    break;
                case EState.MouseRelease:
                    m_behaviourDict.TryAdd(state.CurrState, new StateMouseRelease(this));
                    break;

                default:
                    break;
            }
        }
    }

    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        if (m_currState != EState.MouseRelease)
            return;

        ChangeState(EState.Idle);
    }
    #endregion
}