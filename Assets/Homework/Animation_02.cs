using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Animation_02 : MonoBehaviour
{
    Mesh mesh;
    Vector3[] startingVertices;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        startingVertices = mesh.vertices;
    }

    public Vector3[] Transform(Matrix4x4 matrix, Vector3[] vertices)
    {
        Vector3[] result = new Vector3[vertices.Length];
        for (int i = 0; i < result.Length; i++)
        {
            Vector4 vertex = new Vector4(vertices[i].x, vertices[i].y, vertices[i].z, 1.0f);
            result[i] = matrix * vertex;
        }
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        float tt = Time.realtimeSinceStartup;
        float cos = Mathf.Cos(tt);
        Matrix4x4 scale = Matrix4x4.Scale(new Vector3(cos * 2.0f, cos * 2.0f, 5.0f));
        Matrix4x4 rotation = Matrix4x4.Rotate(Quaternion.Euler(0.0f, 0.0f, tt * 1000.0f));
        Matrix4x4 transformation = scale * rotation;
        Vector3[] result = Transformations.Transform(transformation, startingVertices);
        mesh.vertices = result;
    }
}
