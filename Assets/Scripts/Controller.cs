using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{
    public SteamVR_Input_Sources HandType;
    public SteamVR_Action_Boolean triggerAction;

    private List<int> PositionList = new List<int>();
    private List<int> AngleList = new List<int>();

    private Text Indicator;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject Temp = transform.GetChild(1).gameObject;
        Indicator = GetComponentInChildren<Text>();
        Debug.Log(Indicator);
        //Invoke("ShowResult", 10);
        InvokeRepeating("RecordControllerData", 1,1);
    }

    // Update is called once per frame
    void Update()
    {

        Indicator.text = string.Format("Angle: {0}", (int)Mathf.Floor(transform.eulerAngles.x));
        if((int)Mathf.Floor(transform.eulerAngles.x) > 40 || (int)Mathf.Floor(transform.eulerAngles.x) < 20)
        {
            Indicator.color = Color.red;
        }
        else
        {
            Indicator.color = Color.white;
        }
        
    }
    public bool GetTrigger()
    {
        return triggerAction.GetState(HandType);
    }
    public void RecordControllerData()
    {
        //Debug.Log("record");
        if (GetTrigger())
        {
            PositionList.Add(Mathf.Abs((int)Mathf.Floor(transform.position.x * 10)));
            AngleList.Add((int)Mathf.Floor(transform.eulerAngles.x));
            Debug.Log("triggered" + HandType);
            Debug.Log("position" + PositionList[PositionList.Count-1]);
            Debug.Log("angle" + AngleList[AngleList.Count - 1]);
        }
    }
    private void ShowResult()
    {
        GameObject resultCanvas = GameObject.Find("Canvas");
        Window_Graph resultGraph = GameObject.Find("Window_Graph").GetComponent<Window_Graph>();
        resultCanvas.transform.GetComponent<Canvas>().enabled = true;
        Debug.Log(resultGraph);
        Debug.Log(PositionList);
        resultGraph.valueList = PositionList;
        resultGraph.ShowGraph(resultGraph.valueList);
        CancelInvoke("RecordDistance");

    }
}
