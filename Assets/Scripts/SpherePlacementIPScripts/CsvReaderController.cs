using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CsvReaderController : MonoBehaviour
{

    // prefab for the node to instantiate
    public GameObject nodePrefabSrc;
    public GameObject nodePrefabDst;

    // origin of the sphere
    public GameObject sphereOrigin;

    void Start()
    {
        readCsvSource();
        readCsvDest();
    }


    void Update()
    {
        
    }


    public List<float> convSpheCart(float r, float theta, float phi)
    {
        var x = r * Mathf.Cos(theta * (Mathf.PI / 180.0f)) * Mathf.Sin(phi * (Mathf.PI / 180.0f));
        var z = r * Mathf.Sin(theta * (Mathf.PI / 180.0f)) * Mathf.Sin(phi * (Mathf.PI / 180.0f));
        var y = r * Mathf.Cos(phi * (Mathf.PI / 180.0f));

        var conv_r = x / 40.0;
        var conv_phi = (z - 1.0) * (360 / 9);
        var conv_theta = (y - 1.0) * (360 / 100);

        var listCartesianCoord = new List<float>();

        listCartesianCoord.Add(x);
        listCartesianCoord.Add(z);
        listCartesianCoord.Add(y);

        //Debug.Log("r : " + r);
        //Debug.Log("theta : " + theta);
        //Debug.Log("phi : " + phi);
        //Debug.Log("x : " + x);
        //Debug.Log("z : " + z);
        //Debug.Log("y : " + y);

        return listCartesianCoord;
    }




    // -------------------------------------- READ CSV SOURCE IP
    public void readCsvSource()
    {

        // we have to store our csv in the resources folder to be accessible after the building if the app
        TextAsset fileData = Resources.Load<TextAsset>("VisuIP/disctinct_src_nodes");

        // retrieve each line of the csv as a new string
        string[] linesData = fileData.text.Split(
            new[] { Environment.NewLine },
            StringSplitOptions.None
        );

        // solve problem range....
        for (var i = 0; i<linesData.Length - 1; i++)
        {
            string[] rowArray = linesData[i].Split(',');
            var src_ip = rowArray[0];

            // extract the value of the IP src --> store as spherique coordinates first
            var r_ip = float.Parse(rowArray[1]);
            var phi_ip = float.Parse(rowArray[2]);
            var theta_ip = float.Parse(rowArray[3]);

            // spherique --> cartesian coordinates
            var listCartesianCoord = new List<float>();
            listCartesianCoord = convSpheCart(r_ip, phi_ip, theta_ip);

            // instantiate the node
            var instantiateNode = Instantiate(nodePrefabSrc, Vector3.zero, Quaternion.identity);
            instantiateNode.name = src_ip;
            instantiateNode.transform.SetParent(sphereOrigin.transform);
            instantiateNode.transform.localPosition = new Vector3(listCartesianCoord[0], listCartesianCoord[2], listCartesianCoord[1]);
        }

    }


    // -------------------------------------- READ CSV DESTINATION IP
    public void readCsvDest()
    {
        // we have to store our csv in the resources folder to be accessible after the building if the app
        TextAsset fileData = Resources.Load<TextAsset>("VisuIP/disctinct_dst_nodes");

        // retrieve each line of the csv as a new string
        string[] linesData = fileData.text.Split(
            new[] { Environment.NewLine },
            StringSplitOptions.None
        );

        // solve problem range....
        for (var i = 0; i < linesData.Length - 1; i++)
        {
            string[] rowArray = linesData[i].Split(',');
            var dst_ip = rowArray[0];

            print(dst_ip);

            // extract the value of the IP dst --> store as spherique coordinates first
            var r_ip = float.Parse(rowArray[1]);
            var phi_ip = float.Parse(rowArray[2]);
            var theta_ip = float.Parse(rowArray[3]);

            // spherique --> cartesian coordinates
            var listCartesianCoord = new List<float>();

            
            listCartesianCoord = convSpheCart(r_ip, phi_ip, theta_ip);

            // instantiate the node
            var instantiateNodeDst = Instantiate(nodePrefabDst, Vector3.zero, Quaternion.identity);
            instantiateNodeDst.name = dst_ip;
            instantiateNodeDst.transform.SetParent(sphereOrigin.transform);
            instantiateNodeDst.transform.localPosition = new Vector3(listCartesianCoord[0], listCartesianCoord[2], listCartesianCoord[1]);
        }
    }

    

}
