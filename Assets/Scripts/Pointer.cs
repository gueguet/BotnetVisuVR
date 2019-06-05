using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{

    public float m_DefaultLenght = 5.0f;
    public GameObject dot;

    // GameObject we touch with the pointer
    public GameObject currentObject;

    // UI
    public Text objectHitName;
    public Text saveNodeText;

    private Material previousMaterial;

    // SteamVR Input
    public VRInputTest m_InputModule;

    private bool csvLoad = false;

    //public GameObject saveNodeTouched;

    private LineRenderer lineRenderer = null;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLine();
    }


    private void UpdateLine()
    {
        // User default or distance
        //PointerEventData data = m_InputModule.GetData();




        // if we don't hit anything --> use the default lenght
        //float targetLength = data.pointerPressRaycast.distance == 0 ? m_DefaultLenght : data.pointerCurrentRaycast.distance;
        var targetLength = m_DefaultLenght;

        // Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        // Default (if we don't hit anything)
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

       

        // Or based on hit
        if (hit.collider != null)
        {
            // update position of the pointer
            endPosition = hit.point;
            currentObject = hit.transform.gameObject;



            //if (hit.collider.transform.name == "ButtonLaunch" && csvLoad == false)
            //{
            //    GraphManager graphManager = GameObject.Find("GraphController").GetComponent<GraphManager>();
            //    graphManager.CreateGraph();
            //    csvLoad = true;
            //}



            try
            {
                string nodeInfoString = "";
                    
                nodeInfoString += "<b> Distance IP : </b>" + currentObject.GetComponent<DstNodeData>().ip_dst + "\n";
                nodeInfoString += "<b> Distance port : </b>" + currentObject.GetComponent<DstNodeData>().port_dst + "\n";
                nodeInfoString += "<b> Protocol : </b>" + currentObject.GetComponent<DstNodeData>().protocol + "\n";
                nodeInfoString += "<b> Flow sizes : </b>" + currentObject.GetComponent<DstNodeData>().size + " bytes \n";
                nodeInfoString += "<b> Flow duration : </b>" + currentObject.GetComponent<DstNodeData>().duration + " ms \n";
                nodeInfoString += "<b> Emission date : </b>" + currentObject.GetComponent<DstNodeData>().date + "\n";
                nodeInfoString += "<b> Substation : </b>" + currentObject.GetComponent<DstNodeData>().substation + "\n";

                objectHitName.text = nodeInfoString;

                hit.transform.gameObject.GetComponent<Renderer>().material = (Material) Resources.Load("GreenSubNodeMat");
                hit.transform.gameObject.GetComponent<LineRenderer>().startColor = Color.green;
                hit.transform.gameObject.GetComponent<LineRenderer>().endColor = Color.green;

                //saveNodeTouched = hit.transform.gameObject;

                //saveNodeText.text = saveNodeTouched.name;

            }

            catch
            {
                objectHitName.text = "No substation touched";
            }





        }
        

        // FIND A WAY TO RETRIEVE THE OLD MATERIAL FOR THE NON TOUCH NODES
        //else
        //{
        //    if (currentObject.tag == "SubNode")
        //    currentObject.transform.GetComponent<Renderer>().material = (Material) Resources.Load("SubNodeMat");
        //}

        // Set position of the dot
        dot.transform.position = endPosition;

        // Set linerenderer
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }


    // method to create a raycast depending on the lenght of the pointer
    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLenght);
        return hit;
    }

}
