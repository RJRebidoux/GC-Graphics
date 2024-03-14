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
        Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.Euler(0.0f, tt * 100.0f, 0.0f));
        material.SetMatrix("_Model", matrix);
    }
}
