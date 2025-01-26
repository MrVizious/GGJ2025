using UnityEngine;

public class KillerProjectile : MonoBehaviour
{
    public bool destroyOnKill = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().Damage();
            if (destroyOnKill)
            {
                Destroy(gameObject);
            }
        }
    }
}
