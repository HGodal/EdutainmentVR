using UnityEngine;
using UnityEngine.Events;

public class PhysicalButtonPressed : MonoBehaviour
{
    public UnityEvent onPressed;
    protected bool lastState;

    void Start()
    {
        lastState = false;
    }

    public void SetState(bool state)
    {
        if (state && !lastState && onPressed != null)
        {
            onPressed.Invoke();
        }
        lastState = state;
    }
}
