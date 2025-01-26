using UnityEngine;

public class BallsController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().Damage();
            BallsHit();
        }
    }

    private async void BallsHit()
    {
        (await GameController.GetInstance()).TakeDamage();
    }
}
