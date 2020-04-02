using AStar;
using HG;
using UnityEngine;
using UnityEngine.UI;

public class testAstar : MonoBehaviour
{

	public GridLayoutGroup grid;
	public Image image;
	
	public int xCount = 80;

	public int yCount = 30;

	public int blockCount = 50;
	private AStarFindV2 AStarFindV2;
	private Image[,] images;

	// Use this for initialization
	void Start ()
	{
		images = new Image[xCount,yCount];
		for (int i = 0; i < xCount; i++)
		{
			for (int j = 0; j < yCount; j++)
			{
				var image = getImage();
				image.gameObject.name = i + "-"+j;
				images[i, j] = image;
			}
		}
		
		entry();
	}

	private void OnGUI()
	{
		if (GUILayout.Button("Test"))
		{
			entry();
		}
		
//		if (GUILayout.Button("result"))
//		{
//			if (AStarFindV2 != null)
//			{
//				var result = AStarFindV2.Result;
//				if (result != null)
//				{
//					for (var index = result.Count - 2; index >= 0; index--)
//					{
//						var t = result[index];
//				
//						images[t.X,t.Y].color = Color.yellow;
//					}
//				}
//			}
//		}
	}

	void entry()
	{
//		byte[,] map = new byte[6,5];
//		images = new Image[6,5];
//
//		map[3, 1] = 1;
//		map[3, 2] = 1;
//		map[3, 3] = 1;
//		map[3, 4] = 1;
//            
//		int sx = 1;
//		int sy = 2;
//		int tx = 5;
//		int ty = 2;
//
//		xCount = 6;yCount = 5;
		for (int i = 0; i < xCount; i++)
		{
			for (int j = 0; j < yCount; j++)
			{
				images[i, j].color = Color.white;
			}
		}
		
		byte[,] map = new byte[xCount,yCount];
		
		int sx, sy, tx, ty = 0;

		var max = xCount * yCount;

		for (int i = 0; i < blockCount; i++)
		{
			var rn = Random.Range(0, max);
			var x = rn % xCount;
			var y = rn / xCount;
			map[x, y] = 1;
		}
		
		for (int i = 0; i < xCount; i++)
		{
			for (int j = 0; j < yCount; j++)
			{
				if (map[i,j] == 1)
				{
					images[i, j].color = Color.black;
				}
			}
		}

		

		while (true)
		{
			var rn = Random.Range(0, max);
			var x = rn % xCount;
			var y = rn / xCount;

			if (map[x,y] == 0)
			{
				sx = x;
				sy = y;
				break;
			}
		}
		
		while (true)
		{
			var rn = Random.Range(0, max);
			var x = rn % xCount;
			var y = rn / xCount;

			if (map[x,y] == 0)
			{
				tx = x;
				ty = y;
				break;
			}
		}

		images[sx, sy].color = Color.green;
		images[tx, ty].color = Color.red;

		grid.constraintCount = xCount;
		LayoutRebuilder.ForceRebuildLayoutImmediate(grid.transform as RectTransform);
//		StartCoroutine(xxx());
		
		AStarFindV2 = new AStarFindV2(map,sx, sy, tx, ty);
		
		AStarFindV2.Find();

		var result = AStarFindV2.Result;
		if (result != null)
		{
			AStarFindV2.Dsth();
			for (var index = result.Count - 2; index >= 0; index--)
			{
				var t = result[index];
				
				images[t.X,t.Y].color = Color.yellow;
			}
		}
	}

	void InitMap()
	{
		
	}

	public Image getImage()
	{
		var t = Instantiate(image);
		t.gameObject.SetActive(true);
		grid.gameObject.Attach(t.gameObject);
		return t;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
