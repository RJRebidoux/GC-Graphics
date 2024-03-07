using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolation : MonoBehaviour
{
    public float t = 0.0f;
    Vector3 t0 = new Vector3(-5.0f, 0.0f, 0.0f);
    Vector3 t1 = new Vector3( 5.0f, 10.0f, 0.0f);
    Vector3 s0 = new Vector3(1.0f, 1.0f, 1.0f);
    Vector3 s1 = new Vector3( 2.0f, 3.0f, 0.0f);
    Color c0 = Color.red;
    Color c1 = Color.yellow;
    Quaternion r0 = Quaternion.identity;
    Quaternion r1 = Quaternion.Euler(0.0f, 0.0f, 90.0f);

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
        transform.position = Vector3.Lerp(t0, t1, nsin);
        transform.rotation = Quaternion.Slerp(r0, r1, nsin);
        transform.localScale = Vector3.Lerp(s0, s1, nsin);
        Color color = Color.Lerp(c0, c1, nsin);
        color.a = Mathf.Lerp(1.0f, 0.0f, nsin);
        GetComponent<MeshRenderer>().material.color = color;
    }
}
