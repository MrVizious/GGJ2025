using UnityEngine;
using UnityEngine.InputSystem;

public class BossMovement : MonoBehaviour
{

    private BossController _bossController;
    protected BossController bossController
    {
        get
        {
            if (_bossController == null) TryGetComponent<BossController>(out _bossController);
            return _bossController;
        }
    }
    private Vector2 moveValue;
    public float moveSpeed = 5f;
    public void UpdateMoveVector(InputAction.CallbackContext inputContext)
    {
        moveValue = inputContext.ReadValue<Vector2>();
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        bossController.abilityStartTransform.position += moveSpeed * Vector3.forward * Mathf.Clamp(moveValue.y, -1f, 1f) * Time.deltaTime;
    }
}
