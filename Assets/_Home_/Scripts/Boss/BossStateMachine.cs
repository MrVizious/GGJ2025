using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossStateMachine : MonoBehaviourStateMachine<BossState>
{
    private void Start()
    {
        ChangeToState(typeof(MoveBossState));
    }
    public void ProcessInputEvent(InputAction.CallbackContext inputEvent)
    {
        currentState.ProcessInputEvent(inputEvent);
    }
}
