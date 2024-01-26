using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpolation : MonoBehaviour
{
    Mesh mesh;
    Vector3[] original;
    Vector3 A = new Vector3(-5.0f, 0.0f);
    Vector3 B = new Vector3( 5.0f, 0.0f);
    public float t = 0.0f;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        original = mesh.vertices;
        Debug.Log(Vector3.Lerp(A, B, t));
    }

    // Update is called once per frame
    void Update()
    {
        float tt = Time.realtimeSinceStartup;
        float cos = Mathf.Cos(tt);

        Matrix4x4 transformation = Matrix4x4.Translate(Vector3.Lerp(A, B, t));
        Vector3[] output = Transformations.Transform(transformation, original);
        mesh.vertices = output;
    }
}
