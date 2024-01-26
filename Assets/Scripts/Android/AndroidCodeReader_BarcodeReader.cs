using UnityEngine;
using ZXing;

public class AndroidCodeReader_BarcodeReader : MonoBehaviour
{
    IBarcodeReader barcodeReader;
    protected IBarcodeReader BarcodeReader
    {
        get
        {
            barcodeReader ??= new BarcodeReader
            {
                AutoRotate = false,
                Options = new ZXing.Common.DecodingOptions
                {
                    TryHarder = false
                }
            };
            return barcodeReader;
        }
    }
}
