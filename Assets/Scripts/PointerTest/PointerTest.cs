using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerTest : MonoBehaviour
{

    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;

    private LineRenderer m_LineRenderer = null;

    public Text testtest;

    public VRInputTest modelutestinput;


    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();

    }


    void Update()
    {
        UpdateLine();    


    }


    private void UpdateLine()
    {
        float targetLength = m_DefaultLength;

        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if (hit.collider != null)
        {
            endPosition = hit.point;
            testtest.text = hit.transform.gameObject.name;

        }

        m_Dot.transform.position = endPosition;

        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);


    }


    private RaycastHit CreateRaycast(float lenght)
    {

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);

        return hit;
    }

}
