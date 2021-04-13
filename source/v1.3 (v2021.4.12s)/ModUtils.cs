using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LoveCoupleMod
{
    public static class ModUtils
    {
        public static float Vec2Distance(Vector3 v1, Vector3 v2)
        {
            return (float)Math.Sqrt((double)(v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y));

        }
        public static float Vec2Distance(Vector2 v1, Vector2 v2)
        {
            return (float)Math.Sqrt((double)(v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y));
        }
        public static string Vec3ToString(Vector3 v1)
        {
            return "(" + v1.x + ", " + v1.y + ", " + v1.z + ")";
        }
        public static string Vec2ToString(Vector3 v1)
        {
            return "(" + v1.x + ", " + v1.y + ")";
        }
        public static Sprite createSprite(string path, int width, int height, int pixres, float pivotx = 0, float pivoty = 0)
        {
            Texture2D tex = createTexture(path, width, height, pixres);
            return createSpriteFromTex(tex, pixres,pivotx,pivoty);
        }
        public static Sprite createSprite(string path, int width, int height, int pixres, Vector2 pivot)
        {
            Texture2D tex = createTexture(path, width, height, pixres);
            
            return createSpriteFromTex(tex, pixres,pivot);
        }
        public static Texture2D createTexture(string path, int width, int height, int pixres)
        {
            UnityEngine.Texture2D tex = new UnityEngine.Texture2D(width, height, UnityEngine.TextureFormat.RGBA32, false);

            byte[] imgArray = File.ReadAllBytes(path);


            tex.LoadRawTextureData(imgArray);
            tex.Apply();
            return tex;
        }
        public static Sprite createSpriteFromTex(Texture2D tex, int pixres, float pivotx = 0, float pivoty = 0)
        {
           var spr= UnityEngine.Sprite.Create(tex, new UnityEngine.Rect(0, 0, tex.width, tex.height), new UnityEngine.Vector2(pivotx, pivoty), pixres, 0,SpriteMeshType.FullRect);
            
            return spr;

        }
        public static Sprite createSpriteFromTex(Texture2D tex, int pixres, Vector2 pivot)
        {
            return UnityEngine.Sprite.Create(tex, new UnityEngine.Rect(0, 0, tex.width, tex.height), pivot, pixres, 0, SpriteMeshType.FullRect);

        }
        public static Texture2D createTextureFromArray(byte[] imgArray, int width, int height)
        {

            UnityEngine.Texture2D tex = new UnityEngine.Texture2D(width, height, UnityEngine.TextureFormat.RGBA32, false);
            tex.LoadRawTextureData(imgArray);
            tex.Apply();
            return tex;
        }
    }
}
