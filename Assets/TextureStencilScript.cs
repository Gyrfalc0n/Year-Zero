using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//turns off the referenced renderer if the fog camera sees the transform's position on a "fog" pixel
public class TextureStencilScript : MonoBehaviour
{
    public Camera cam; //The Camera using the masked render texture
    public Renderer myRenderer; // reference to the render you want toggled based on the position of this transform
    [Range(0f, 1f)] public float threshold = 0.1f; //the threshold for when this script considers myRenderer should render

    // made so all instances share the same texture, reducing texture reads
    private static Texture2D myT2D;
    private static Rect r_rect;
    private static bool isDirty = true;// used so that only one instance will update the RenderTexture per frame

    private Color GetColorAtPosition()
    {
        if (!cam)
        {
            // if no camera is referenced script assumes there no fog and will return white (which should show the entity)
            return Color.white;
        }

        RenderTexture renderTexture = cam.targetTexture;
        if (!renderTexture)
        {
            //fallback to Camera's Color
            return cam.backgroundColor;
        }

        if (myT2D == null || renderTexture.width != r_rect.width || renderTexture.height != r_rect.height)
        {
            r_rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            myT2D = new Texture2D((int)r_rect.width, (int)r_rect.height, TextureFormat.RGB24, false);
        }

        if (isDirty)
        {
            RenderTexture.active = renderTexture;
            myT2D.ReadPixels(r_rect, 0, 0);
            RenderTexture.active = null;
            isDirty = false;
        }

        var pixel = cam.WorldToScreenPoint(transform.position);
        return myT2D.GetPixel((int)pixel.x, (int)pixel.y);
    }

    private void Update()
    {
        isDirty = true;
    }

    void LateUpdate()
    {
        if (!myRenderer)
        {
            this.enabled = false;
            return;
        }

        myRenderer.enabled = GetColorAtPosition().grayscale >= threshold;
    }
}
