using UnityEngine;

public class flyMovement : MonoBehaviour
{
    public float maxSpeed = 1.5f;
    public float turnSpeed = 3f;
    public float acceleration = 1f;
    private float speed = 0;
    private GameObject Target;
    private GameObject[] players;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Target = players[0];
    }

    void Update()
    {
        selectTarget();
        rotateTowards(Target.transform.position);
        advance();
    }

    void rotateTowards(Vector3 target)
    {
        Quaternion _lookRotation = Quaternion.LookRotation((target - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
    }

    void advance()
    {
        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed) { speed = maxSpeed; }
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void selectTarget()
    {
        foreach (GameObject player in players)
        {
            float _distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            float _distanceToTarget = Vector3.Distance(Target.transform.position, transform.position);
            if (_distanceToPlayer < _distanceToTarget) { Target = player; }
        }
    }
}