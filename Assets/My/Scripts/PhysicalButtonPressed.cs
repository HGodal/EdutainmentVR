using UnityEngine;
using UnityEngine.Events;

public class PhysicalButtonPressed : MonoBehaviour
{
    public UnityEvent onPressed;
    protected bool lastState;

    AudioSource test;

    void Start()
    {
        lastState = false;
        test = GetComponent<AudioSource>();
    }

    public void SetState(bool state)
    {
        if (state && !lastState && onPressed != null)
        {
            onPressed.Invoke();
        }
        lastState = state;
    }

    public void TestButton(){
        test.Play();
    }
}
