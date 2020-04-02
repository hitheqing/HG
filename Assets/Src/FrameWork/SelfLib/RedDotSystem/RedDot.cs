using HG;
using UnityEngine.UI;

public class RedDot:MonoObserverBase
{
    public Text Text;

    public Image NewImage;

    public Image NumBg;

    private string _key;
    private bool _showNum = true;

    
    private void SetCount(int num)
    {
        if (_showNum)
        {
            NewImage.gameObject.SetActive(false);

            NumBg.gameObject.SetActive(num != 0);

            Text.text = num.ToString();
        }
        else
        {
            NumBg.gameObject.SetActive(false);

            NewImage.gameObject.SetActive(num != 0);
        }
    }

    public void SetBindingKey(string key,bool showNum = true)
    {
        _key = key;
        _showNum = showNum;

        Subscribe(key.GetHashCode(), f => { SetCount((int) f[0]); });

        var node = RedDotMgr.Instance.Get(key);
        if (node != null)
        {
            SetCount(node.NoticeCount);
        }
    }
}