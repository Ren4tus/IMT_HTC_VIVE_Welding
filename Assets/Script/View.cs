using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography;
using UnityEngine;

public class View : MonoBehaviour
{

    private Vector3 mousePos;
    public GameObject iron;
    public GameObject sparkEffect;
    private Ray ray; 
    private RaycastHit hit;

    void Start()
    {

    }


    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        mousePos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        if (Input.GetMouseButton(0))
        {
            if(Physics.Raycast(ray, out hit, 500f))
            {
                Debug.DrawRay(ray.origin, ray.direction * 500f, Color.red); //레이 출력(빨간선)
                if (Vector3.Distance(mousePos, iron.transform.position) <= 5f) //거리 <= 5f
                {
                    iron.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
                    Instantiate(iron, mousePos, Quaternion.identity); 
                }
                else
                {
                    Instantiate(iron, mousePos, Quaternion.identity);
                    iron.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                }

                //스파크 이펙트
                GameObject spark = (GameObject)Instantiate(sparkEffect, mousePos, Quaternion.identity);
                Destroy(spark, spark.GetComponent<ParticleSystem>().duration + 0.2f);
            }
            print(mousePos);
        }
    }
}