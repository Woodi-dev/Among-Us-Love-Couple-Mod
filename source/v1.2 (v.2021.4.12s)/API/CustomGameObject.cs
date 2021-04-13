using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LoveCoupleMod
{
    public abstract class CustomGameObject
    {
        public GameObject go;
        public string name;

        protected CustomGameObject()
        {
            go = new GameObject();
           
        }
        protected Vector3 getPosition()
        {
            return go.transform.position;
        }
        protected void setPosition(Vector3 pos)
        {
            go.transform.position = pos;
        }
        public abstract void Update();

        public virtual void Destroy()
        {

            GameObject.Destroy(go);
        }
    }
}
