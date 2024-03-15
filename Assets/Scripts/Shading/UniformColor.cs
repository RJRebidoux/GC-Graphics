using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniformColor : MonoBehaviour
{
    Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        material.SetColor("_Color", Color.red);
    }
}
