using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    private float Speed;
    private Vector3 oldPos = new Vector3(-14, 0, -3);
    private GameObject controller;
    private GameObject target;

    private List<int> DistanceList = new List<int>();
    private List<int> DistanceList2 = new List<int>();
    private List<int> DistanceList3 = new List<int>();

    public float speed = 20f;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetChild(0).gameObject;
        target = transform.Find("Target").gameObject;
        Debug.Log(controller.transform.position);
        Invoke("ShowResult", 10);
        InvokeRepeating("RecordDistance", 1, 1.0f);

        timer = Time.time;
    }

    private void ShowResult()
    {
        GameObject resultCanvas = GameObject.Find("Canvas");
        Window_Graph resultGraph = GameObject.Find("Window_Graph").GetComponent<Window_Graph>();
        resultCanvas.transform.GetComponent<Canvas>().enabled = true;
        Debug.Log(resultGraph);
        Debug.Log(DistanceList);
        resultGraph.valueList = DistanceList;
        resultGraph.ShowGraph(resultGraph.valueList);

        GameObject resultCanvas2 = GameObject.Find("Canvas");
        Window_Graph resultGraph2 = GameObject.Find("Window_Graph").GetComponent<Window_Graph>();
        resultCanvas2.transform.GetComponent<Canvas>().enabled = true;
        Debug.Log(resultGraph2);
        Debug.Log(DistanceList2);
        resultGraph2.valueList = DistanceList2;
        resultGraph2.ShowGraph(resultGraph.valueList);

        GameObject resultCanvas3 = GameObject.Find("Canvas");
        Window_Graph resultGraph3 = GameObject.Find("Window_Graph").GetComponent<Window_Graph>();
        resultCanvas3.transform.GetComponent<Canvas>().enabled = true;
        Debug.Log(resultGraph3);
        Debug.Log(DistanceList3);
        resultGraph3.valueList = DistanceList3;
        resultGraph3.ShowGraph(resultGraph.valueList);

        CancelInvoke("RecordDistance");
    }
    private void RecordDistance()
    {
        /*float distance = Vector3.Distance(controller.transform.position, target.transform.position);
        DistanceList.Add((int)Mathf.Floor(distance));
        Debug.Log(DistanceList[DistanceList.Count - 1]);


        float rotx = Mathf.Round(controller.transform.eulerAngles.x);
        DistanceList2.Add((int)Mathf.Floor(rotx));
        Debug.Log(DistanceList2[DistanceList2.Count - 1]);*/

        

        //Indicator.text = string.Format("Angle: {0}", (int)Mathf.Floor(transform.eulerAngles.x));
        
    }
    private void FixedUpdate()
    {

        //Debug.Log("Move");
        //controller.transform.Translate(Vector3.right * Time.deltaTime * speed);
        
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 nowPos = controller.transform.position;
        var distanceVector = (nowPos - oldPos);
        float distance = Vector3.Magnitude(distanceVector);
        if (timer + 0.3f <= Time.time)
        {
            Debug.Log("a" + distance);
            Debug.Log("time" + Time.deltaTime);
            Debug.Log("value" + distance/Time.deltaTime*10);
            //Debug.Log("a" + oldPos);
            //Debug.Log("b" + nowPos);
            //Indicator.text = string.Format("Speed: {0}", (int)((distance / Time.deltaTime) * 1000)); 
            DistanceList3.Add((int)((distance / Time.deltaTime) * 10));
            timer = Time.time;
        }

        oldPos = nowPos;

        //Debug.Log(DistanceList3[DistanceList3.Count - 1]);

    }
}
