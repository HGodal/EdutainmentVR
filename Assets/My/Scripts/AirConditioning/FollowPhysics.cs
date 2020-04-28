using UnityEngine;

public class FollowPhysics : MonoBehaviour
{
    public Transform target;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * 20 * Time.deltaTime);
    }
}
