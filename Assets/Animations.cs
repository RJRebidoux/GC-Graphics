using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Animations : MonoBehaviour
{
    Mesh mesh;
    Vector3[] original;


    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        original = mesh.vertices;
    }

    void Update()
    {
        float tt = Time.realtimeSinceStartup;
        float sin = Mathf.Sin(tt);

        Matrix4x4 scale = Matrix4x4.identity;
        Matrix4x4 rotation = Matrix4x4.identity;
        Matrix4x4 translation = Matrix4x4.identity;

        scale = Matrix4x4.Scale(new Vector3(2.0f, 1.0f, 1.0f));
        rotation = Matrix4x4.Rotate(Quaternion.Euler(0.0f, 0.0f, 45.0f));
        translation = Matrix4x4.Translate(new Vector3(5.0f * sin, 0.0f, 0.0f));
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
        Vector3[] outputVertices = Transformations.Transform(translation * rotation * scale, original);
        mesh.vertices = outputVertices;
    }
}
