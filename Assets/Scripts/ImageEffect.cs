using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageEffect : MonoBehaviour
{
    public Material effect;

    // This essentially treats our game window as a giant image that we can read from and write to!
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Runs our "ShaderToy" shader for every pixel in our image
        Graphics.Blit(source, destination, effect);
    }
}
