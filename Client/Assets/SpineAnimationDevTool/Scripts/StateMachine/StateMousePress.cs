using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMousePress : StateBase
{
    #region[Field]
    #endregion

    #region[Cycle]
    public StateMousePress(FsmComponent _stateMachine) : base(_stateMachine)
    {
    }
    #endregion

    #region[Method]
    #endregion
    public override void Enter()
    {
        base.Enter();
        SetAnimationByData(EState.MousePress);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void ManualFixedUpdate(float _deltaTime)
    {
        base.ManualFixedUpdate(_deltaTime);
    }

    public override void ManualUpdate(float _deltaTime)
    {
        base.ManualUpdate(_deltaTime);

        if (Input.GetMouseButtonUp(0))
            m_stateMachine.ChangeState(EState.MouseRelease);
    }

    public override bool CheckChangeStateEnable()
    {
        return true;
    }
}