using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Button]
    public async Task SpawnCheeto()
    {
        Cheeto newCheeto = (Cheeto)(await VarietyPool.GetInstance()).Get<Cheeto>();
        Debug.Log($"Cheeto is {newCheeto}", this);
        newCheeto.transform.position = transform.position;
    }
}
