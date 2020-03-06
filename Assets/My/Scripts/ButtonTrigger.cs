using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Transform pipePrefab;
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("PointerFingerTip"))
        //{
            Transform t = Instantiate(pipePrefab);
            t.position = spawnPoint.position;
        //}
    }
}
