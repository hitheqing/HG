using System;
using System.Text;

public partial class RedDotMgr
{
    //邮件系统
    public const string MailRoot = "MailRoot";
    public const string MailCategory = "MailCategory_";

    //联盟系统
    public const string UnionRoot = "UnionRoot";
    public const string UnionCategory = "UnionCategory_";

    public void Init()
    {
        //主界面邮件红点
        AddModule(MailRoot);
        AddModule(UnionRoot);
        
        //邮件分类红点
        foreach (int t in Enum.GetValues(typeof(MailCategory))) 
        {
            AddSubModule(MailRoot, MailCategory + t);  
        }

        foreach (int t in Enum.GetValues(typeof(UnionCategoryEnum)))
        {
            AddSubModule(UnionRoot, UnionCategory + t);
        }
    }

    public DotTreeNode GetMailNode(int category)
    {
        return Get(MailCategory + category);
    }

    public DotTreeNode GetUnionNode(int category)
    {
        return Get(UnionCategory + category);
    }
    
    public void Dispose()
    {
        Clear();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        
        foreach (var t in _map)
        {
            sb.AppendLine(t.Key + "--->" + t.Value.NoticeCount);
        }

        return string.Format("<color=red>{0}</color>", sb);
    }
}

public enum UnionCategoryEnum
{
}

public enum MailCategory
{
}