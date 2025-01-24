using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbilityData : ScriptableObject
{
    public InputAction action;
    public float secondsOfCooldown = 5f;
}
