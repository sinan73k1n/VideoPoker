using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

public class NewIAP : MonoBehaviour
{
    string id_Ads = "com.s7software.video.poker.removeads";
    string id_Credits_150 = "com.s7software.video.poker.150credidfor0.99";
    string id_Credits_350 = "com.s7software.video.poker.350credidfor1.99";
    string id_Credits_750 = "com.s7software.video.poker.750credidfor3.99";
    string id_Credits_2000 = "com.s7software.video.poker.2000credidfor9.99";
    async void Start()
    {
        try
        {
            var options = new InitializationOptions().
                SetEnvironmentName(id_Ads);
            var _id_Credits_150 = new InitializationOptions().
                SetEnvironmentName(id_Credits_150);
            var _id_Credits_350 = new InitializationOptions().
                SetEnvironmentName(id_Credits_350);
            var _id_Credits_750 = new InitializationOptions().
                SetEnvironmentName(id_Credits_750);
            var _id_Credits_2000 = new InitializationOptions().
                SetEnvironmentName(id_Credits_2000);




            await UnityServices.InitializeAsync(options);
            await UnityServices.InitializeAsync(_id_Credits_350);
            await UnityServices.InitializeAsync(_id_Credits_150);
            await UnityServices.InitializeAsync(_id_Credits_750);
            await UnityServices.InitializeAsync(_id_Credits_2000);


        }
        catch (System.Exception )
        {
            Debug.Log("hata");
            //Debug.Log(ex.Message);
        }
    }
}
