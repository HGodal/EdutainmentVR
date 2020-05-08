using UnityEngine;

public class Reset : MonoBehaviour
{
    public SnapZones script;

    public void ResetPipeWall()
    {
        script.CreateStart();
    }
}
