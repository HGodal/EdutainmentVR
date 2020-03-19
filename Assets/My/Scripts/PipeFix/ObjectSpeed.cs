using UnityEngine;

public class ObjectSpeed : MonoBehaviour
{
    Vector3 currentSpeed;
    Vector3 lastPos;

    private void Start()
    {
        lastPos = transform.position;
    }

    private void Update()
    {
        if (lastPos != transform.position)
        {
            currentSpeed = transform.position - lastPos;
            currentSpeed /= Time.deltaTime;
            lastPos = transform.position;
        }
    }

    public float getMagnitude()
    {
        return currentSpeed.magnitude;
    }
}