using UnityEngine;
using UnityEngine.UI;

// Kendi penceresini açan buton. Menüye eklenen yeni özellikler (başarılar, sıralama) bu betikle girer:
// CanvasMENU'ya alan eklenmez, buton silinirse özellik yalnız görünmez olur.
[RequireComponent(typeof(Button))]
public class UI_PENCERE_AC : MonoBehaviour
{
    [SerializeField] GameObject _pencere;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleAc);
    }

    void HandleAc()
    {
        if (_pencere == null) return;
        if (SesKutusu.instance != null) SesKutusu.instance.Play(NameOfAudioClip.VideoPokerTusaBas);
        Instantiate(_pencere);
    }
}
