using System;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class cheeto : MonoBehaviour
{
    public float maxRange = 4;
    public float speed = 4;
    public float turnVariationMin = 30f;
    public float turnVariationMax = 60f;
    private float turnSpeed;
    private Vector3 spawnPosition;
    private bool canTurn = false;
    private int direction = 0;
    
    void Start()
    {
        spawnPosition = transform.position;
        turnSpeed = UnityEngine.Random.Range(turnVariationMin, turnVariationMax);
        if(UnityEngine.Random.Range(-1f, 1f) > 0f) {direction = 1;} else {direction = -1;} 
        turnSpeed = turnSpeed * direction;
    }

    
    void Update()
    {
        if (!canTurn) {checkCanTurn();}
        else {applyTurn();}
        advance();
    }
    
    void advance()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void checkCanTurn()
    {
        float _distance = Vector3.Distance(transform.position, spawnPosition);
        if (_distance > maxRange) {canTurn = true;}
    }

    void applyTurn()
    {
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        //if (transform.rotation.y > 0.99) {transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);}
        if (direction == 1 && transform.rotation.y > 0.99) {transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);}
        if (canTurn && direction == -1 && transform.rotation.y < -0.99) {Debug.Log(transform.rotation.y);transform.rotation = Quaternion.Euler(transform.rotation.x, -180, transform.rotation.z);}
        //else {transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);}
    }
}
