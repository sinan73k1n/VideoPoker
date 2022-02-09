using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DEBUG_GAME : MonoBehaviour
{
    public static DEBUG_GAME instance;
    [SerializeField] Button _btnReload;
    [SerializeField] Text _txtDebu1, _txtDebu2, _txtDebu3;
    void Awake()
    {
        instance = this;
        _btnReload.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

   
    public void Yazdir(string text,int hangisine)
    {
        switch (hangisine)
        {
            
            case 2:_txtDebu2.text = text; break;
            case 3: _txtDebu3.text = _txtDebu3.text +text; break;
            case 1: 
            default:
                _txtDebu1.text = text;
                break;
        }
    }
}
