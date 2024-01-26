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

        // Matrix multiplication combines transformations, quaternion multiplication combines rotations
        Quaternion q1 = Quaternion.Euler(tt * 100.0f, 0.0f, 0.0f);
        Quaternion q2 = Quaternion.Euler(0.0f, 0.0f, 45.0f);
        //Matrix4x4 rotation = Matrix4x4.Rotate(q1 * q2);

        // Since matrices store rotations, we can also just combine rotation matrices!
        Matrix4x4 r1 = Matrix4x4.Rotate(q1);
        Matrix4x4 r2 = Matrix4x4.Rotate(q2);
        //Matrix4x4 rotation = r1 * r2;

        // (q1 * q2 is unnecessary because q1 is x and q2 is z so we could make this into a single quaternion initially)
        Matrix4x4 rotation = Matrix4x4.Rotate(Quaternion.Euler(tt * 100.0f, 0.0f, 45.0f));

        Matrix4x4 translation = Matrix4x4.Translate(new Vector3(5.0f * cos, 0.0f, 0.0f));
        //Matrix4x4 result = translation;// * rotation * scale;
        //Matrix4x4 result = translation * rotation * scale;

        // Default matrix is a zero-matrix which will cause our vertices to be zero!
        //Matrix4x4 transformation = new Matrix4x4();
        Matrix4x4 transformation = rotation * Matrix4x4.identity;
        Debug.Log(transformation);

        // ORDER MATTERS!!! Matrices are multiplied right-to-left, so the right-most matrix will be applied first.
        // For example, if we rotate then translate, the rotation will be done relative to the world origin,
        // so the result will be as expected.
        // However, if we translate first and then rotate, we create distance between the world origin and vertices,
        // so the rotation appears to greatly distort the vertices
        //Vector3[] newVertices = Transform(translation * rotation, original); <-- rotate then translate (expected result)
        //Vector3[] newVertices = Transform(rotation * translation, original); <-- translate then rotate (unexpected result)

        // Generally, the order is scale then rotate then translate
        Vector3[] output = Transform(transformation, original);
        mesh.vertices = output;
    }
}
