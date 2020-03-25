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
            case "upPipe":
                script.upTurn();
                break;
            case "downPipe":
                script.downTurn();
                break;
            case "straightPipe":
                script.straightTurn();
                break;
        }
    }

    public void callUnsnapped()
    {
        script.UnSnapped();
    }
}
