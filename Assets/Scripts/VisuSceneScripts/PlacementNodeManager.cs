using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlacementNodeManager : MonoBehaviour
{

    public GameObject nodeSourcePrefab;
    public GameObject originPlacement;

    // General info about source node
    private List<float> boundaries = new List<float>();

    // Class NodeInfo
    public class NodeInfo
    {
        public float x;
        public float y;
        public float z;


        public NodeInfo(float x, float y, float z)

        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public string printInfo()
        {
            return ("X : " + this.x + " Y : " + this.y + " Z : " + this.z);
        }


    }

    void Start()
    {

        readGeneralInfo();
        readCsvSource();


    }


    void Update()
    {
        


    }


    public void readGeneralInfo()
    {
        // we have to store our csv in the resources folder to be accessible after the building if the app
        TextAsset fileData = Resources.Load<TextAsset>("info_src");

        // retrieve each line of the csv as a new string
        string[] linesData = fileData.text.Split(
            new[] { Environment.NewLine },
            StringSplitOptions.None
        );

        // split each row to instanciate a new Flow objects
        for (var i = 0; i < linesData.Length - 1; i++)
        {
            // split the row and create corresponding object
            string[] rowArray = linesData[i].Split(',');

            boundaries.Add(float.Parse(rowArray[0]));
            boundaries.Add(float.Parse(rowArray[1]));

        }

    }

    public void readCsvSource()
    {
        // we have to store our csv in the resources folder to be accessible after the building if the app
        TextAsset fileData = Resources.Load<TextAsset>("disctinct_src_nodes");

        // retrieve each line of the csv as a new string
        string[] linesData = fileData.text.Split(
            new[] { Environment.NewLine },
            StringSplitOptions.None
        );

        Debug.Log("BOUND");
        foreach (float boundarie in boundaries)
        {
            Debug.Log(boundarie.ToString());
        }

        // split each row to instanciate a new Flow objects
        for (var i = 0; i < linesData.Length - 1; i++)
        {
            // split the row and create corresponding object
            string[] rowArray = linesData[i].Split(',');

            var nodeInfo = new NodeInfo(float.Parse(rowArray[1]), float.Parse(rowArray[2]), float.Parse(rowArray[3]));

            var sourcePlaceNode = new GameObject();
            sourcePlaceNode = Instantiate(nodeSourcePrefab, Vector3.zero, Quaternion.identity);
            sourcePlaceNode.transform.SetParent(originPlacement.transform);

            //sourcePlaceNode.GetComponent<IpInfo>().fullIP = 

            var out_x = convertData(boundaries[0], boundaries[1], nodeInfo.x);
            var out_y = convertData(boundaries[2], boundaries[3], nodeInfo.y);
            var out_z = convertData(boundaries[4], boundaries[5], nodeInfo.z);

            print(nodeInfo.printInfo());
            Debug.Log("Output values : " + out_x.ToString() + " " + out_y.ToString() + " " + out_z.ToString());

            sourcePlaceNode.transform.localPosition = new Vector3(out_x, out_y, out_z);

            //var tm = sourcePlaceNode.GetComponent<TextMesh>();
            //tm.text = "Position : 10." + nodeInfo.x + "." + nodeInfo.y + "." + nodeInfo.z; 



    
        }



    }

    public float convertData(float minValue, float maxValue, float inputValue)
    {

        //Debug.Log("min value : " + minValue);
        //Debug.Log("max value : " + maxValue);
        //Debug.Log("input value : " + inputValue);

        var outPosition = new float();

        if (inputValue != maxValue)
        {
            var offset = (minValue / maxValue);
            outPosition = (inputValue / maxValue) - offset;
        }

        else
        {
            outPosition = (inputValue / maxValue);
        }



        //Debug.Log("output : " + outPosition);

        return outPosition;
    }




}
