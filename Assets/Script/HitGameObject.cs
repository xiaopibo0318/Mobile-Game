using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class HitGameObject : MonoBehaviour
{
	InputAction _click;

    private void FixedUpdate()
    {
        _click = GetComponent<PlayerInput>().currentActionMap["OnClick"];
        Debug.Log(_click);
    }

    

	
}