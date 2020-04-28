using UnityEngine;

public class OutConStats : MonoBehaviour
{
    [Range(0, 1)]
    public int facingAwayFromSun;

    public int CalculateScore(GameObject inConSnaps)
    {
        foreach (Transform snapZone in inConSnaps.transform)
        {
            if (snapZone.GetComponent<UsageStatus>().GetUsageStatus())
            {
                int distance = (int)Mathf.Ceil(Vector3.Distance(transform.position, snapZone.transform.position));

                if (distance > 1)
                {
                    return facingAwayFromSun * 20 + (30 - distance);
                }
                else
                {
                    return facingAwayFromSun * 20 + 5;
                }
            }
        }
        return -1;
    }
}
