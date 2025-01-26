using UnityEngine;
using DesignPatterns;

public class Kunai: Poolable
{
    public float speed = 40f;

    private void FixedUpdate()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        if (transform.position.x > 300) {Destroy(gameObject);
    }
}
}
