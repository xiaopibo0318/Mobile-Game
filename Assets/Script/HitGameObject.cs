using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class HitGameObject : MonoBehaviour
{
    //InputAction _click;


    
    private void Update()
    {
        ////判斷是否按下左鍵
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("以點下去");
        //    //發射線
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
     
        //    Debug.Log(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider.gameObject.name == "nearicon")
        //        {
        //            Debug.Log("以觸發");
        //        }
        //        Debug.Log(hit.transform.name);
        //        if (hit.transform.name == "nearicon")
        //        {
        //            Debug.Log("以點下去ICON");
        //        }
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Debug.Log("以按下A鍵");
        //}
    }


    private void FixedUpdate()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("以典籍物品");
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //   Debug.Log("以典籍物品");
        //    }     
        //}
        //_click = GetComponent<PlayerInput>().currentActionMap["OnClick"];
        //Debug.Log(_click);
        
    }


    

	
}