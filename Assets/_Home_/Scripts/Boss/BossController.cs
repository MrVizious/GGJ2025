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
    private Dictionary<InputAction, Ability> _abilityActions;
    public Dictionary<InputAction, Ability> abilityActions
    {
        get
        {
            if (_abilityActions == null || _abilityActions.Count <= 0) FillInputActionAbilityDictionary();
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
        _abilityActions = new Dictionary<InputAction, Ability>();
        foreach (Ability ability in GetComponentsInChildren<Ability>())
        {
            _abilityActions.Add(ability.abilityData.actionReference.action, ability);
        }
    }
}