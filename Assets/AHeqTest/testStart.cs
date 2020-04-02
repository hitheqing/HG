using System.Collections.Generic;
using HG;
using UnityEngine;

public class testStart : MonoBehaviour
{
	private void Start()
	{
		ScriptsTime.Start();
		ReferenceMgr.Instance.RegType<MyStruct>(1000);
		ScriptsTime.Show();

		List<MyStruct> mylist = new List<MyStruct>();
		for (int i = 0; i < 1000; i++)
		{
			mylist.Add(ReferenceMgr.Instance.Get<MyStruct>());
		}
		ScriptsTime.Show();
		
		for (int i = 0; i < 1000; i++)
		{
			mylist.Add(new MyStruct());
		}
		ScriptsTime.Show();
	}
	
	
	public class MyStruct
	{
		public string id;
		public string name;
		public string abbr;//简称
		public int level;//等级
		public int exp;//经验
		public int icon;//头像
		public int point;//联盟积分
		public int recruitType;//招募类型，1为公开
		public string intro;//介绍
		public string announce;//宣言
		public int curMember;//当前人数
		public int maxMember;//最大人数
		public string leaderUid;//盟主uid
		public string leaderName;//盟主name
		public int leaderIcon;//盟主icon
		public long powerRestriction;//战力限制条件
		public int castleRestriction;//城堡等级限制条件
		public long createTime;
		public long totalPower;//总战力
	}
	
	

}
