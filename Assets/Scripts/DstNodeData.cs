using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script to store some data attached to particular node
public class DstNodeData : MonoBehaviour
{
    // TOTAL INHERITANCE IN C# ??
    // class to store the flow we get from the csv
    public string ip_dst;
    public string port_dst;
    public string protocol;
    public string size;
    public string duration;
    public string date;
    public string substation;

}
