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
    private GameObject Muzzle;
    private float maxDistance = 300f;
    public GameObject iron;
    public GameObject sparkEffect;

    Vector3 oldPos;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        oldPos = transform.position;
        Muzzle = transform.GetChild(3).gameObject;
        Debug.Log(Muzzle);
        Indicator = GetComponentInChildren<Text>();
        Debug.Log(Indicator);
        //Invoke("ShowResult", 10);
        InvokeRepeating("RecordControllerData", 1,1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nowPos = transform.position;

        //Indicator.text = string.Format("Angle: {0}", (int)Mathf.Floor(transform.eulerAngles.x));
        var distanceVector = (nowPos - oldPos);
        float distance = Vector3.Magnitude(distanceVector);
        if (timer+0.3f <= Time.time)
        {
            Debug.Log(distance / Time.deltaTime);
            Indicator.text = string.Format("Speed: {0}", (int)((distance / Time.deltaTime)*1000));
            timer = Time.time;
        }
        

        oldPos = nowPos;
        if((int)Mathf.Floor(transform.eulerAngles.x) > 40 || (int)Mathf.Floor(transform.eulerAngles.x) < 20)
        {
            Indicator.color = Color.red;
        }
        else
        {
            Indicator.color = Color.white;
        }
        Debug.DrawRay(Muzzle.transform.position, Muzzle.transform.forward * Hit.distance, Color.red);
        if (GetTrigger())
        {
            if(Physics.Raycast(Muzzle.transform.position, Muzzle.transform.forward, out Hit, maxDistance))
            {
                //Debug.Log("hit point : " + Hit.point + ", distance : " + Hit.distance + ", name : " + Hit.collider.name); 
                
                GameObject spark = (GameObject)Instantiate(sparkEffect, Hit.point, Quaternion.identity);
                Destroy(spark, spark.GetComponent<ParticleSystem>().main.duration);
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
                Debug.DrawRay(Muzzle.transform.position, Muzzle.transform.forward * 1000f, Color.blue); 
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
            //Debug.Log("triggered" + HandType);
            //Debug.Log("position" + PositionList[PositionList.Count-1]);
            //Debug.Log("angle" + AngleList[AngleList.Count - 1]);
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
