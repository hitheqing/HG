using HG;
using UnityEngine;

public class testEvent : MonoBehaviour
{
	private LazyObserver le;

	// Use this for initialization
	void Start()
	{
		EventMgr.Instance.Subscribe(100, (obj) => Loger.Color("a0 ob", "#123afd"));
		
		var dis = EventMgr.Instance.Subscribe(300, OnA2, 200);
		EventMgr.Instance.Subscribe(300, OnA1);

		le = new LazyObserver();

		le.Subscribe(300, p => Loger.Log("from le" + p[0] + p[1]), 500);
		EventMgr.Instance.Notify(300, 5, 6);
		
		Loger.Color("aftert");
		dis.Dispose();
		EventMgr.Instance.Notify(300, 5, 6);

	}

	void OnA1(object[] p)
	{
		Loger.Color("a1100" + p[0] + p[1]);
	}

	void OnA2(object[] p)
	{
		Loger.Color("a2200" + p[0] + p[1]);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			EventMgr.Instance.Notify(100);
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			EventMgr.Instance.Notify(100, 8);
			EventMgr.Instance.Notify(100);
		}

		if (Input.GetKeyDown(KeyCode.L))
		{
			EventMgr.Instance.Notify(300, 5, 6);
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
//			EventMgr.Inst.RemoveObserver();
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			le.Clear();
		}
	}
}
