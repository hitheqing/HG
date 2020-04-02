using System;
using System.Collections.Generic;
using System.Linq;

namespace HG
{
    /// <summary>
    /// 游戏数据总管理器，不保存任何数据，但是保存所有子数据存储对象
    /// 并且在需要清除的时候统一清除
    /// 原则上。存储全局数据的对象不会删掉，只会清除缓存
    /// </summary>
    public class GameDBMgr:Singleton<GameDBMgr>
    {
        private Dictionary<string,IGameDB> _dbList = new Dictionary<string,IGameDB>();

        public void Register(IGameDB gameDb)
        {
            if (_dbList.ContainsKey(gameDb.Name))
            {
                Loger.Error("repeated element");
                return;
            }
            
            _dbList.Add(gameDb.Name,gameDb);
        }

        /// <summary>
        /// 清除游戏缓存，各个模块自行实现接口清除
        /// </summary>
        public void Clear()
        {
            foreach (var t in _dbList)
            {
                t.Value.ClearCache();
            }
            
            GC.Collect();
        }
    }
}