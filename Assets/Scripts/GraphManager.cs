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
    public GameObject source_linked;

    // UI
    public Text numberOfSubText;

    // init the read csv script
    private CsvReader csvReader;


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
        var flowData = csvReader.flowData;

        // display the number of substation we receive from the csv we read
        numberOfSubText.text = "Number of substation to place on the map : " + flowData.Count;

        // retrieve and use the positions information of each substation
        var subPosData = csvReader.subPositionDict;
        var ind_flow = 0;

        

        foreach (CsvReader.Flow row in flowData)
        {
            Debug.Log("test print the src node ip for each row : " + row.ip_src);

            // instanciate a node for every substation we detect -- WE HAVE TO IMPLEMENT SOMETHING TO AVOIR SUBSTATION DUPLICATION
            GameObject new_substation_instance = Instantiate(substation_prefab, Vector3.zero, Quaternion.identity);
            new_substation_instance.transform.parent = map_arkansas.transform;
            new_substation_instance.transform.name = "Destination node " + ind_flow;

            int x_pos;
            int y_pos;

            try
            {
                x_pos = int.Parse(subPosData[row.substation][0]);
                y_pos = int.Parse(subPosData[row.substation][1]);
                new_substation_instance.transform.localPosition = new Vector3(x_pos, y_pos, 0f);
            }

            catch
            {
                Debug.Log("error dictionnary" + subPosData[row.substation]);
                Debug.Log(row.substation);
            }


            // place the node according to the position info of the substation

            new_substation_instance.GetComponent<DrawLine>().destination = source_linked;
            // NEED TO ADD DATA TO DRAW LINE DEPENDING ON NODE INFO

            // attached flow information to the GameObject
            new_substation_instance.AddComponent<DstNodeData>();
            var nodeData = new_substation_instance.GetComponent<DstNodeData>();

            nodeData.ip_dst = row.ip_dst;
            nodeData.port_dst = row.port_dst;
            nodeData.duration = row.duration;
            nodeData.size = row.size;
            nodeData.substation = row.substation;
            nodeData.date = row.date;
            nodeData.protocol = row.protocol;

            ind_flow++;
        }



    }




}
