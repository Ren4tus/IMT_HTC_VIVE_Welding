using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    private GameObject controller;
    private GameObject target;

    private List<int> DistanceList = new List<int>();

    public float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetChild(0).gameObject;
        target = transform.Find("Target").gameObject;
        Debug.Log(controller.transform.position);
        Invoke("ShowResult", 10);
        InvokeRepeating("RecordDistance", 1, 1.0f);
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
        CancelInvoke("RecordDistance");

    }
    private void RecordDistance()
    {
        
        float distance = Vector3.Distance(controller.transform.position, target.transform.position);
        float speed = distance / 1.0f;
        DistanceList.Add((int)Mathf.Floor(distance));

        Debug.Log(DistanceList[DistanceList.Count - 1]);
    }
    private void FixedUpdate()
    {

        //Debug.Log("Move");
        //controller.transform.Translate(Vector3.right * Time.deltaTime * speed);
        
    }
    // Update is called once per frame
    void Update()
    {


    }
}
