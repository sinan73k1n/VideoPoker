using System;
public class GOREV
{
    public string _ad;
    public int _tamamlanan;
    public int _tamamlanmasiGereken;
    public int _odul;
    public bool _odulAlindi;
    public bool IsGorevTamam() { return _tamamlanan >= _tamamlanmasiGereken; }

}
