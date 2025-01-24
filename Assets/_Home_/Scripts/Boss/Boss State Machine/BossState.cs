using UnityEngine;
using UnityEngine.InputSystem;
public abstract class BossState : MonoBehaviourState<BossState>
{
    public abstract void ProcessInputEvent(InputAction.CallbackContext inputEvent);
    private BossController _bossController;
    private BossController bossController
    {
        get
        {
            if (_bossController == null) TryGetComponent<BossController>(out _bossController);
            return _bossController;
        }
    }
}
