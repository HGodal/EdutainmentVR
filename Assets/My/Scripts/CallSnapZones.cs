using UnityEngine;

public class CallSnapZones : MonoBehaviour
{
    SnapZones script;

    private void Start()
    {
        script = GameObject.Find("/Pipes").GetComponent<SnapZones>();
    }

    public void callZones(string pipeType)
    {
        switch (pipeType)
        {
            case "leftPipe":
                script.leftTurn();
                break;
            case "rightPipe":
                script.rightTurn();
                break;
            case "straightPipe":
                script.straightTurn();
                break;
        }
    }
}
