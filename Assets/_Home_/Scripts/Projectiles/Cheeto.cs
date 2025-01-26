using System;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using DesignPatterns;

public class Cheeto : Poolable
{
    public float maxRange = 80;
    public float speed = 50;
    public float turnVariationMin = 300f;
    public float turnVariationMax = 600f;
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
		float _distance = Vector3.Distance(transform.position, spawnPosition);
        if (_distance > 2*maxRange) {Destroy(gameObject.transform.parent.gameObject);}
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
        if (direction == 1 && transform.localRotation.y > 0.99) {transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180, transform.localRotation.z);}
        if (canTurn && direction == -1 && transform.localRotation.y < -0.99) {Debug.Log(transform.rotation.y);transform.localRotation = Quaternion.Euler(transform.localRotation.x, -180, transform.localRotation.z);}
        //else {transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);}
    }
}