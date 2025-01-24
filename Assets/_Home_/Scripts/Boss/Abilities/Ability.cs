using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UltEvents;
using UnityEngine;

public abstract class Ability<T> : MonoBehaviour where T : AbilityData
{
    public T abilityData;
    public bool canBeActivated = true;
    private float _secondsSinceActivation;
    public float secondsSinceActivation
    {
        get
        {
            return _secondsSinceActivation;
        }
        set
        {
            _secondsSinceActivation = value;
            if (_secondsSinceActivation >= abilityData.secondsOfCooldown)
            {
                onCooldownEnded.Invoke();
                canBeActivated = true;
            }
        }
    }
    public UltEvent onCooldownEnded = new UltEvent();
    [ReadOnly]
    [ShowInInspector]
    [Range(0f, 1f)]
    public float cooldownCompletionPercentage => Mathf.Clamp01(secondsSinceActivation / abilityData.secondsOfCooldown);

    [Button(DrawResult = false)]
    public virtual async UniTask Activate()
    {
        if (!canBeActivated) return;
        canBeActivated = false;
        secondsSinceActivation = 0;
    }

    [Button(DrawResult = false)]
    public void EndCooldown()
    {
        secondsSinceActivation = abilityData.secondsOfCooldown;
    }

    private void Update()
    {
        if (canBeActivated) return;
        secondsSinceActivation += Time.deltaTime;
    }
}
