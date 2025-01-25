using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveValue;
    public float moveSpeed = 20f;
    public float dashSpeed = 10f;
    private float size = 1f;
    private float growthRate = 0.01f;
    private Vector3 originalScale;
    public GameObject sphere;

    private void Start()
    {
        originalScale = transform.localScale;
    }
    
    public void UpdateMoveVector(InputAction.CallbackContext inputContext)
    {
        moveValue = inputContext.ReadValue<Vector2>();
    }
    
    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        if (size < 1f) {Grow();}
    }

    private void Move()
    {
        transform.position -= moveSpeed * Time.deltaTime * Vector3.forward * Mathf.Clamp(moveValue.y, -1f, 1f);
        transform.position -= moveSpeed * Time.deltaTime * Vector3.right * Mathf.Clamp(moveValue.x, -1f, 1f);
    }

    private void Grow()
    {
        size += growthRate;
        transform.localScale = size * originalScale;
        if (size > 1) {size = 1;} 
    }

    public void Dash(InputAction.CallbackContext inputContext)
    {
        if (inputContext.phase == InputActionPhase.Performed && size == 1)
        {
            transform.position -= dashSpeed * Vector3.forward * Mathf.Clamp(moveValue.y, -1f, 1f);
            transform.position -= dashSpeed * Vector3.right * Mathf.Clamp(moveValue.x, -1f, 1f);
            size = 0.5f;
        }
    }
}
