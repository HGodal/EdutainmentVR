using UnityEngine;

public class UsageStatus : MonoBehaviour
{
    bool isUsed;

    void Start() {
        isUsed = false;
    }

    public void ChangeIsUsed()
    {
        isUsed = !isUsed;
    }

    public bool GetUsageStatus()
    {
        return isUsed;
    }
}
