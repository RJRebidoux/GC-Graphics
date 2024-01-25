using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolation : MonoBehaviour
{
    public float t = 0.0f;
    Vector3 p0 = new Vector3(-5.0f, 0.0f, 0.0f);
    Vector3 p1 = new Vector3( 5.0f, 10.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        // "Go To Definition" on Lerp to see the formula!
        Debug.Log(Mathf.Lerp(5.0f, 10.0f, 0.75f));
    }

    // Update is called once per frame
    void Update()
    {
        float tt = Time.realtimeSinceStartup;
        float nsin = Mathf.Sin(tt) * 0.5f + 0.5f;
        transform.position = Vector3.Lerp(p1, p0, 0.0f);
    }
}
