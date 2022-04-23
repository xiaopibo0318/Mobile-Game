using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image cache;
    [SerializeField] Image cache_background;
    [SerializeField] GameObject Book_2;

    public static UIManager Instance;
    bool book_open;
    private void Awake()
    {
        Instance = this;
        cache.enabled = false;
        cache_background.enabled = false;

        if (Book_2.activeInHierarchy == true)
        {
            Book_2.SetActive(false);
        }

        book_open = false;

    }
    
    public void CacheVisible(bool visible)
    {
        
        if(visible == true)
        {
            cache.enabled = true;
            cache_background.enabled = true;

        }else if(visible == false)
        {
            cache.enabled = false;
            cache_background.enabled = false;
        }
    }

    public void OnClickBook()
    {
        OpenOrClose(book_open);
    }

    public void OpenOrClose(bool _open)
    {
        if (_open == true)
        {
            Book_2.SetActive(false);
            _open = false;
        }
        else if (_open == false)
        {
            Debug.Log("開起書來");
            Book_2.SetActive(true);
            _open = true;
        }
        book_open = _open;
    }

}
