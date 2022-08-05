using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeverLoses : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
