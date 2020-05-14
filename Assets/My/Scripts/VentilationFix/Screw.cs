using UnityEngine;

public class Screw : MonoBehaviour
{
    AudioSource drill;
    BoltAction bolts;

    void Start()
    {
        bolts = GameObject.Find("/Bolts/SkrueLogikk").GetComponent<BoltAction>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Drill"))
        {
            bolts.Screws();
        }
    }
}
