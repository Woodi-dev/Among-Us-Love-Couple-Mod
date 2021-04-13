using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LoveCoupleMod
{
    public static class CustomGameObjectManager
    {
        public static List<CustomGameObject> CustomGameObjects = new List<CustomGameObject>();
        private static List<CustomGameObject> toDelete = new List<CustomGameObject>();
        private static List<CustomGameObject> toAdd = new List<CustomGameObject>();

        public static void AddObject(CustomGameObject obj)
        {
            toAdd.Add(obj);
        }
        public static void Update()
        {
            foreach(CustomGameObject obj in toDelete)
            {
                CustomGameObjects.Remove(obj);

            }
            foreach (CustomGameObject obj in toAdd)
            {
                CustomGameObjects.Add(obj);

            }
            toDelete.Clear();
            toAdd.Clear();
            foreach(CustomGameObject obj in CustomGameObjects)
            {
                if (obj.go != null)
                {
                   if(obj.go.activeSelf)
                    obj.Update();
                }
            }
        }
        public static void deleteAll()
        {
            foreach(CustomGameObject obj in CustomGameObjects)
            {
                obj.Destroy();
            }
            CustomGameObjects.Clear();
        }
        public static void Remove(CustomGameObject obj)
        {
            toDelete.Add(obj);
            obj.Destroy();
        }

        public static CustomGameObject[] getByName(string name)
        {
            List<CustomGameObject> list = new List<CustomGameObject>();
            foreach (CustomGameObject obj in CustomGameObjects)
            {
                if (obj.name == name) list.Add(obj);
            }
            return list.ToArray();
        }
        public static void RemoveAllByName(string name)
        {
            
            foreach (CustomGameObject obj in CustomGameObjects)
            {
                if (obj.name == name) Remove(obj);
            }
      
        }
    }
}
