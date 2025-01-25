using DesignPatterns;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbilityBossState : BossState
{
    public virtual Ability ability { get; set; }
}
