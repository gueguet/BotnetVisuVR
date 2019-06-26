using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;
using UnityEngine.SceneManagement;

public class VRInputTest : BaseInputModule
{

    // UI
    //public Text actionText;
    //public Text currentObjectText;
    //public Text launchText;
    //public Text currentVRText;

    // Settings UI intereactable
    //public Dropdown m_Dropdown;
    //public Text m_Dropdownvalue;

    public Camera m_Camera;

    // target source is the controller we want to use --> here the right hand
    public SteamVR_Input_Sources m_TargetSource;
    // this is the input of the controller --> here the trigger
    public SteamVR_Action_Boolean m_ClickAction;

    private GameObject m_CurrentObject = null;
    private PointerEventData m_Data = null;

    private GameObject maliciousNode;

    protected override void Awake()
    {
        base.Awake();

        m_Data = new PointerEventData(eventSystem);
    }

    // call like Update
    public override void Process()
    {
        // reset data, set camera
        m_Data.Reset();
        m_Data.position = new Vector2(m_Camera.pixelWidth /2 , m_Camera.pixelHeight / 2);

        // raycast
        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;

        // clear
        m_RaycastResultCache.Clear();

        // hover
        HandlePointerExitAndEnter(m_Data, m_CurrentObject);

        // get current object
        string nameSelectedObject = m_CurrentObject.transform.name;
        //currentObjectText.text = nameSelectedObject;

        // UI dropdown
        //bool dropDownOpen = false;
        //int m_DropdownValue = m_Dropdown.value;
        //m_Dropdownvalue.text = m_Dropdown.options[m_DropdownValue].text;




        //currentVRText.text = maliciousNode.name;
        //currentVRText.text = "Ici print le nom noeud malicieux";

        // press
        if (m_ClickAction.GetStateDown(m_TargetSource))
        {
            //actionText.text = "trigger down";



            switch (nameSelectedObject)
            {
                case "CreateGraphButton":
                    GraphManager graphManager = GameObject.Find("GraphController").GetComponent<GraphManager>();
                    graphManager.CreateGraph();
                    break;

                // ERROR SOME THINKS ARE NOT DESTROY...
                case "ResetAppButton":
                    foreach (GameObject o in FindObjectsOfType<GameObject>())
                    {
                        Destroy(o);
                    }
                    SceneManager.LoadScene("MainScene");
                    break;


                //case "NodeIsMalButton":


                //    //maliciousNode.GetComponent<Renderer>().material.color = Color.red;
                //    //launchText.text = "CE NOEUD EST MALICIEUX : " + maliciousNode.name;

                //    //launchText.text = "DEFINIR UN NOEUD COMME MALICIEUX";

                //    try
                //    {
                //        var maliciousNode = GameObject.Find("Pointer").GetComponent<Pointer>().currentObject;
                //        //currentVRText.text = maliciousNode.name;
                //        maliciousNode.GetComponent<Renderer>().material.color = Color.red;
                //        maliciousNode.GetComponent<LineRenderer>().endColor = Color.red;
                //        maliciousNode.GetComponent<LineRenderer>().startColor = Color.red;
                //    }

                //    catch
                //    {
                //        //currentObjectText.text = "dommage";
                //    }

                //    break;


                // LABEL LATER
                //case "Label":
                //        m_Dropdown.Show();
                //    break;
                //case "Blocker":
                //        m_Dropdown.Hide();
                //    break;

                default:
                    break;
            }

            ProcessPress(m_Data);
        }

        //release 
        if (m_ClickAction.GetStateUp(m_TargetSource))
        {
            //actionText.text = "trigger up";
            ProcessPress(m_Data);
        }



    }

    public PointerEventData GetData()
    {
        
        return m_Data;
    }

    private void ProcessPress(PointerEventData data)
    {

    }


    private void ProcessRelease(PointerEventData data)
    {

    }





}
