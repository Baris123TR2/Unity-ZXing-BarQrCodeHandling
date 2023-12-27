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
}
public class StandaloneEasyReaderSample : StandaloneEasyReaderSampleGeneric
{
    [SerializeField] bool logAvailableWebcams;
    [SerializeField] int selectedWebcamIndex;

    [SerializeField] RawImage WebCamOutputImage;
    public string Output => CamDataSO.lastResult;

    private void Awake() {
        CameraSetter.LogWebcamDevices(logAvailableWebcams);
        CameraSetter.SetupWebcamTexture(ref CamDataSO.CamTexture, selectedWebcamIndex);
        CameraSetter.PlayWebcamTexture(CamDataSO.CamTexture, ref CamDataSO.width, ref CamDataSO.height, ref CamDataSO.cameraColorData);

        if (WebCamOutputImage.color.a == 0) WebCamOutputImage.color = new Color(WebCamOutputImage.color.r, WebCamOutputImage.color.g, WebCamOutputImage.color.b, 1);

        CamDataSO.lastResult = "Last Result";
        CamDataSO.screenRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    private void OnEnable()
    {
        CameraSetter.CamTextureSetEnable(CamDataSO.CamTexture, CamDataSO.CCOnEnable);
    }

    private void OnDisable() 
    {
        CameraSetter.CamTextureSetEnable(CamDataSO.CamTexture, CamDataSO.CCOnDisable);
    }

    private void Update() 
    {
        CamDataSO.UpdateCamData();

        CameraSetter.StreamWebcamTextureToRawImage(CamDataSO.CamTexture, WebCamOutputImage);
    }

    /*void OldUpdate()
    {
        if (CamDataSO.CamTexture != null)
        {
            if (CamDataSO.CamTexture.isPlaying)
            {
                // decoding from camera image
                CamDataSO.CamTexture.GetPixels32(CamDataSO.cameraColorData); // -> performance heavy method 
                CamDataSO.Result = BarcodeReader.Decode(CamDataSO.cameraColorData, CamDataSO.width, CamDataSO.height); // -> performance heavy method
                if (CamDataSO.Result != null)
                {
                    CamDataSO.lastResult = CamDataSO.Result.Text + " " + CamDataSO.Result.BarcodeFormat;
                    print(CamDataSO.lastResult);
                }
            }
        }
    }*/

    /*private void OnGUI()
    {
        GUI.DrawTexture(CamDataSO.screenRect, CamDataSO.CamTexture, ScaleMode.ScaleToFit); // show camera image on screen
        GUI.TextField(new Rect(10, 10, 256, 25), CamDataSO.lastResult); // show decoded text on screen
    }*/

    private void OnDestroy()
    {
        CameraSetter.CamTextureSetEnable(CamDataSO.CamTexture, CamDataSO.CCOnDestroy);
    }
}
