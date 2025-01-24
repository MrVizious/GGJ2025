using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbilityData : ScriptableObject
{
    public InputActionReference action;
    public float secondsOfCooldown = 5f;
}
