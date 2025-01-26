using ExtensionMethods;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    public Rigidbody rb
    {
        get
        {
            if (_rb == null) TryGetComponent<Rigidbody>(out _rb);
            return _rb;
        }
    }


    private Vector3 moveValue;
    public float moveSpeed = 20f;
    public float dashSpeed = 10f;
    private float size = 1f;
    private float growthRate = 0.01f;
    private Vector3 originalScale;
    public GameObject deathEffect;
    public GameObject dashEffect;

    private void Start()
    {
        originalScale = transform.localScale;
        dashEffect.SetActive(false);
    }

    public void UpdateMoveVector(InputAction.CallbackContext inputContext)
    {
        moveValue = Vector3.ClampMagnitude(inputContext.ReadValue<Vector2>(), 1f);
        moveValue = Vector3.zero.With(x: moveValue.x, z: moveValue.y);
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        if (size < 1f) { Grow(); }
    }

    private void Move()
    {
        Vector3 addedForce = -1f * moveValue * moveSpeed * Time.deltaTime;
        rb.AddForce(addedForce, ForceMode.Acceleration);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 10f);
        // transform.position -= moveSpeed * Time.deltaTime * Vector3.forward * Mathf.Clamp(moveValue.y, -1f, 1f);
        // transform.position -= moveSpeed * Time.deltaTime * Vector3.right * Mathf.Clamp(moveValue.x, -1f, 1f);
    }

    private void Grow()
    {
        size += growthRate;
        transform.localScale = size * originalScale;
        if (size > 1) { size = 1; dashEffect.SetActive(false);}
    }

    public void Dash(InputAction.CallbackContext inputContext)
    {
        if (inputContext.phase == InputActionPhase.Performed && size == 1)
        {
            transform.position -= moveValue * dashSpeed;
            // transform.position -= dashSpeed * Vector3.forward * Mathf.Clamp(moveValue.y, -1f, 1f);
            // transform.position -= dashSpeed * Vector3.right * Mathf.Clamp(moveValue.x, -1f, 1f);
            size = 0.5f;
            dashEffect.SetActive(true);
        }
    }

    [Button]
    public async void Damage()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        (await GameController.GetInstance()).RespawnPlayer(this);
    }

}
