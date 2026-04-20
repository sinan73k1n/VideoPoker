public static class PokerHandEvaluator
{
    public static int Evaluate(KartVideoPoker[] karts)
    {
        if (RoyalFlush(karts))    return 250;
        if (StraightFlush(karts)) return 50;
        if (FourOfAKind(karts))   return 25;
        if (FullHouse(karts))     return 9;
        if (Flush(karts))         return 6;
        if (Straight(karts))      return 4;
        if (ThreeOfKind(karts))   return 3;
        if (TwoPair(karts))       return 2;
        if (JacksOrBetter(karts)) return 1;
        return 0;
    }

    public static string GetEl(KartVideoPoker[] karts)
    {
        string el = "";
        foreach (var k in karts) el += GetKart(k);
        return el;
    }

    static string GetKart(KartVideoPoker k)
    {
        string tur;
        switch (k._kartTur)
        {
            case KartTur.Kupa:  tur = "KU"; break;
            case KartTur.Maca:  tur = "MA"; break;
            case KartTur.Karo:  tur = "KA"; break;
            default:            tur = "SI"; break;
        }
        return tur + (k._index < 10 ? "0" + k._index : k._index.ToString());
    }

    // Bug fix: orijinal kodda tüm kontroller isRoyalFlush0'a yazılıyordu,
    // bu yüzden Royal Flush hiç tetiklenmiyordu.
    static bool RoyalFlush(KartVideoPoker[] karts)
    {
        if (!Flush(karts)) return false;
        bool has10 = false, hasJ = false, hasQ = false, hasK = false, hasA = false;
        foreach (var k in karts)
        {
            if (k._index == 10) has10 = true;
            if (k._index == 11) hasJ  = true;
            if (k._index == 12) hasQ  = true;
            if (k._index == 13) hasK  = true;
            if (k._index == 1)  hasA  = true;
        }
        return has10 && hasJ && hasQ && hasK && hasA;
    }

    static bool StraightFlush(KartVideoPoker[] karts)
    {
        if (!Flush(karts)) return false;
        int min = 14;
        foreach (var k in karts)
            if (k._index < min) min = k._index;

        bool k2 = false, k3 = false, k4 = false, k5 = false;
        foreach (var k in karts)
        {
            if (min + 1 == k._index) k2 = true;
            if (min + 2 == k._index) k3 = true;
            if (min + 3 == k._index) k4 = true;
            if (min + 4 == k._index) k5 = true;
        }
        return k2 && k3 && k4 && k5;
    }

    static bool FourOfAKind(KartVideoPoker[] karts)
    {
        int sayi = 1;
        for (int i = 1; i < 5; i++)
            if (karts[0]._index == karts[i]._index) sayi++;
        if (sayi == 4) return true;

        sayi = 1;
        for (int i = 2; i < 5; i++)
            if (karts[1]._index == karts[i]._index) sayi++;
        return sayi == 4;
    }

    static bool FullHouse(KartVideoPoker[] karts)
    {
        int indexOf3 = -1;
        for (int i = 0; i < 3; i++)
        {
            int sayi = 1;
            for (int j = i + 1; j < 5; j++)
                if (karts[i]._index == karts[j]._index) sayi++;
            if (sayi == 3) { indexOf3 = karts[i]._index; break; }
        }
        if (indexOf3 < 0) return false;

        for (int i = 0; i < 5; i++)
        {
            if (karts[i]._index == indexOf3) continue;
            int sayi = 1;
            for (int j = i + 1; j < 5; j++)
            {
                if (karts[j]._index == indexOf3) continue;
                if (karts[i]._index == karts[j]._index) sayi++;
            }
            if (sayi == 2) return true;
        }
        return false;
    }

    static bool Flush(KartVideoPoker[] karts)
    {
        for (int i = 1; i < 5; i++)
            if (karts[0]._kartTur != karts[i]._kartTur) return false;
        return true;
    }

    static bool Straight(KartVideoPoker[] karts)
    {
        bool haveAce = false;
        foreach (var k in karts)
            if (k._index == 1) { haveAce = true; break; }

        if (haveAce)
        {
            bool k2 = false, k3 = false, k4 = false, k5 = false;
            foreach (var k in karts)
            {
                if (k._index == 2)  k2 = true;
                if (k._index == 3)  k3 = true;
                if (k._index == 4)  k4 = true;
                if (k._index == 5)  k5 = true;
            }
            if (k2 && k3 && k4 && k5) return true;

            k2 = k3 = k4 = k5 = false;
            foreach (var k in karts)
            {
                if (k._index == 13) k2 = true;
                if (k._index == 12) k3 = true;
                if (k._index == 11) k4 = true;
                if (k._index == 10) k5 = true;
            }
            return k2 && k3 && k4 && k5;
        }
        else
        {
            int min = 14;
            foreach (var k in karts)
                if (k._index < min) min = k._index;

            bool k2 = false, k3 = false, k4 = false, k5 = false;
            foreach (var k in karts)
            {
                if (min + 1 == k._index) k2 = true;
                if (min + 2 == k._index) k3 = true;
                if (min + 3 == k._index) k4 = true;
                if (min + 4 == k._index) k5 = true;
            }
            return k2 && k3 && k4 && k5;
        }
    }

    static bool ThreeOfKind(KartVideoPoker[] karts)
    {
        for (int i = 0; i < 3; i++)
        {
            int sayi = 1;
            for (int j = i + 1; j < 5; j++)
                if (karts[i]._index == karts[j]._index) sayi++;
            if (sayi == 3) return true;
        }
        return false;
    }

    static bool TwoPair(KartVideoPoker[] karts)
    {
        bool firstPair = false;
        for (int i = 0; i < 5; i++)
        {
            for (int j = i + 1; j < 5; j++)
            {
                if (karts[i]._index == karts[j]._index)
                {
                    if (!firstPair) { firstPair = true; break; }
                    return true;
                }
            }
        }
        return false;
    }

    static bool JacksOrBetter(KartVideoPoker[] karts)
    {
        int[] highCards = { 1, 11, 12, 13 };
        foreach (int val in highCards)
        {
            int count = 0;
            foreach (var k in karts)
                if (k._index == val) count++;
            if (count == 2) return true;
        }
        return false;
    }
}
