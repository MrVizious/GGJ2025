using UnityEngine;

public class Despawner: MonoBehaviour
{
    public float timeToLive = 15f;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }
    
    private void Update()
    {
        if (Time.time > startTime + timeToLive)
        {
            Destroy(gameObject);
        }
    } 
}
