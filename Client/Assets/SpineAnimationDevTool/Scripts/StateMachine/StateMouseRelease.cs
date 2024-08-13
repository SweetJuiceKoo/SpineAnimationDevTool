using Cysharp.Threading.Tasks;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StateMouseRelease : StateBase
{
    #region[Field]
    private CancellationTokenSource m_token;
    #endregion

    #region[Cycle]
    public StateMouseRelease(FsmComponent _stateMachine) : base(_stateMachine)
    {
    }
    #endregion

    #region[Method]
    public override void Enter()
    {
        base.Enter();
        SetAnimationByData(EState.MouseRelease);

        m_token?.Cancel();
        UniTaskChangeStateDelay().Forget();
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
    }

    public override bool CheckChangeStateEnable()
    {
        return true;
    }

    private async UniTask UniTaskChangeStateDelay()
    {
        m_token = new CancellationTokenSource();

        var clip = m_stateMachine.Entity.SkelAnim.AnimationState.Tracks.Items[0];
        var delayTime = clip.AnimationEnd - clip.AnimationTime;

        await UniTask.WaitForSeconds(delayTime, cancellationToken: m_token.Token);
        m_stateMachine.ChangeState(EState.Idle);
    }
    #endregion
}