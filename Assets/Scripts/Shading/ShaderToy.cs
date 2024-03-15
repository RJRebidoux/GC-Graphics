using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderToy : MonoBehaviour
{
    public Material effect;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, effect);
    }
}
