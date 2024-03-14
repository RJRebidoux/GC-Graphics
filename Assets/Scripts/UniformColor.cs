using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniformColor : MonoBehaviour
{
    Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        Debug.Log(material.shader);
    }

    void Update()
    {
        float tt = Time.realtimeSinceStartup;
        Color color;
        color.r = Mathf.Sin(tt * Mathf.PI * 2.0f * 0.333f) * 0.5f + 0.5f;
        color.g = Mathf.Sin(tt * Mathf.PI * 2.0f * 0.666f) * 0.5f + 0.5f;
        color.b = Mathf.Sin(tt * Mathf.PI * 2.0f * 1.000f) * 0.5f + 0.5f;
        color.a = 1.0f;

        material.SetColor("_Color", color);
    }
}
