using ExtensionMethods;
using PrimeTween;
using Sirenix.OdinInspector;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

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
    public float cameraShakeDuration = 0.5f;
    public float cameraShakeStrength = 3f;
    public float moveSpeed = 20f;
    public float dashSpeed = 10f;
    public float dashCooldown = 2f;
    private Vector3 originalScale;
    public VisualEffect deathEffect;
    public VisualEffect dashEffect;

    private void Start()
    {
        originalScale = transform.localScale;
        dashEffect.enabled = false;
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


    private void Move()
    {
        Vector3 addedForce = -1f * moveValue * moveSpeed * Time.deltaTime;
        rb.AddForce(addedForce, ForceMode.Acceleration);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 10f);
        // transform.position -= moveSpeed * Time.deltaTime * Vector3.forward * Mathf.Clamp(moveValue.y, -1f, 1f);
        // transform.position -= moveSpeed * Time.deltaTime * Vector3.right * Mathf.Clamp(moveValue.x, -1f, 1f);
    }


    public void Dash(InputAction.CallbackContext inputContext)
    {
        if (inputContext.phase == InputActionPhase.Performed && transform.localScale.magnitude >= originalScale.magnitude)
        {
            transform.position -= moveValue * dashSpeed;
            // transform.position -= dashSpeed * Vector3.forward * Mathf.Clamp(moveValue.y, -1f, 1f);
            // transform.position -= dashSpeed * Vector3.right * Mathf.Clamp(moveValue.x, -1f, 1f);
            dashEffect.enabled = true;
            dashEffect.Play();
            Tween.CompleteAll(transform);
            transform.localScale = originalScale * 0.5f;
            Tween.Scale(transform, originalScale, dashCooldown);
        }
    }

    [Button]
    public async void Damage()
    {
        GameObject newDeathEffectGO = Instantiate(deathEffect, transform.position, transform.rotation).gameObject;
        Destroy(newDeathEffectGO, 1f);
        (await GameController.GetInstance()).RespawnPlayer(this);
        ShakeCamera();
    }

    [Button]
    public void ShakeCamera()
    {
        Tween.ShakeCamera(Camera.main, cameraShakeStrength, cameraShakeDuration);
    }
}