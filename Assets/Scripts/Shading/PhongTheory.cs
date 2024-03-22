using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhongTheory : MonoBehaviour
{
    [Range(0.0f, 1.0f)] public Vector3 lightColor = new Vector3(1.0f, 0.0f, 0.0f);
    [Range(0.0f, 1.0f)] public float ambientIntensity = 0.25f;
    [Range(0.0f, 1.0f)] public float diffuseIntensity = 0.75f;
    [Range(0.0f, 256.0f)] public float specularPower = 32.0f;

    Vector3 Phong()
    {
        // Step 0: step up scene information
        Vector3 position = Vector3.zero;    // position of fragment (pixel on object)
        Vector3 lightPosition = new Vector3(1.0f, 2.0f, -6.0f);
        Vector3 cameraPosition = new Vector3(-5.0f, 5.0f, -5.0f);
        Vector3 N = Vector3.up; // normal vector pointing up (+y)

        // Step 1: calculate lighting vectors
        Vector3 L = (lightPosition - position).normalized;  // vector from fragment to light
        Vector3 V = (cameraPosition - position).normalized; // vector from fragment to camera
        Vector3 R = Vector3.Reflect(-L, N);

        // Step 2: calculate lighting vector similarities
        float dotNL = Mathf.Max(0.0f, Vector3.Dot(N, L));
        float dotVR = Mathf.Max(0.0f, Vector3.Dot(V, R));

        // Step 3: calculate phong!
        Vector3 lighting = Vector3.zero;
        lighting += lightColor * ambientIntensity;
        lighting += lightColor * diffuseIntensity * dotNL;
        lighting += lightColor * Mathf.Pow(dotVR, specularPower);
        return lighting;
    }

    public Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        Vector3 lighting = Phong();
        material.color = new Color(lighting.x, lighting.y, lighting.z);
    }
}
