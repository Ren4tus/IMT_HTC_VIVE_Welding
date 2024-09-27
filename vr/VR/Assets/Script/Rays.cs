using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VRUiKits.Utils
{
    public class Rays : MonoBehaviour
    {
        public GameObject emptyGraphPrefab;
        public WMG_Axis_Graph graph;
        public WMG_Series series1;

        public GameObject iron;
        public GameObject sparkEffect;
        public GameObject smokeEffect;

        public Ray ray;
        public RaycastHit hit;

        private float Tick;
        private float smokeDelay;
        private float disDelay = 1;

        public float Dist;
        public float RotX;
        public float RotY;
        public float Speed;

        public Text text_Distance;
        public Text text_RotX;
        public Text text_RotY;
        public Text text_Speed;

        public Vector3 nowBead;
        public Vector3 lastBead;

        public List<string> test;
        public int k;

        private float Delay = 0.1f; //초기 설정값 (딜레이, 거리비율, 충돌비율)
        private float distime = 0.005f;
        private float colltime = 0.003f;

        void Start()
        {

        }

        void Update()
        {
            SetDistime();
            SetColltime();
            SetDelay();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            /*mousePos = hit.point;
            mousePos = Camera.main.ScreenToWorldPoint(
               new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));*/

            Tick += Time.deltaTime;

            if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit, 50f) && !hit.transform.CompareTag("Player") && hit.transform.CompareTag("Plane"))
            {
                Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red); //레이 출력(빨간선)
                //거리 부분
                Dist = Vector3.Distance(hit.point, iron.transform.position);
                text_Distance.text = "거리: " + Dist; //거리 출력
                //속력 부분
                nowBead = hit.point;
                var distanceVector = (nowBead - lastBead);
                float distance = Vector3.Magnitude(distanceVector);
                float originDistance = distance / Time.deltaTime;
                float onSecDistance = originDistance * (1 / Time.deltaTime);
                text_Speed.text = "속력: " + (int)Mathf.Floor(onSecDistance) * (0.01f);
                lastBead = nowBead;
                if (Dist <= 5f) //거리 <= 5f
                {
                    disiron();
                }
                else
                {
                    nomaliron();
                }
            }
            else if(Input.GetMouseButton(0) && Physics.Raycast(ray, out hit, 50f) && !hit.transform.CompareTag("Player") && hit.transform.CompareTag("iron"))
            {
                hitiron();
            }
            else
            {
                iron.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }

        void hitiron()
        {
            if (Tick >= Delay)
            {
                if (hit.transform.localScale.x > 0.2f) return;
                hit.transform.localScale += new Vector3(colltime, colltime, colltime);
                spark();
                Tick = 0;
                smokeDelay++;
                //Debug.Log("Hiting");
            }
        }
        void disiron()
        {
            if (Tick >= Delay)
            {
                iron.transform.localScale += new Vector3(distime, distime, distime);
                Instantiate(iron, hit.point, Quaternion.identity);
                spark();
                Tick = 0;
                smokeDelay++;
                disDelay++;
                //Debug.Log("Disiron");
                if (disDelay >= 10f)
                {
                    iron.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    disDelay = 1;
                }
            }
        }

        void nomaliron()
        {
            if (Tick >= Delay)
            {
                Instantiate(iron, hit.point, Quaternion.identity);
                iron.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                spark();
                Tick = 0;
                smokeDelay++;
            }
        }

        void spark()
        {
            GameObject spark = Instantiate(sparkEffect, hit.point, Quaternion.identity);
            Destroy(spark, spark.GetComponent<ParticleSystem>().duration + 0.1f);
            if (smokeDelay >= 50.0f)
            {
                GameObject smoke = Instantiate(smokeEffect, hit.point, Quaternion.identity);
                Destroy(smoke, smoke.GetComponent<ParticleSystem>().duration + 4f);
                smokeDelay = 0;
            }

        }

        void SetDistime()
        {
            if (null != GameObject.Find("Menu"))
            {
                OptionsManager opmgdis = GameObject.Find("Distime").GetComponent<OptionsManager>();
                if (opmgdis.selectedIdx == 0)
                {
                    distime = 0.005f;
                }
                else if (opmgdis.selectedIdx == 1)
                {
                    distime = 0.008f;
                }
                else if (opmgdis.selectedIdx == 2)
                {
                    distime = 0.012f;
                }
            }
        }

        void SetDelay()
        {
            if (null != GameObject.Find("Menu"))
            {
                OptionsManager opmgdelay = GameObject.Find("Delaytime").GetComponent<OptionsManager>();
                if (opmgdelay.selectedIdx == 0)
                {
                    Delay = 0.1f;
                }
                else if (opmgdelay.selectedIdx == 1)
                {
                    Delay = 0.2f;
                }
                else if (opmgdelay.selectedIdx == 2)
                {
                    Delay = 0.3f;
                }
            }
        }

        void SetColltime()
        {
            if (null != GameObject.Find("Menu"))
            {
                OptionsManager opmgcoll = GameObject.Find("Colltime").GetComponent<OptionsManager>();
                if (opmgcoll.selectedIdx == 0)
                {
                    colltime = 0.003f;
                }
                else if (opmgcoll.selectedIdx == 1)
                {
                    colltime = 0.006f;
                }
                else if (opmgcoll.selectedIdx == 2)
                {
                    colltime = 0.009f;
                }
            }
        }

        public IEnumerator updateVector()
        {
            for (int i = 0; ; i++)
            {
                yield return new WaitForSeconds(1.0f);
                test.Add(i + "," + Dist);
                Debug.Log(test[i]);
                k = i;
            }
        }

        public void StartFeedBack()
        {
            StartCoroutine("updateVector");
        }

        public void StopFeedBack()
        {
            StopCoroutine("updateVector");
            //GameObject graphGO = GameObject.Instantiate(emptyGraphPrefab);
            //graphGO.transform.SetParent(this.transform, false);
            //graph = graphGO.GetComponent<WMG_Axis_Graph>();

            //series1 = graph.addSeries();
            //graph.xAxis.AxisMaxValue = 10;

            //List<string> groups = new List<string>();
            //List<Vector2> data = new List<Vector2>();
            //for (int i = 0; i < k; i++)
            //{
            //    string[] row = test[i].Split(',');
            //    groups.Add(row[0]);
            //    if (!string.IsNullOrEmpty(row[1]))
            //    {
            //        float y = float.Parse(row[1]);
            //        data.Add(new Vector2(i + 1, y));
            //        //data.Add(new Vector2(timer, Rotx));
            //    }
            //}

            //graph.groups.SetList(groups);
            //graph.useGroups = true;

            //graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
            //graph.xAxis.AxisNumTicks = groups.Count;

            //series1.seriesName = "속도 데이터";

            //series1.UseXDistBetweenToSpace = true;

            //series1.pointValues.SetList(data);
        }
    }
}