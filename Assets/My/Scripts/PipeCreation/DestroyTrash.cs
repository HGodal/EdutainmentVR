using UnityEngine;

public class DestroyTrash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.layer.Equals(0))
        {
            Destroy(other.transform.root.gameObject);
        }
    }
}
