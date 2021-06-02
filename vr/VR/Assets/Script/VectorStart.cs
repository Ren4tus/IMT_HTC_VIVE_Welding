//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using VRUiKits.Utils;

//public class VectorStart : MonoBehaviour
//{
//	public float timer;
//	public List<float> ttt;
//	private Rays rayz;
//	public void Start()
//	{

//	}
//	public void Update()
//	{
//		timer += Time.deltaTime;
//	}
//	public IEnumerator updateVector()
//	{
//		for (int i = 0; ; i++)
//		{
//			yield return new WaitForSeconds(1.0f);
//			rayz = GetComponent<Rays>();
//			ttt.Add(rayz.Dist);
//			Debug.Log(ttt[i] + "," + i);
//		}
//	}

//	public void StartFeedBack()
//	{
//		StartCoroutine("updateVector");
//	}
//}
