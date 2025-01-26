using Sirenix.OdinInspector;
using UnityEngine;

public class CojonesSelector : MonoBehaviour
{
    public Cojones cojones;
    [Button]
    public void SetCojones(bool newValue)
    {
        cojones.cojones = newValue;
    }
}
