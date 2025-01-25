using Cysharp.Threading.Tasks;
using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleBossState : BossState
{
    public override async UniTask ProcessInputEvent(InputAction.CallbackContext inputContext)
    {
        if (bossController.abilityActions.ContainsKey(inputContext.action))
        {
            Ability ability = bossController.abilityActions[inputContext.action];
            ChargeAbilityBossState newState = (ChargeAbilityBossState)stateMachine.PrepareState(typeof(ChargeAbilityBossState));
            newState.ability = ability;
            stateMachine.ChangeToState(newState);
        }
    }

}
