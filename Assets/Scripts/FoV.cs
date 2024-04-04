using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoV : MonoBehaviour
{
    public Transform target;

    bool InFoV(Vector3 viewPosition, Vector3 targetPosition, Vector3 viewDirection, float angle, float viewDistance = float.PositiveInfinity)
    {
        // AB = B - A
        Vector3 targetDirection = (targetPosition - viewPosition).normalized;
        Debug.DrawLine(viewPosition, viewPosition + viewDirection * 5.0f, Color.green);
        Debug.DrawLine(viewPosition, viewPosition + targetDirection * 5.0f, Color.red);

        // Compare view direction (red in spotlight diagram) with our view direction (black in spotlight diagram)
        float dot = Vector3.Dot(viewDirection, targetDirection);

        // Dot(a, b) = |a| x |b| x cos(t)
        // We know a & b are unit-vectors, so their magnitudes are 1.
        // 1 x 1 x cos(t) = cos(t) --> we can remove the magnitudes from our equation!
        // Dot(a, b) = cos(t)

        return Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad) < dot;
    }

    float angle = 60.0f;

    Quaternion r0 = Quaternion.Euler(0.0f, 0.0f, 240.0f);
    Quaternion r1 = Quaternion.Euler(0.0f, 0.0f, 120.0f);

    void Start()
    {
        
    }

    void Update()
    {
        float tt = Time.realtimeSinceStartup;
        float nsin = Mathf.Sin(tt) * 0.5f + 0.5f;
        transform.rotation = Quaternion.Slerp(r0, r1, nsin);

        bool inFoV = InFoV(transform.position, target.position, transform.up, angle);
        target.GetComponent<MeshRenderer>().material.color = inFoV ? Color.red : Color.green;
        //Debug.DrawLine(transform.position, transform.position + transform.up * 5.0f, Color.red);
    }
}
