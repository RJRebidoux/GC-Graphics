using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phong : MonoBehaviour
{
    public GameObject[] objects;
    Material phong;

    public Color lightColor;

    [Range(0.0f, 1.0f)]
    public float ambient;

    void Start()
    {
        phong = GetComponent<MeshRenderer>().material;
        for (int i = 0; i < objects.Length; i++)
            objects[i].GetComponent<MeshRenderer>().material = phong;
    }

    void Update()
    {
        phong.SetColor("_LightColor", lightColor);
        phong.SetFloat("_Ambient", ambient);
    }
}
