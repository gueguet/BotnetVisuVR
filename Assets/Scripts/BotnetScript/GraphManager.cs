using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GraphManager : MonoBehaviour
{
    
    // GameObject
    public GameObject map_arkansas;
    public GameObject substation_prefab;
    public GameObject substation_malicious_prefab;
    public GameObject source_linked;

    // UI
    public Text numberOfSubText;
    public Text timerText;

    // init the read csv script
    private CsvReader csvReader;
    public List<CsvReader.Flow> flowData = new List<CsvReader.Flow>();


    void Start()
    {

        

    }


    void Update()
    {


        // WE HAVE TO CHANGE THIS INPUT --> WITH THE CONTROLLER
        if (Input.GetKeyDown("k"))
        {

            CreateGraph();

        }

    }


    public void CreateGraph()
    {

        csvReader = GameObject.Find("CsvReaderController").GetComponent<CsvReader>();

        // get the output of our readCsv script
        flowData = csvReader.flowData;

        // display the number of substation we receive from the csv we read
        numberOfSubText.text = "Number of substation to place on the map : " + flowData.Count;


        StartCoroutine("DrawLine");


    }

    IEnumerator DrawLine()
    {

        // retrieve and use the positions information of each substation
        var subPosData = csvReader.subPositionDict;
        var ind_flow = 0;

        foreach (CsvReader.Flow row in flowData)
        {

            Debug.Log("Current flow charger " + row.sub_detect);

            var placedNode = new GameObject();

            // instanciate a node for every substation we detect -- WE HAVE TO IMPLEMENT SOMETHING TO AVOID SUBSTATION DUPLICATION

            if (row.sub_detect == "normal")
            {
                placedNode = Instantiate(substation_prefab, Vector3.zero, Quaternion.identity);
                placedNode.transform.parent = map_arkansas.transform;
                placedNode.transform.name = "Destination node " + ind_flow;
            }

            else
            {
                placedNode = Instantiate(substation_malicious_prefab, Vector3.zero, Quaternion.identity);
                placedNode.transform.parent = map_arkansas.transform;
                placedNode.transform.name = "Destination node " + ind_flow;
            }



            int x_pos;
            int y_pos;

            try
            {
                x_pos = int.Parse(subPosData[row.substation][0]);
                y_pos = int.Parse(subPosData[row.substation][1]);
                placedNode.transform.localPosition = new Vector3(x_pos, y_pos, 0f);
            }

            catch
            {
                Debug.Log("error dictionnary" + subPosData[row.substation]);
                Debug.Log(row.substation);
            }


            // place the node according to the position info of the substation

            placedNode.GetComponent<DrawLine>().destination = source_linked;
            // NEED TO ADD DATA TO DRAW LINE DEPENDING ON NODE INFO

            // attached flow information to the GameObject
            placedNode.AddComponent<DstNodeData>();
            var nodeData = placedNode.GetComponent<DstNodeData>();

            nodeData.ip_dst = row.ip_dst;
            nodeData.port_dst = row.port_dst;
            nodeData.duration = row.duration;
            nodeData.size = row.size;
            nodeData.substation = row.substation;
            nodeData.date = row.date;
            nodeData.protocol = row.protocol;

            ind_flow++;

            // extract timer from the row
            var timerString = row.date.Substring(row.date.Length - 5);
            timerText.text = "Time : " + timerString;

            yield return new WaitForSeconds(2);

        }
    }




}
