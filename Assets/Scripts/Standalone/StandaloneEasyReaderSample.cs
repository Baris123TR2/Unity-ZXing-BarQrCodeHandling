using UnityEngine;
using UnityEngine.UI;
using ZXing;
public class StandaloneEasyReaderSampleGeneric : MonoBehaviour
{
    [SerializeField] protected CamDataSO CamDataSO;
    IBarcodeReader barcodeReader;
    protected IBarcodeReader BarcodeReader
    {
        get
        {
            if (barcodeReader == null)
            {
                barcodeReader = new BarcodeReader
                {
                    AutoRotate = false,
                    Options = new ZXing.Common.DecodingOptions
                    {
                        TryHarder = false
                    }
                };
            }
            return barcodeReader;
        }
    }

    [SerializeField] RawImage webCamOutputImage;
    protected RawImage WebCamOutputImage
    {
        get
        {
            return webCamOutputImage;
        }
    }
}
public class StandaloneEasyReaderSample : StandaloneEasyReaderSampleGeneric
{
    [SerializeField] bool logAvailableWebcams;
    [SerializeField] int selectedWebcamIndex;
    public string Output => CamDataSO.ResultOutput;

    private void OnEnable()
    {
        if (CamDataSO.CamTexture == null)
        {
            CameraSetter.SetupWebcamTexture(ref CamDataSO.CamTexture, selectedWebcamIndex);
        }

        if (WebCamOutputImage.color.a == 0)
        {
            WebCamOutputImage.color = new Color(WebCamOutputImage.color.r, WebCamOutputImage.color.g, WebCamOutputImage.color.b, 1);
        }
        CamDataSO.ResultOutput = "Last Result";
        CamDataSO.screenRect = new Rect(0, 0, Screen.width, Screen.height);

        CameraSetter.CamTextureSetEnable(CamDataSO.CamTexture, CamDataSO.CCOnEnable, selectedWebcamIndex);
    }

    private void OnDisable() 
    {
        CameraSetter.CamTextureSetEnable(CamDataSO.CamTexture, CamDataSO.CCOnDisable, selectedWebcamIndex);
    }

    private void Update() 
    {
        CameraSetter.StreamWebcamTextureToRawImage(ref CamDataSO.CamTexture, WebCamOutputImage, selectedWebcamIndex);
    }

    private void OnDestroy()
    {
        CameraSetter.CamTextureSetEnable(CamDataSO.CamTexture, CamDataSO.CCOnDestroy, selectedWebcamIndex);
    }
}
