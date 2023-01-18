using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;


public class IAPManager : MonoBehaviour
{
    string id_Ads = "com.s7software.video.poker.removeads";
    string id_Credits_150 = "com.s7software.video.poker.150credidfor0.99";
    string id_Credits_350 = "com.s7software.video.poker.350credidfor1.99";
    string id_Credits_750 = "com.s7software.video.poker.750credidfor3.99";
    string id_Credits_2000 = "com.s7software.video.poker.2000credidfor9.99";


    //async void Start()
    //{
    //    try
    //    {
    //        var options = new InitializationOptions().
    //            SetEnvironmentName(id_Ads);
    //        var _id_Credits_150 = new InitializationOptions().
    //            SetEnvironmentName(id_Credits_150);
    //        var _id_Credits_350 = new InitializationOptions().
    //            SetEnvironmentName(id_Credits_350);
    //        var _id_Credits_750 = new InitializationOptions().
    //            SetEnvironmentName(id_Credits_750);
    //        var _id_Credits_2000 = new InitializationOptions().
    //            SetEnvironmentName(id_Credits_2000);


    //        await UnityServices.InitializeAsync(options);
    //        await UnityServices.InitializeAsync(_id_Credits_350);
    //        await UnityServices.InitializeAsync(_id_Credits_150);
    //        await UnityServices.InitializeAsync(_id_Credits_750);
    //        await UnityServices.InitializeAsync(_id_Credits_2000);
    //    }
    //    catch (System.Exception ex)
    //    {
    //        Debug.Log(ex.ToString());
    //    }
    //}
   public void HandleReklam()
    {
        FindObjectOfType<CanvasMENU>().SetActiveAdsButton(false);
    }
    public void OnPurchaseComplete(Product product)
    {
        if (id_Ads == product.definition.id)
        {
            AdControl.instance.CloseBanner();
            KAYIT.SetReklamVar(false);

            Invoke(nameof(HandleReklam), 0.4f);
            
           // Debug.Log("Reklam satın alma basarili");

        }
        else if (id_Credits_150 == product.definition.id)
        {
            GameManagerVideoPoker.instance.AddCredits(150);
           // Debug.Log("150 kredi satın alındı");

        }
        else if (id_Credits_350 == product.definition.id)
        {
            GameManagerVideoPoker.instance.AddCredits(350);

            //Debug.Log("350 kredi satın alındı");

        }
        else if (id_Credits_750 == product.definition.id)
        {
            GameManagerVideoPoker.instance.AddCredits(750);
           // Debug.Log("750 kredi satın alındı");

        }
        else if (id_Credits_2000 == product.definition.id)
        {
            GameManagerVideoPoker.instance.AddCredits(2000);
           // Debug.Log("2000 kredi satın alındı");
        }
        else 
        {
            Debug.Log("Bilinmeyen ürün satın alındı");
        }
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason p)
    {
        if (id_Ads == product.definition.id)
        {
            Debug.Log("reklam satın alma başarısız : "+ p);
       

        }
        else if (id_Credits_150 == product.definition.id)
        {
            Debug.Log("150 kredi satın alma başarısız: "+p);

        }
        else if (id_Credits_350 == product.definition.id)
        {
            Debug.Log("350 kredi satın alma başarısız: " + p);

        }
        else if (id_Credits_750 == product.definition.id)
        {
            Debug.Log("750 kredi satın alma başarısız: " + p);

        }
        else if (id_Credits_2000 == product.definition.id)
        {
            Debug.Log("2000 kredi satın alma başarısız: " + p);
        }
        else
        {
            Debug.Log("Bilinmeyen ürün satın alma başarısız: " + p);
        }

    }
}
