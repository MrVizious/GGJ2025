using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbilityData : ScriptableObject
{
    public InputActionReference actionReference;
    public float secondsOfCooldown = 5f;
}
