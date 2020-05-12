using UnityEngine;
using VRTK.Prefabs.Interactions.InteractableSnapZone;

public class CallSnapZones : MonoBehaviour
{
    SnapZones script;
    GameObject snappedObject;

    private void Start()
    {
        script = GameObject.Find("/Pipes").GetComponent<SnapZones>();
    }

    public void callZones(string pipeType)
    {
        snappedObject = GetComponent<SnapZoneFacade>().SnappedGameObject;
        snappedObject.transform.GetChild(1).gameObject.SetActive(false);
        snappedObject.transform.GetChild(2).gameObject.SetActive(false);
        switch (pipeType)
        {
            case "upPipe":
                script.UpTurn();
                break;
            case "straightPipe":
                script.StraightTurn();
                break;
            case "downPipe":
                script.DownTurn();
                break;
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
    }
}
