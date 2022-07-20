using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject Book_2;

    public static UIManager Instance;
    bool book_open;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(gameObject);
        }

        if (Book_2.activeInHierarchy == true)
        {
            Book_2.SetActive(false);
        }

        book_open = false;
       

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
