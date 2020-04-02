using HG;
using UnityEngine;

public delegate string DDDe(int n);

public class testtemp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	private string target(int n)
	{
		Loger.Info(n);
		return n.ToString();
	}

	private void OnGUI()
	{
		if (GUILayout.Button("start", GUILayout.Width(400), GUILayout.Height(200)))
		{
		}
		if (GUILayout.Button("end", GUILayout.Width(400), GUILayout.Height(200)))
		{
		}
	}


	private void OnDestroy()
	{
	}
}
