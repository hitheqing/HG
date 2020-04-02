using System.Text;
using HG;
using UnityEngine.UI;

public class testChatDemo : MonoObserverBase
{

	public Button Connect;
	public Button Send;
	public Text ChatText;
	public InputField InputField;
	public VerticalLayoutGroup Content;

	private bool isconnect;
	private NetChannel netChannel;

	private void Start()
	{
		Subscribe(NetChannel.ConnectSucced, o =>
		{
			//
			isconnect = true;
			Loger.Log("ConnectSucced--->" + o[0]);
		});
		
		Subscribe(NetChannel.SendError, o =>
		{
			Loger.Log("SendError--->");
		});
		
		Subscribe(NetChannel.SendSucced, o =>
		{
			//
			byte[] bts = (byte[]) o[0];
			string words = Encoding.UTF8.GetString(bts);
			
			Loger.Log("SendSucced--->" + words);

			var text = CloneText();
			text.text = words;
		});
		
		netChannel = new NetChannel("TestDemo");
		Connect.onClick.AddListener(() =>
		{
			//
			netChannel.Connect("127.0.0.1", 4567);
		});
		
		Send.onClick.AddListener(() =>
		{
			if (isconnect)
			{
				netChannel.Send(InputField.text);
			}
		});
	}

	protected override void OnDestroy()
	{
		if (netChannel != null)
		{
			netChannel.Close();
		}
	}

	private Text CloneText()
	{
		var go = Instantiate(ChatText.transform.parent.gameObject);
		go.transform.SetParent(Content.transform);
		return go.GetComponentInChildren<Text>();
	}
}
