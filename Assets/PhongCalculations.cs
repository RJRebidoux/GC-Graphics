using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhongCalculations : MonoBehaviour
{
    Material material;
    [Range(0.0f, 1.0f)] public float ambientIntensity = 0.25f;
    [Range(0.0f, 1.0f)] public float diffuseIntensity = 0.75f;
    [Range(2.0f, 256.0f)] public float specularPower = 32.0f;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        // Step 0: define all starting data
        Vector3 cameraPosition = new Vector2(2.0f, 1.0f);
        Vector3 fragmentPosition = new Vector2(0.0f, 0.0f);
        Vector3 N = new Vector2(0.0f, 1.0f);    // normal
        Vector3 lightPosition = new Vector2(-5.0f, 5.0f);

        // Step 1: Calculate lighting vectors
        Vector3 L = (lightPosition - fragmentPosition).normalized;      // FROM fragment TO light
        Vector3 V = (cameraPosition - fragmentPosition).normalized;     // FROM fragment TO camera
        Vector3 R = Vector3.Reflect(-L, V);                             // Reflection about normal

        // Step 2: Calculate lighting vector similarities
        float dotNL = Vector3.Dot(N, L);
        float dotVR = Vector3.Dot(V, R);

        // Step 3: Apply ambient, diffuse, and specular contributions
        Vector3 lightColor = new Vector3(185.0f / 255.0f, 122.0f / 255.0f, 87.0f / 255.0f);

        Vector3 lighting = Vector3.zero;
        lighting += lightColor * ambientIntensity;
        lighting += lightColor * diffuseIntensity * dotNL;
        lighting += lightColor * Mathf.Pow(dotVR, specularPower);

        material.color = new Color(lighting.x, lighting.y, lighting.z);
        Debug.Log(lighting);
    }
}
