using Cysharp.Threading.Tasks;
using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChargeAbilityBossState : AbilityBossState
{
    public override async UniTask ProcessInputEvent(InputAction.CallbackContext inputEvent)
    {
        if (inputEvent.action != ability.abilityData.actionReference.action) return;
        if (inputEvent.phase == InputActionPhase.Canceled)
        {
            PerformAbilityBossState newState = (PerformAbilityBossState)stateMachine.PrepareState(typeof(PerformAbilityBossState));
            newState.ability = ability;
            stateMachine.ChangeToState(newState);
        }
    }

    public override void Enter(IStateMachine<BossState> newStateMachine)
    {
        base.Enter(newStateMachine);
        bossController.targetTransform.position = bossController.abilityStartTransform.position;
    }
    private void Update()
    {
        if (ability == null) return;
        if (!ability.canBeActivated) return;
        ability.ChargeAbilityUpdate(bossController);
    }
}
