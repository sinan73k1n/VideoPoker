using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CanvasMenuHOWTOPLAY : MonoBehaviour
{
    [SerializeField] Button _btnClose;


    void Start()
    {
        _btnClose.onClick.AddListener(() => HandleExit());
    }

    void HandleExit()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        gameObject.SetActive(false);
    }
}