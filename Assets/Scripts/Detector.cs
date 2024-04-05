using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float FoV;

    Quaternion r0 = Quaternion.Euler(0.0f, 0.0f, 120.0f);
    Quaternion r1 = Quaternion.Euler(0.0f, 0.0f, 240.0f);

    bool InFoV(Vector3 viewerPosition, Vector3 viewerDirection, Vector3 targetPosition, float fov, float viewDistance = float.PositiveInfinity)
    {
        // AB = B - A
        Vector3 targetDirection = (targetPosition - viewerPosition).normalized;
        float dot = Vector3.Dot(viewerDirection, targetDirection);

        Debug.DrawLine(viewerPosition, viewerPosition + viewerDirection * 5.0f, Color.green);
        Debug.DrawLine(viewerPosition, viewerPosition + targetDirection * 5.0f, Color.red);
        return dot >= Mathf.Cos(fov * 0.5f * Mathf.Deg2Rad);
    }

    void Start()
    {
        
    }

    void Update()
    {
        float tt = Time.realtimeSinceStartup;
        float nsin = Mathf.Sin(tt) * 0.5f + 0.5f;
        transform.rotation = Quaternion.Slerp(r0, r1, nsin);

        bool inFoV = InFoV(transform.position, transform.up, target.position, FoV);
        target.GetComponent<MeshRenderer>().material.color = inFoV ? Color.red : Color.green;
    }
}
