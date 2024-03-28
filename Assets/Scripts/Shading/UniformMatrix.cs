using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniformMatrix : MonoBehaviour
{
    Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        float tt = Time.realtimeSinceStartup;
        Vector3 a = Vector3.forward * 5.0f;
        Vector3 b = Vector3.forward * -5.0f;

        Matrix4x4 matrix = Matrix4x4.Translate(Vector3.Lerp(a, b, Mathf.Sin(tt) * 0.5f + 0.5f));
        material.SetMatrix("_Model", matrix);
    }
}
