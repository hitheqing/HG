namespace HG
{
    /// <summary>
    /// 游戏数据存储接口
    /// </summary>
    public interface IGameDB
    {
        string Name { get; }
        void ClearCache();
    }
}