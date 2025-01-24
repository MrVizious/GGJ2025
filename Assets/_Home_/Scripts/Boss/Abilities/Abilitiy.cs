using UltEvents;
using UnityEngine;

public abstract class Abilitiy<T> where T : AbilityData
{
    public float secondsOfCooldown = 5f;
    private float _secondsToCompleteCooldown;
    private float secondsToCompleteCooldown
    {
        get
        {
            return _secondsToCompleteCooldown;
        }
        set
        {
            _secondsToCompleteCooldown = value;
            if (_secondsToCompleteCooldown <= 0f)
            {
                onCooldownEnded.Invoke();
            }
        }
    }
    public UltEvent onCooldownEnded = new UltEvent();
    public float cooldownCompletionPercentage => 1f - (secondsToCompleteCooldown / secondsOfCooldown);

    public abstract void Use();
    public void EndCooldown()
    {
        secondsToCompleteCooldown = 0f;
    }
}
