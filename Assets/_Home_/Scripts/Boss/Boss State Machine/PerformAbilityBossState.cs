using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerformAbilityBossState : AbilityBossState
{
    public override void Enter(IStateMachine<BossState> newStateMachine)
    {
        base.Enter(newStateMachine);
        ability.Perform(bossController);
        stateMachine.ChangeToState(typeof(IdleBossState));
    }
}
