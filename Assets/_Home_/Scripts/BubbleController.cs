using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    private Vector2 moveInputValue;

    private void onMove(InputValue inputValue){
        moveInputValue = inputValue.Get<Vector2>();
    }

    private void moveLogic(){
        Vector2 result = moveInputValue * speed * Time.fixedDeltaTime;
        rb.linearVelocity = result;
        Debug.Log("Speed = " + result);
    }

    private void FixedUpdate() {
        moveLogic();
    }
}
