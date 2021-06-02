using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRUiKits.Utils;
public class X_Tutorial : FeedBack
{
	public GameObject emptyGraphPrefab;
	public WMG_Axis_Graph graph;
	public WMG_Series series1;
	//public GameObject Player;
	//public GameObject Metal;

	public FeedBack test;

	public float Rotx;
	public float timer;

	public List<Vector2> series1Data;
	public bool useData2;
	public List<string> series1Data2;

    // 초기화에 사용

    void Awake()
    {
		GameObject graphGO = GameObject.Instantiate(emptyGraphPrefab);
		graphGO.transform.SetParent(this.transform, false);
		graph = graphGO.GetComponent<WMG_Axis_Graph>();

		series1 = graph.addSeries();
		graph.xAxis.AxisMaxValue = 10;
		//Rotx = Mathf.Round(Player.transform.eulerAngles.x);
	}

    void Update()
    {
		//if (timer + 1f <= Time.time)
		//{
		//	Rotx = Mathf.Round(Player.transform.eulerAngles.x);
		//	timer = Time.time;
		//}
		if (useData2)
		{
			//if (timer < 10.1f) { }
			List<string> groups = new List<string>();
			List<Vector2> data = new List<Vector2>();
			for (int i = 0; i < test.ttt[i].Length; i++)
			{
				string[] row = test.ttt[i].Split(',');
				groups.Add(row[0]);
				if (!string.IsNullOrEmpty(row[1]))
				{
					float y = float.Parse(row[1]);
					data.Add(new Vector2(i + 1, y));
					//data.Add(new Vector2(timer, Rotx));
				}
			}
			graph.groups.SetList(groups);
			graph.useGroups = true;

			graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
			graph.xAxis.AxisNumTicks = groups.Count;

			series1.seriesName = "속도 데이터";

			series1.UseXDistBetweenToSpace = true;

			series1.pointValues.SetList(data);
		}

		else
		{
			series1.pointValues.SetList(series1Data);
		}
	}

	//public IEnumerator updateVector()
	//{
	//	yield return new WaitForSeconds(1.0f);
	//	for(int i = 0; i < timer; i++)
	//	{
	//	}
	//}

	//public void StartFeedBack()
	//{
	//	StartCoroutine("updateVector");
	//}
}