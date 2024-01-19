using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformations : MonoBehaviour
{
    Mesh mesh;
    Vector3[] original;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        original = mesh.vertices;
    }

    Vector3[] Transform(Matrix4x4 matrix, Vector3[] inputVertices)
    {
        Vector3[] outputVertices = new Vector3[inputVertices.Length];
        for (int i = 0; i < outputVertices.Length; i++)
        {
            // For multiplication, we need M rows by N columns
            // So we must convert from vec3 to vec4
            Vector4 vertex = new Vector4(
                inputVertices[i].x,
                inputVertices[i].y,
                inputVertices[i].z,
            1.0f);

            outputVertices[i] = matrix * vertex;
        }
        return outputVertices;
    }

    // Update is called once per frame
    void Update()
    {
        float tt = Time.realtimeSinceStartup;
        float cos = Mathf.Cos(tt);
        Matrix4x4 scale = Matrix4x4.Scale(new Vector3(2.0f, 1.0f, 1.0f));
        Matrix4x4 rotation = Matrix4x4.Rotate(Quaternion.Euler(0.0f, 0.0f, 45.0f));
        Matrix4x4 translation = Matrix4x4.Translate(new Vector3(5.0f * cos, 0.0f, 0.0f));
        //Matrix4x4 result = translation;// * rotation * scale;
        //Matrix4x4 result = translation * rotation * scale;

        // ORDER MATTERS!!! Matrices are multiplied right-to-left, so the right-most matrix will be applied first.
        // For example, if we rotate then translate, the rotation will be done relative to the world origin,
        // so the result will be as expected.
        // However, if we translate first and then rotate, we create distance between the world origin and vertices,
        // so the rotation appears to greatly distort the vertices
        //Vector3[] newVertices = Transform(translation * rotation, original); <-- rotate then translate (expected result)
        //Vector3[] newVertices = Transform(rotation * translation, original); <-- translate then rotate (unexpected result)

        // Generally, the order is scale then rotate then translate
        mesh.vertices = Transform(translation * rotation * scale, original);
    }
}
