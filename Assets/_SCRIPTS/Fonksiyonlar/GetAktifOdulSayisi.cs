using UnityEngine;

public static class GetAktifOdulSayisi
{
    public static int Hangi(bool gorev)
    {
        if (!gorev) return 0;

        int count = 0;
        for (int i = 0; i < 6; i++)
        {
            int countIlkUc = 0;
            for (int j = 0; j < 4; j++)
            {
                if (j != 3)
                {
                    GOREV gorevMevcut = GOREV_YONETICISI.instance.GetGOREV(i, j);
                    if (gorevMevcut.IsGorevTamam()) countIlkUc++;
                    if (gorevMevcut.IsGorevTamam() && !gorevMevcut._odulAlindi) count++;
                }
                else
                {
                    if (countIlkUc == 3 && !KAYIT_GOREV_YONETICISI.GetGorevTamamlandi(i, j)) count++;
                }
            }
        }
        return count;
    }
}
