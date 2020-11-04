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
    private RaycastHit Hit;
    private float maxDistance = 300f;
    public GameObject iron;
    public GameObject sparkEffect;
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

        if (GetTrigger())
        {
            if(Physics.Raycast(transform.position, transform.forward, out Hit, maxDistance))
            {
                Debug.Log("hit point : " + Hit.point + ", distance : " + Hit.distance + ", name : " + Hit.collider.name); 
                Debug.DrawRay(transform.position, transform.forward * Hit.distance, Color.red);
                GameObject spark = (GameObject)Instantiate(sparkEffect, Hit.point, Quaternion.identity);
                Destroy(spark, spark.GetComponent<ParticleSystem>().main.duration + 0.2f);
                if (Hit.transform.tag != "iron")
                {
                    Instantiate(iron, Hit.point, Quaternion.identity);
                    iron.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                }
                else
                {

                    if (Hit.transform.localScale.x < 0.08f)
                        Hit.transform.localScale += new Vector3(0.0005f, 0.0005f, 0.0005f);
                }
            }
            else 
            { 
                Debug.DrawRay(transform.position, transform.forward * 1000f, Color.blue); 
            }
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
