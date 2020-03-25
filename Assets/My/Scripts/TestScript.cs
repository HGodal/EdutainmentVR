using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VRTK.Prefabs.Interactions.InteractableSnapZone;

public class TestScript : MonoBehaviour
{
    TextMeshProUGUI text;
    int children;
    SnapZoneFacade snapObject;

    // Start is called before the first frame update
    void Start()
    {
        children = 0;
        snapObject = GetComponent<SnapZoneFacade>();
        text = GameObject.Find("/InfoCanvas/InfoText").GetComponent<TextMeshProUGUI>();
    }

    public void RunAndCheck()
    {
        children = 0;
        AddDescendants(transform);
        snapObject.SnappedGameObject.transform.GetChild(1).gameObject.SetActive(false);
        text.text = snapObject.SnappedGameObject.ToString();
    }

    private void AddDescendants(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == tag)
            {
                children++;
            }
            AddDescendants(child);
        }
    }


}
