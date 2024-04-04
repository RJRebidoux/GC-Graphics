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
        return false;
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

        InFoV(transform.position, target.position, transform.up, angle);
        //Debug.DrawLine(transform.position, transform.position + transform.up * 5.0f, Color.red);
    }
}
