using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CsvReader : MonoBehaviour
{

    // path of the csv with the flow data
    private string csvPath;

    // UI
    public Text flowDataInfo;

    // store all the flow we received
    public List<Flow> flowData = new List<Flow>();

    // class to store the flow we get from the csv
    public class Flow {
        public string ip_src;
        public string ip_dst;
        public string port_src;
        public string port_dst;
        public string protocol;
        public string size;
        public string duration;
        public string date;
        public string substation;
        public string sub_type;
        public string sub_detect;

        public Flow(string ip_src, string ip_dst, string port_src, string port_dst, string protocol, string size,
            string duration, string date, string substation, string sub_type, string sub_detect)

        {
            this.ip_src = ip_src;
            this.ip_dst = ip_dst;
            this.port_src = port_src;
            this.port_dst = port_dst;
            this.protocol = protocol;
            this.size = size;
            this.duration = duration;
            this.date = date;
            this.substation = substation;
            this.sub_type = sub_type;
            this.sub_detect = sub_detect;
        }

        public string printInfo() {
            return ("Souce IP : " + this.ip_src + " Substation : " + this.substation + " Detected as : " + this.sub_detect);
        }
    }


    // we will store each position of the different substations in a dictionary 
    // we will retreieve those positions when we will instanciate the substation on the map of Arkansas
    public Dictionary<string, string[]> subPositionDict = new Dictionary<string, string[]>();
    

    void Start()
    {
        // read the csv as soon as the app starts
        ReadCsv();
        // print the info
        foreach (Flow flow in flowData)
        {
            flowDataInfo.text += flow.printInfo() + "\n";
        }
        
        ReadSubPositionCsv();
    }


    void Update()
    {

    }


    // get and convert our flow data from csv
    void ReadCsv()
    {
        // we have to store our csv in the resources folder to be accessible after the building if the app
        TextAsset fileData = Resources.Load<TextAsset>("TestFlowCsv");

        // retrieve each line of the csv as a new string
        string[] linesData = fileData.text.Split(
            new[] { Environment.NewLine },
            StringSplitOptions.None
        );

        // index of the flow
        var ind = 0;

        // split each row to instanciate a new Flow objects
        for (var i = 0; i < linesData.Length - 1; i++)
        {
            // split the row and create corresponding object
            string[] rowArray = linesData[i].Split(',');
            Flow newFlow = new Flow(rowArray[0], rowArray[1], rowArray[2], rowArray[3], rowArray[4], rowArray[5], rowArray[6], rowArray[7], rowArray[8], rowArray[9], rowArray[10]);

            newFlow.printInfo();
            flowData.Add(newFlow);

            ind ++;
        }

    }


    // get and convert our substation position data from csv
    void ReadSubPositionCsv()
    {
        TextAsset fileSubPosData = Resources.Load<TextAsset>("substation_position_3Dapp");

        // retrieve each line of the csv as a new string
        string[] linesData = fileSubPosData.text.Split(
            new[] { Environment.NewLine },
            StringSplitOptions.None
        );

        // index of the flow
        var ind = 1;

        // split each row to instanciate a new position for a substation
        // BE CAREFUL, length-1 otherwise we get an index error because of the csv --> blank line we don't want
        for (var i=0; i<linesData.Length-1; i++)
        {
            // split the row and create corresponding object
            string[] rowArray = linesData[i].Split(',');

            string[] posArray = new string[2];
            posArray[0] = rowArray[1];
            posArray[1] = rowArray[2];

            subPositionDict.Add(rowArray[0], posArray);

            ind++;
        }

    }

}
