using Cysharp.Threading.Tasks;
using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveBossState : BossState
{
    public InputActionReference moveAction;
    private Vector2 moveValue;
    public override async UniTask ProcessInputEvent(InputAction.CallbackContext inputContext)
    {
        if (bossController.abilityActions.ContainsKey(inputContext.action))
        {
            Ability ability = bossController.abilityActions[inputContext.action];
            ChargeAbilityBossState newState = (ChargeAbilityBossState)stateMachine.PrepareState(typeof(ChargeAbilityBossState));
            newState.ability = ability;
            stateMachine.ChangeToState(newState);
            return;
        }

        if (inputContext.action == moveAction.action)
        {
            moveValue = inputContext.ReadValue<Vector2>().normalized;
            Debug.Log($"Move value is {moveValue}", this);
        }

    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        bossController.abilityStartTransform.position += Vector3.forward * moveValue.y * Time.deltaTime;
    }
}
