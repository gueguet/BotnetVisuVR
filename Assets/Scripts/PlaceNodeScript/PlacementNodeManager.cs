using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlacementNodeManager : MonoBehaviour
{

    public GameObject nodeSourcePrefab, nodeDestinationPrefab;
    public GameObject originPlacement;

    // General info about source node
    private List<float> boundaries = new List<float>();
    private List<string> srcNodeList = new List<string>();


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

        readCsvSource();


    }


    void Update()
    {

        if (Input.GetKeyDown("k"))
        {

            foreach (string ip in srcNodeList)
            {
                Debug.Log(ip);
            }

        }

    }





    public void readCsvSource()
    {

        // we have to store our csv in the resources folder to be accessible after the building if the app
        TextAsset fileData = Resources.Load<TextAsset>("TestFlowCsv");

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

            // SOURCE NODE
            // store the src node (without duplicate)


            if (!srcNodeList.Contains(rowArray[0]))
            {
                srcNodeList.Add(rowArray[0]);

                
                var destinationNode = new GameObject();
                destinationNode = Instantiate(nodeSourcePrefab, Vector3.zero, Quaternion.identity);
                destinationNode.transform.SetParent(originPlacement.transform);
                

                
                var src_ip = rowArray[0];

                Debug.Log(src_ip);

                // extract the value of the IP dst

                var x_src_ip = float.Parse(src_ip.Substring(3, 1));
                var y_src_ip = float.Parse(src_ip.Substring(5, 1));
                var z_src_ip = float.Parse(src_ip.Substring(7));

                destinationNode.transform.localPosition = new Vector3((x_src_ip / 255), (y_src_ip / 255), (z_src_ip / 255));

                destinationNode.name = rowArray[1].ToString();
                

            }


            // DST NODE 
            // extract IP dst of the flow
            var dst_ip = rowArray[1];
            var ip_length = dst_ip.Length;

            // extract the value of the IP dst
            var x_dst_ip = float.Parse(dst_ip.Substring(3, 2));
            var y_dst_ip = float.Parse(dst_ip.Substring(6, 1));
            var z_dst_ip = float.Parse(dst_ip.Substring(8));

            //// be careful of the legnth of the id
            //switch (ip_length)
            //{
            //    case 9:
            //        z_dst_ip = float.Parse(dst_ip.Substring(8, 2));
            //        break;

            //    case 10:
            //        z_dst_ip = float.Parse(dst_ip.Substring(8, 3));
            //        break;

            //    case 11:
            //        z_dst_ip = float.Parse(dst_ip.Substring(8, 4));
            //        break;
            //}
            


            // store info of the node
            var nodeInfo = new NodeInfo(x_dst_ip, y_dst_ip, z_dst_ip);

            var sourcePlaceNode = new GameObject();
            sourcePlaceNode = Instantiate(nodeDestinationPrefab, Vector3.zero, Quaternion.identity);
            sourcePlaceNode.transform.SetParent(originPlacement.transform);

            sourcePlaceNode.transform.localPosition = new Vector3((x_dst_ip / 255), (y_dst_ip / 255), (z_dst_ip / 255));

            sourcePlaceNode.name = rowArray[1].ToString();

        }

    }

}
