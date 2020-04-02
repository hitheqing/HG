using System.Collections.Generic;
using HG;

/// <summary>
/// 红点系统处理节点关系，获取节点
/// </summary>
public partial class RedDotMgr : Singleton<RedDotMgr>
{
    private DotTreeNode RootNode = new DotTreeNode("RooTNode");

    private readonly Dictionary<string, DotTreeNode> _map = new Dictionary<string, DotTreeNode>();

    /// <summary>
    /// 节点更新,派发消息
    /// </summary>
    public void MarkChange(DotTreeNode node)
    {
        if (node != RootNode)
        {
            EventMgr.Instance.Notify(node.Key.GetHashCode(), node.NoticeCount); 
        }
    }
    
    /// <summary>
    /// 获取节点数据类
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public DotTreeNode Get(string key)
    {
        return _map.ContainsKey(key) ? _map[key] : null;
    }

    /// <summary>
    /// 添加模块
    /// </summary>
    private void AddModule(string moduleName)
    {
        var node = new DotTreeNode(moduleName);

        if (RootNode.AddChild(node))
        {
            _map[moduleName] = node;
        }
    }

    /// <summary>
    /// 添加子模块
    /// </summary>
    private void AddSubModule(string mainName, string subName)
    {
        var parent = Get(mainName);
        if (parent == null)
        {
            Loger.Error("[dot][main module null]");
            return;
        }

        var node = new DotTreeNode(subName);

        if (parent.AddChild(node))
        {
            _map[subName] = node;
        }
    }

    /// <summary>
    /// 移除模块，通常不会用到
    /// </summary>
    private void RemoveModule(string moduleName)
    {
        var node = Get(moduleName);
        if (node == null)
        {
            Loger.Error("[dot][main module null]");
            return;
        }

        if (RootNode.RemoveChild(node))
        {
            _map.Remove(moduleName);
        }
    }

    /// <summary>
    /// 移除子模块，通常不会用到
    /// </summary>
    private void RemoveSubModule(string mainName, string subName)
    {
        var parent = Get(mainName);
        var child = Get(subName);

        if (parent == null || child == null)
        {
            Loger.Error("[dot][main module null]");
            return;
        }

        if (parent.RemoveChild(child))
        {
            _map.Remove(subName);
        }
    }

    private void Clear()
    {
        _map.Clear();
        RootNode.Dispose();
    }
}