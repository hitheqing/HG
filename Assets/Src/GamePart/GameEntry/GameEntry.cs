using HG;
using UnityEngine;

namespace GameEntry
{
    public class GameEntry:MonoBehaviour
    {
        private void Awake()
        {
            InitFrameWork();

            RunTimeEnvironmentDetect();
            InitGameLogic();
        }

        private void InitGameLogic()
        {
            
        }

        private void RunTimeEnvironmentDetect()
        {
            
        }

        /// <summary>
        /// 初始化框架脚本
        /// </summary>
        private void InitFrameWork()
        {
            MainThreadScheduler.Instance.Init();
        }
    }
}