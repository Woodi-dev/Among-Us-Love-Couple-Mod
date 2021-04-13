using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;


namespace LoveCoupleMod
{
    public class Heart : CustomGameObject
    {
        public static Sprite heart;
        public static Sprite heartsmall;

        public SpriteRenderer renderer;
        private TextMeshPro parent;
        public Heart(TextMeshPro parent) : base()
        {
            this.parent = parent;
            name = "Heart";
            renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = heart;
            
            go.transform.position = parent.transform.position + new Vector3(0.52f* parent.bounds.size.x, -0.1f, 0);
            go.transform.localScale = new Vector3(0.1f, 0.1f, 1);
            go.transform.parent = parent.transform;


        }
        public Heart() : base()
        {
            name = "Heart";
            renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = heart;

      


        }
        public override void Update()
        {

        }
    }
}
