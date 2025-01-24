using Cysharp.Threading.Tasks;
using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveBossState : BossState
{
    public override async UniTask ProcessInputEvent(InputAction.CallbackContext inputContext)
    {
        Ability ability = bossController.abilityActions[inputContext.action];
        if (ability != null)
        {
            StartAbilityBossState newState = (StartAbilityBossState)stateMachine.PrepareState(typeof(StartAbilityBossState));
            newState.ability = ability;
            stateMachine.ChangeToState(newState);
            return;
        }
    }
}
