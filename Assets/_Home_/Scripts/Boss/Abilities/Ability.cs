using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UltEvents;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public abstract AbilityData abilityData { get; }
    public abstract bool hasChargeMethod { get; }
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
            onCooldownPercentageChanged.Invoke(cooldownCompletionPercentage);
            if (_secondsSinceActivation >= abilityData.secondsOfCooldown)
            {
                onCooldownEnded.Invoke();
                canBeActivated = true;
            }
        }
    }
    public UltEvent onCooldownEnded = new UltEvent();
    public float cooldownCompletionPercentage => Mathf.Clamp01(secondsSinceActivation / abilityData.secondsOfCooldown);
    public UltEvent<float> onCooldownPercentageChanged = new UltEvent<float>();

    [Button(DrawResult = false)]
    public virtual async UniTask<bool> Perform(BossController bossController)
    {
        if (!canBeActivated) return false;
        canBeActivated = false;
        secondsSinceActivation = 0;
        return true;
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

    public virtual void ChargeAbilityUpdate(BossController bossController) { }
}