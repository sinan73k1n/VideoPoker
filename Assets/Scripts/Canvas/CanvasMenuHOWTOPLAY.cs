using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CanvasMenuHOWTOPLAY : MonoBehaviour
{
    [SerializeField] Button _btnClose;
    private void Awake()
    {
        GetComponent<Canvas>().sortingOrder = 20;
    }

    void Start()
    {
        _btnClose.onClick.AddListener(() => HandleExit());
    }

    void HandleExit()
    {
        SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Destroy(gameObject);
    }
}