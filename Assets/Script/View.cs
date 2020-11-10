using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class View : MonoBehaviour
{
    private Vector3 mousePos;
    public GameObject iron;
    public GameObject sparkEffect;
    public GameObject smokeEffect;
    private Ray ray; 
    private RaycastHit hit;
    private float Delay = 0.1f;
    private float Tick;
    private float smokeDelay;

    public Collision coll;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        mousePos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        Tick += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit, 500f))
            {
                Debug.DrawRay(ray.origin, ray.direction * 500f, Color.red); //레이 출력(빨간선)
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
            iron.transform.localScale = new Vector3(0.3f, 0.3f, 0.05f);
        }
    }

    void hitiron()
    {
        if (Tick >= Delay)
        {
            iron.transform.localScale += new Vector3(0.003f, 0.003f, 0f);
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
            iron.transform.localScale += new Vector3(0.005f, 0.005f, 0f);
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
            iron.transform.localScale = new Vector3(0.3f, 0.3f, 0.05f);
            spark();
            Tick = 0;
            smokeDelay++;
        }
    }

    void spark()
    {
        GameObject spark = Instantiate(sparkEffect, mousePos, Quaternion.identity);
        Destroy(spark, spark.GetComponent<ParticleSystem>().duration + 0.2f);
        if(smokeDelay >= 10.0f)
        {
            GameObject smoke = Instantiate(smokeEffect, mousePos, Quaternion.identity);
            Destroy(smoke, smoke.GetComponent<ParticleSystem>().duration + 4f);
            smokeDelay = 0;
        }

    }
}