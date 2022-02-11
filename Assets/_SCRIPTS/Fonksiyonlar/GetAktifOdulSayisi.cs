using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAktifOdulSayisi : MonoBehaviour
{

    public static int Hangi(bool gorev)
    {
        if (gorev)
        {

            int count = 0;
            for (int i = 0; i < 6; i++)
            {
                int countIlkUc = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (3 != j)
                    {
                        GOREV gorevMevcut = GOREV_YONETICISI.instance.GetGOREV(i, j);
                        if (gorevMevcut.IsGorevTamam()) countIlkUc++;
                        if (gorevMevcut.IsGorevTamam() && !gorevMevcut._odulAlindi) count++;
                    }
                    else
                    {

                        if (countIlkUc == 3)
                        {
                            Debug.Log("3");
                             if(! KAYIT_GOREV_YONETICISI.GetGorevTamamlandi(i, j))count++;
                        }

                    }
                }
            }
            return count;

        }
        else
        {
            return 0;
        }

    }

}
