using UnityEngine;

public class UsageStatus : MonoBehaviour
{
    private bool isUsed;

    private void Start() {
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
