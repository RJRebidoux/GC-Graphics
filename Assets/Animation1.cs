using UnityEngine;

public class Animation1 : MonoBehaviour
{
    Mesh mesh;
    Vector3[] original;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        original = mesh.vertices;
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
        //Matrix4x4 rotation = Matrix4x4.Rotate(Quaternion.Euler(tt * 100.0f, 0.0f, 45.0f));
        Matrix4x4 rotation = Matrix4x4.Rotate(Quaternion.Euler(0.0f, 0.0f, tt * 100.0f));

        Matrix4x4 translation = Matrix4x4.Translate(new Vector3(5.0f, 0.0f, 0.0f));

        // Default matrix is a zero-matrix which will cause our vertices to be zero!
        //Matrix4x4 transformation = new Matrix4x4();

        // Sometimes the "orbiting" effect of translating before rotating may be desirable!
        Matrix4x4 transformation = rotation * translation;

        // Generally, the order is scale then rotate then translate
        Vector3[] output = Transformations.Transform(transformation, original);
        mesh.vertices = output;
    }
}