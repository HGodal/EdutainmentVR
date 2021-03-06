﻿using UnityEngine;

public class BreakWallPart : MonoBehaviour
{
    int smallPieceCount;
    float width;
    float depth;
    GameObject wallGrid;
    ProgressOverview progress;
    AudioSource[] sounds;


    void Start()
    {
        smallPieceCount = 3;
        width = gameObject.transform.localScale.z / smallPieceCount;
        depth = 0.03f;
        wallGrid = transform.parent.gameObject;

        progress = GameObject.Find("/InfoCanvas/InfoText").GetComponent<ProgressOverview>();
        sounds = GameObject.Find("/Sounds").GetComponents<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Hammer") && other.gameObject.GetComponent<ObjectSpeed>().getMagnitude() > 3.0f && other.gameObject.GetComponent<ObjectSpeed>().getMagnitude() < 10.0f)
        {
            progress.UpdateScore(-1);
            sounds[0].Play();
            Explode();
            return;
        }
        else if (other.tag.Equals("Hammer") || other.tag.Equals("PipeWrench"))
        {
            sounds[1].Play();
        }
    }

    void Explode()
    {
        for (int i = 0; i < smallPieceCount; i++)
        {
            for (int j = 0; j < smallPieceCount; j++)
            {
                CreateSmallWall(i, j);
            }
        }

        Destroy(gameObject);

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 2);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(3, new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z), 2, 3);
            }
        }
    }

    void CreateSmallWall(float y, float z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        piece.GetComponent<Renderer>().material.SetColor("_Color", gameObject.GetComponent<Renderer>().material.color);

        piece.transform.position = transform.position + new Vector3(width * depth, width * y, width * z);
        piece.transform.localScale = new Vector3(0.03f, width, width);
        piece.tag = "Invalid";
        piece.layer = 20;

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = width;

        Physics.IgnoreLayerCollision(9, 20, true);

        Destroy(piece, 2);
    }
}
