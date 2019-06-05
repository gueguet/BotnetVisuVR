using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SrcNodeData : MonoBehaviour
{

    // class to store the flow we get from the csv
    public string ip_src;
    public string port_src;

    // UI 
    public Text IPSrcText;
    public Text PortSrcText;

    public void Start()
    {
        IPSrcText.text = ip_src;
        PortSrcText.text = port_src;
    }





}
