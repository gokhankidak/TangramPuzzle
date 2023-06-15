using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Insula.Tangram
{
    public class TangramManager : MonoBehaviour
    {
        [SerializeField] private RenderTexture problem, answer;
        [SerializeField][Range(0,1)] private float completeThreshold;
        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                CompareTextures(problem, answer);
            }
        }

        private void CompareTextures(RenderTexture renTex1, RenderTexture renTex2)
        {
            var textureSize = renTex1.width * renTex1.height;
            int sameTextureCount = 0;
            Texture2D tex1 = new Texture2D(renTex1.width, renTex1.height);
            RenderTexture.active = renTex1;
            tex1.ReadPixels(new Rect(0, 0, renTex1.width, renTex1.height), 0, 0);

            Texture2D tex2 = new Texture2D(renTex2.width, renTex2.height);
            RenderTexture.active = renTex2;
            tex2.ReadPixels(new Rect(0, 0, renTex2.width, renTex2.height), 0, 0);

            for (int x = 0; x < tex1.width; x++)
            {
                for (int y = 0; y < tex1.height; y++)
                {
                    Color32 color1 = tex1.GetPixel(x, y);
                    Color32 color2 = tex2.GetPixel(x, y);
                    if (color1.a > 0 && color2.a > 0)
                    {
                        sameTextureCount++;
                    }
                    else if(color1.a == 0 && color2.a == 0)
                    {
                        sameTextureCount++;
                    }
                }
            }

            if (sameTextureCount  >= textureSize * completeThreshold) Debug.Log("Game Completed");
        }
    }
}