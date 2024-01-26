using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolation : MonoBehaviour
{
    Mesh mesh;
    Vector3[] original;
    Vector3 A = new Vector3(-5.0f, 0.0f);
    Vector3 B = new Vector3( 5.0f, 0.0f);

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        original = mesh.vertices;

        // We can use the computer to test our lerp calculations!
        Debug.Log(Vector3.Lerp(A, B, 0.25f));
    }

    // Update is called once per frame
    void Update()
    {
        float tt = Time.realtimeSinceStartup;
        float t = Mathf.Sin(tt) * 0.5f + 0.5f;

        Matrix4x4 transformation = Matrix4x4.Translate(Vector3.Lerp(A, B, t));
        Vector3[] output = Transformations.Transform(transformation, original);
        mesh.vertices = output;
    }
}
