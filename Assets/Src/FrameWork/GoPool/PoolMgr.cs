﻿using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HG
{
    /*
     *对象池应用遵循如下规则
     * 1.其他地方不能继续持有该对象引用，也不能操作
     * 2.应该先有取出来再有放回去。否则不符合逻辑
     * /
     */
    public class PoolMgr: SingletonMono<PoolMgr>
    {
        private readonly Dictionary<string,Pool> _pools = new Dictionary<string, Pool>();

        public void Init()
        {
            
        }

        public GameObject Get(string name)
        {
            if (!_pools.ContainsKey(name))
            {
                _pools[name] = new Pool(name);
            }

            return _pools[name].Get();
        }

        public void Push(GameObject go)
        {
            var cmp = go.GetComponent<MonoBase>();
            if (cmp)
            {
                _pools[cmp.LoadName].Push(go); 
                return;
            }
            
            Loger.Error("push before get！！logic error");
        }

        public void PrintDetail()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var pair in _pools)
            {
                stringBuilder.AppendLine($"{pair.Key}==>{pair.Value.Count}");
            }

            Loger.Color(stringBuilder.ToString(),"yellow");
        }

        public void ClearPool()
        {
            foreach (var pair in _pools)
            {
                pair.Value.ClearAll();
            }
        }

        public void ClearLimit()
        {
            foreach (var pair in _pools)
            {
                pair.Value.ClearInLimit();
            }
        }
    }
}