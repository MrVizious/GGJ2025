using UnityEngine;

public class MonsterCharco : MonoBehaviour
{
    private BoxCollider col;

    private void Start()
    {
        col = gameObject.GetComponent<BoxCollider>();
        col.enabled = false;
        Invoke("ActivateSlow", 2f);
    }

    private void ActivateSlow()
    {
        col.enabled = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().SlowOn();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().SlowOff();
        }
    }
}
