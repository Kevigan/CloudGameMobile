using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostEffectScript : MonoBehaviour
{
    public Material mat;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //source is the full rendered scene that you would normally
        //send directly to the monitor. We are intercepting
        //this so we can do a bit more work, before passing it on.

        Graphics.Blit(source, destination, mat);
    }
}
