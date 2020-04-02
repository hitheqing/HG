using UnityEngine;

namespace HG
{
    public class MonoBase:MonoBehaviour
    {
        [HideInInspector]
        public bool UsePool = false;

        [HideInInspector]
        public string LoadName = "";
        
        protected virtual void OnDestroy()
        {
            if (UsePool)
            {
                PoolMgr.Instance.Push(gameObject);
            }
        }
    }
}