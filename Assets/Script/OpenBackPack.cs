using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBackPack : MonoBehaviour
{

    bool open;
    [SerializeField] GameObject Content;

    void Awake()
    {
        if(Content.activeInHierarchy == true)
        {
            Content.SetActive(false);
            open = false;
        }
        else
        {
            open = false;
        }
    }

    public void OnClickBackPack()
    {
        OpenOrClose(open);
    }

    public void OpenOrClose (bool _open)
    {
        if(_open == true)
        {
            Content.SetActive(false);
            _open = false;
        }else if(_open == false)
        {
            Content.SetActive(true);
            _open = true;
        }
        open = _open;
    }
}
