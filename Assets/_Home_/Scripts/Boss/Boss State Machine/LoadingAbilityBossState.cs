using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoadingAbilityBossState : BossState
{
    public Ability<AbilityData> ability;

    public override void ProcessInputEvent(InputAction.CallbackContext inputEvent)
    {
        throw new System.NotImplementedException();
    }
}
