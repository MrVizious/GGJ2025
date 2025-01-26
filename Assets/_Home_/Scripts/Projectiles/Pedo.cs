using UnityEngine;

public class Pedo: MonoBehaviour
{
    private float fartForce = 80000;
    void OnTriggerEnter(Collider other) {
        Debug.Log("trigger enter");
        if (other.tag == "Player")
        {
            Debug.Log("applying force");
            Vector3 direction = new Vector3(fartForce, 0, 0);
            other.gameObject.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Acceleration);
        }
    }
}
