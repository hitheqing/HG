using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HG
{
    public class Pool
    {
        internal Pool(string name, int cap = 50)
        {
            Name = name;
            MaxCap = cap;
        }
        
        internal int MaxCap { get; private set; }
        
        internal string Name{ get; private set; }
        
        public int Count => _gameObjects.Count;

        private Stack<GameObject> _gameObjects = new Stack<GameObject>();

        internal GameObject Get()
        {
            var target = _gameObjects.Count > 0 ? _gameObjects.Pop() : ResourceMgr.Instance.LoadPrefabForPool(Name);
            
            target.gameObject.SetActive(true);
            return target;
        }

        internal void Push(GameObject go)
        {
            go.transform.parent = PoolMgr.Instance.transform;
            go.gameObject.SetActive(false);
            _gameObjects.Push(go);
        }
        
        internal void ClearInLimit()
        {
            var curcount = _gameObjects.Count;
            while (--curcount > MaxCap)
            {
                var go = _gameObjects.Pop();
                Object.Destroy(go);
            }
        }

        internal void ClearAll()
        {
            var curcount = _gameObjects.Count;
            while (--curcount > 0)
            {
                var go = _gameObjects.Pop();
                Object.Destroy(go);
            }
        }
    }
}