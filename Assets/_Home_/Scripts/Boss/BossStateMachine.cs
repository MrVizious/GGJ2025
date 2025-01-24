using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossStateMachine : MonoBehaviourStateMachine<BossState>
{
    public void ProcessInputEvent(InputAction.CallbackContext inputEvent)
    {
        currentState.ProcessInputEvent(inputEvent);
    }
}
