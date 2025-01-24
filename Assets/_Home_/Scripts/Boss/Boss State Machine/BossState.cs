using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class BossState : MonoBehaviourState<BossState>
{
    public virtual async UniTask ProcessInputEvent(InputAction.CallbackContext inputEvent) { }
    private BossController _bossController;
    protected BossController bossController
    {
        get
        {
            if (_bossController == null) TryGetComponent<BossController>(out _bossController);
            return _bossController;
        }
    }
}