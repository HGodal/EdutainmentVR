using UnityEngine;

public class DestroyTrash : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.layer.Equals(0))
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
}
