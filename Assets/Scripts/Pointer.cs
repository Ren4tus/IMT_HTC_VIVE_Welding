using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 50.0f;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;

    private LineRenderer m_LineRenderer = null;
    // Start is called before the first frame update
    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
    }
    private void UpdateLine()
    {
        float targetLength = m_DefaultLength;

        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if(hit.collider != null)
        {
            //Debug.Log(hit);
            endPosition = hit.point;
        }

        m_Dot.transform.position = endPosition;

        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }
    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);
        return hit;
    }
}
