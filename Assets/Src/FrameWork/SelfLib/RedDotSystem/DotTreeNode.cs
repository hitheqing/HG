using System.Collections.Generic;
using HG;

/// <summary>
/// 红点系统节点
/// </summary>
public class DotTreeNode
{
    public DotTreeNode(string key)
    {
        Key = key;
    }

    /// <summary> 父节点 </summary>
    private DotTreeNode Parent { get; set; }

    /// <summary> 子节点s </summary>
    private readonly List<DotTreeNode> _childNodes = new List<DotTreeNode>();

    /// <summary> 提示数量 </summary>
    public int NoticeCount => _count;

    private int _count;

    /// <summary> 节点消息key </summary>
    public string Key { get; }

    /// <summary>
    /// 添加子节点,要求key值唯一，不重复添加
    /// </summary>
    /// <param name="node"></param>
    public bool AddChild(DotTreeNode node)
    {
        foreach (var t in _childNodes)
        {
            if (t == node || t.Key.Equals(node.Key))
            {
                Loger.Error("[dot][repeated add]--->" + node.Key);
                return false;
            }
        }

        node.Parent = this;
        _childNodes.Add(node);

        if (node._count != 0)
        {
            _IncrementUpdate(node._count);
        }
        
        return true;
    }

    /// <summary>
    /// 移除子节点
    /// </summary>
    public bool RemoveChild(DotTreeNode node)
    {
        if (!_childNodes.Contains(node))
        {
            Loger.Error("[dot][remove no exist]--->" + node.Key);
            return false;
        }

        node.Parent = null;
        _childNodes.Remove(node);

        if (node._count != 0)
        {
            _IncrementUpdate(-node._count);
        }
        
        return true;
    }

    /// <summary>
    /// 全量更新
    /// (原则上)只有最底层的子节点才可以进行 全量更新
    /// </summary>
    public void FullUpdate(int count)
    {
        
        //(实际上) 有需要直接给系统节点赋值的情况，所以注释掉以下代码
//        if (_childNodes.Count != 0)
//        {
//            Loger.Error("[dot][modify] not lowest node]--->" + Key);
//            return;
//        }
        
        var increment = count - _count;
        if (increment == 0)
        {
            return;
        }

        _IncrementUpdate(increment);
    }

    /// <summary>
    /// 增量更新
    /// 只有最底层的子节点才可以进行 增量更新
    /// </summary>
    public void IncrementUpdate(int count)
    {
        if (_childNodes.Count != 0)
        {
            Loger.Error("[dot][modify] not lowest node]--->" + Key);
            return;
        }
        
        if (count == 0)
        {
            return;
        }

        _IncrementUpdate(count);
    }

    private void _IncrementUpdate(int modify)
    {
        _count += modify;
        MarkChange();

        Parent?._IncrementUpdate(modify);
    }

    private void MarkChange()
    {
        RedDotMgr.Instance.MarkChange(this);
    }

    public void Dispose()
    {
        Parent = null;
        foreach (var t in _childNodes)
        {
            t.Dispose();
        }
        _childNodes.Clear();
    }
}