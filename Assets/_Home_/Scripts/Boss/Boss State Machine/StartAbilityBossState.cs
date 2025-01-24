using Cysharp.Threading.Tasks;
using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartAbilityBossState : AbilityBossState
{
    public override async UniTask ProcessInputEvent(InputAction.CallbackContext inputEvent)
    {
        base.ProcessInputEvent(inputEvent);
        if (inputEvent.action == ability.abilityData.actionReference.action)
        {
            if (inputEvent.action.phase == InputActionPhase.Canceled)
            {
                stateMachine.ChangeToState(typeof(MoveBossState));
            }
            else if (inputEvent.action.phase == InputActionPhase.Performed
            || inputEvent.action.phase == InputActionPhase.Started)
            {
                await ability.Activate();
            }
        }
    }
}
