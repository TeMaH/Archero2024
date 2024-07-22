using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector3 targetPosition;
    private Rigidbody rb;
    private float lifetime = 3.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetTarget(Vector3 targetPos)
    {
        targetPosition = targetPos;
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.AddForce(direction * speed, ForceMode.Impulse);
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
