using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    //public GameObject MessagePanel;
    //public Text text;
    
    void Start()
    {
        GetComponent<Text>().text = "Message...";
    }
}
