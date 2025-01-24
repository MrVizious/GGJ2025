using System.Collections.Generic;
using DesignPatterns;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossController : MonoBehaviour
{
    public Transform targetTransform;
    public Transform abilityStartTransform;

    [ReadOnly]
    [ShowInInspector]
    private Dictionary<InputActionReference, Ability> _abilityActions;
    public Dictionary<InputActionReference, Ability> abilityActions
    {
        get
        {
            if (_abilityActions == null || _abilityActions.Count <= 0) _abilityActions = new Dictionary<InputActionReference, Ability>();
            return _abilityActions;
        }
        private set
        {
            _abilityActions = value;
        }
    }

    [Button]
    private void FillInputActionAbilityDictionary()
    {
        foreach (Ability ability in GetComponents<Ability>())
        {
            Debug.Log($"Ability found {ability.abilityData.action}", this);
            abilityActions.Add(ability.abilityData.action, ability);
        }
    }
}