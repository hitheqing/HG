using UnityEngine;

namespace HG
{
    
    
    /*
     *    约定根节点->
     *    约定名字不重名，
     *    加载路径为 根节点+名字
     *
     *
     * 
     */
    
    
    public class ResourceMgr:SingletonMono<ResourceMgr>
    {
        public string GetResRootPath()
        {
            return "";
        }

        public GameObject LoadPrefab(string name)
        {
            return Resources.Load<GameObject>(GetResRootPath() + name);
        }

        internal GameObject LoadPrefabForPool(string name)
        {
            var go = LoadPrefab(name);
            var monobase = go.GetSafeComponent<MonoBase>();
            monobase.UsePool = true;
            monobase.LoadName = name;
            return go;
        }
        
        
        
        public void Check()
        {
            
        }
    }
}