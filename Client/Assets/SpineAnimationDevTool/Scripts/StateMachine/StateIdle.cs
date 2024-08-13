using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : StateBase
{
    #region[Field]
    #endregion

    #region[Cycle]
    public StateIdle(FsmComponent _stateMachine) : base(_stateMachine)
    {
    }
    #endregion

    #region[Method]
    #endregion
    public override void Enter()
    {
        base.Enter();
        SetAnimationByData(EState.Idle);
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

        if (Input.GetMouseButtonDown(0))
           m_stateMachine.ChangeState(EState.MousePress);
    }

    public override bool CheckChangeStateEnable()
    {
        return true;
    }
}