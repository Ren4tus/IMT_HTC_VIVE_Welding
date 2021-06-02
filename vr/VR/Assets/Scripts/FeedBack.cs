using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using VRUiKits.Utils;
using JetBrains.Annotations;

public class FeedBack : MonoBehaviour
{
    public GameObject Player;
    public GameObject Metal;
    //private float Dist;
    private float RotX;
    private float RotY;
    private float Speed;


    public List<string> ttt;
    public List<float> kkk;
    public int k;
    public float sum;
    public Text text_Distance;
    public Text text_RotX;
    public Text text_RotY;
    public Text text_Speed;
    public GameObject text_notice_down;
    public GameObject text_notice_up;

    private Vector3 oldPos = new Vector3(0, 0, 0);
    private void Start()
    {
        StartCoroutine("updateVector");
    }
    void Update()
    {
        //Dist = Vector3.Distance(hit.point, iron.transform.position);
        //text_Distance.text = "거리: " + Dist;

        RotX = Mathf.Round(Player.transform.eulerAngles.x);

        text_RotX.text = "진행각: 70 < " + RotX + " < 80";
        if (RotX < 70 || RotX > 80)
        {
            text_RotX.text = "진행각: 70 < " + "<color=#ff0000>" + RotX + "</color>" + " < 80";
            if (RotX < 70)
            {
                text_notice_up.SetActive(true);
                text_notice_down.SetActive(false);
            }
            else
            {
                text_notice_down.SetActive(true);
                text_notice_up.SetActive(false);
            }
        } else //70 <= RotX <= 80
        {
            text_RotX.text = "진행각: 70 < " + "<color=#00ff00>" + RotX + "</color>" + " < 80";
            text_notice_down.SetActive(false);
            text_notice_up.SetActive(false);
        }
        RotY = Mathf.Round(Player.transform.eulerAngles.y);
        text_RotY.text = "작업각: 0 = " + RotY;
        if (RotY != 0)
        {
            text_RotY.text = "작업각: 0 = " + "<color=#ff0000>" + RotY + "</color>";
        }

        //1) V(속도) = S(거리) / T(시간)
        //2) V = rigidbody.velocity, S = transform.position - prePosition, T = Time.deltaTime
        //   rigidbody.velocity = (transform.position - prePosition) / Time.deltaTime;
        //  Vector3.distance(이전 프레임 좌표, 현재 좌표) / Time.deltatime
        //Speed = Vector3.Distance(currentFrame, Player.transform.position) / Time.deltaTime;
        //text_Speed.text = "속력: " + Speed;
        if (oldPos.x == 0 && oldPos.y == 0 && oldPos.z == 0)
        {
            oldPos = Player.transform.position;
        }
        Vector3 nowPos = Player.transform.position;
        Speed = Vector3.Distance(oldPos, Player.transform.position) / Time.deltaTime;
        oldPos = nowPos;
        text_Speed.text = "속력: " + Speed;
    }
    public IEnumerator updateVector()
    {
        for (int i = 0; ; i++)
        {
            for (k = 0; k < 10 ; k++)
            {
                yield return new WaitForSeconds(0.1f);
                kkk.Add(RotX);
                sum += kkk[k];
            }
            yield return new WaitForSeconds(1.0f);
            ttt.Add(i + "," + RotX);
            Debug.Log(ttt[i] + " 평균 " + sum / 10);
            kkk.Clear();
            sum = 0;
        }
    }
}