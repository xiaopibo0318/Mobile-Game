using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactive_icon : MonoBehaviour
{
    [SerializeField] GameObject icon;


    private void Awake()
    {
        icon.SetActive(false);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        icon.SetActive(true);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        icon.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
