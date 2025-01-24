using System.Collections.Generic;
using DesignPatterns;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossController : Singleton<BossController>
{
    protected override bool dontDestroyOnLoad => false;
    public Transform targetTransform;
    public Transform abilityStartTransform;

    [ReadOnly]
    [ShowInInspector]
    private Dictionary<InputAction, Ability<AbilityData>> _abilityActions;
    public Dictionary<InputAction, Ability<AbilityData>> abilityActions
    {
        get
        {
            if (_abilityActions == null || _abilityActions.Count <= 0) _abilityActions = new Dictionary<InputAction, Ability<AbilityData>>();
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
        abilityActions = new Dictionary<InputAction, Ability<AbilityData>>();
        foreach (Ability<AbilityData> ability in GetComponents<Ability<AbilityData>>())
        {
            abilityActions.Add(ability.abilityData.action, ability);
        }
    }



}
