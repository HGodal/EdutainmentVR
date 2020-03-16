using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreakWallPart : MonoBehaviour
{
    int smallPieceCount;
    float width;
    Vector3 hammerVelocity;
    float hammerSpeed;
    AudioSource[] sounds;

    [Tooltip("The anchor from the OVRCameraRig to track velocity for.")]
    public GameObject trackedGameObject;
    [Tooltip("An optional object to consider the source relative to when retrieving velocities.")]
    public GameObject relativeTo;

    GameObject hammer;
    GameObject relativeObject;

    TextMeshProUGUI tmText;

    float forrige = 0;
    float gjeldende = 0;



    private void Start()
    {

        sounds = gameObject.transform.parent.GetComponents<AudioSource>();
        smallPieceCount = 3;
        width = gameObject.transform.localScale.z / smallPieceCount;

        tmText = GameObject.Find("/Canvas").transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        trackedGameObject = GameObject.Find("/RoomsAndVR/OVRCameraRig/TrackingSpace/CenterEyeAnchor");
        relativeTo = GameObject.Find("/RoomsAndVR/OVRCameraRig/TrackingSpace");
        relativeObject = GameObject.Find("/RelativeTo");
        hammer = GameObject.Find("/RubberMallet");
    }

    private void Update()
    {
        Rigidbody test1 = hammer.GetComponent<Rigidbody>();
        Rigidbody test2 = relativeObject.GetComponent<Rigidbody>();
        Vector3 aed = test1.GetRelativePointVelocity(test2.velocity);

        //var fart = Mathf.Abs((gjeldende - forrige) * Time.deltaTime);
        var fart = Mathf.Abs((gjeldende - forrige);

        forrige = gjeldende;

        gjeldende = test1.position.magnitude;

        //var aed = hammer.GetComponent<Rigidbody>().GetRelativePointVelocity(relativeTo.GetComponent<Rigidbody>().velocity);
        //Debug.Log(test1.position.magnitude);
        //Vector3 hei = relativeTo.transform.rotation * (OVRManager.isHmdPresent ? OVRPlugin.GetNodeVelocity(OVRPlugin.Node.EyeCenter, OVRPlugin.Step.Render).FromFlippedZVector3f() : Vector3.zero);
        tmText.text = fart.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Hammer"))
        {
            hammerVelocity = other.transform.parent.parent.parent.GetComponent<Rigidbody>().velocity;
            hammerSpeed = Mathf.Sqrt(Mathf.Pow(hammerVelocity.x, 2) + Mathf.Pow(hammerVelocity.y, 2) + Mathf.Pow(hammerVelocity.z, 2));

            //tmText.text = hammerSpeed.ToString();

            if (hammerSpeed >= 3.0f)
            {
                sounds[0].Play();
                Explode();
                return;
            }

            sounds[1].Play();
        }

        sounds[1].Play();
    }


    private void Explode()
    {
        Destroy(gameObject);

        for (int i = 0; i < smallPieceCount; i++)
        {
            for (int j = 0; j < smallPieceCount; j++)
            {
                CreateSmallWall(0.03f, i, j);
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 2);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(3, new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z), 2, 3);
            }
        }
    }

    private void CreateSmallWall(float x, float y, float z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(width * x, width * y, width * z);// - cubesPivot;
        piece.transform.localScale = new Vector3(0.03f, width, width);
        piece.layer = 20;

        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = width;

        Physics.IgnoreLayerCollision(9, 20, true);

        Destroy(piece, 2);
    }

}
