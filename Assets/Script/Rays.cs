using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRUiKits.Utils
{
    public class Rays : MonoBehaviour
    {

        public GameObject iron;
        public GameObject sparkEffect;
        public GameObject smokeEffect;

        private Vector3 mousePos;
        private Ray ray;
        private RaycastHit hit;

        private float Tick;
        private float smokeDelay;

        private float Delay = 0.1f;
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
            //mousePos = Camera.main.ScreenToWorldPoint(
            //    new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            mousePos = hit.point;
            Tick += Time.deltaTime;

            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, 50f))
                {
                    Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red); //레이 출력(빨간선)
                    if (hit.transform.CompareTag("iron")) //레이에 충돌한게 태그 iron 이면
                    {
                        hitiron();
                    }
                    else if (Vector3.Distance(mousePos, iron.transform.position) <= 5f) //거리 <= 5f
                    {
                        disiron();
                    }
                    else
                    {
                        nomaliron();
                    }
                }
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
                iron.transform.localScale += new Vector3(colltime, colltime, colltime);
                Instantiate(iron, mousePos, Quaternion.identity);
                spark();
                Tick = 0;
                smokeDelay++;
                Debug.Log("Hiting");
            }
        }

        void disiron()
        {
            if (Tick >= Delay)
            {
                iron.transform.localScale += new Vector3(distime, distime, distime);
                Instantiate(iron, mousePos, Quaternion.identity);
                spark();
                Tick = 0;
                smokeDelay++;
            }

        }

        void nomaliron()
        {
            if (Tick >= Delay)
            {
                Instantiate(iron, mousePos, Quaternion.identity);
                iron.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                spark();
                Tick = 0;
                smokeDelay++;
            }
        }

        void spark()
        {
            GameObject spark = Instantiate(sparkEffect, mousePos, Quaternion.identity);
            Destroy(spark, spark.GetComponent<ParticleSystem>().duration + 0.2f);
            if (smokeDelay >= 10.0f)
            {
                GameObject smoke = Instantiate(smokeEffect, mousePos, Quaternion.identity);
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
                    distime = 0.006f;
                }
                else if (opmgdis.selectedIdx == 2)
                {
                    distime = 0.007f;
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
                    colltime = 0.004f;
                }
                else if (opmgcoll.selectedIdx == 2)
                {
                    colltime = 0.005f;
                }
            }
        }
    }
}