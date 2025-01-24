using DesignPatterns;
using UnityEngine;

public class BossController : Singleton<BossController>
{
    protected override bool dontDestroyOnLoad => false;
    public Transform targetTransform;
    public Transform abilityStartTransform;

}
