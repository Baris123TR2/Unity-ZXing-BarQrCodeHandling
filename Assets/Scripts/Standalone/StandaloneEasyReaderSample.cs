using UnityEngine;
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
    public string Output => CamDataSO.lastResult;

    private void Awake() {
        StandartStandaloneEasyReader_CameraSetter.LogWebcamDevices(logAvailableWebcams);
        StandartStandaloneEasyReader_CameraSetter.SetupWebcamTexture(ref CamDataSO.CamTexture, selectedWebcamIndex);
        StandartStandaloneEasyReader_CameraSetter.PlayWebcamTexture(CamDataSO.CamTexture, ref CamDataSO.width, ref CamDataSO.height, ref CamDataSO.cameraColorData);

        CamDataSO.lastResult = "Last Result";
        CamDataSO.screenRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    private void OnEnable()
    {
        StandartStandaloneEasyReader_CameraSetter.CamTextureSetEnable(CamDataSO.CamTexture, CamDataSO.CCOnEnable);
    }

    private void OnDisable() 
    {
        StandartStandaloneEasyReader_CameraSetter.CamTextureSetEnable(CamDataSO.CamTexture, CamDataSO.CCOnDisable);
    }

    private void Update() 
    {
        CamDataSO.UpdateCamData();
        /*if (CamDataSO.CamTexture != null)
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
        }*/
    }

    private void OnGUI() 
    {
        GUI.DrawTexture(CamDataSO.screenRect, CamDataSO.CamTexture, ScaleMode.ScaleToFit); // show camera image on screen
        GUI.TextField(new Rect(10, 10, 256, 25), CamDataSO.lastResult); // show decoded text on screen
    }

    private void OnDestroy()
    {
        StandartStandaloneEasyReader_CameraSetter.CamTextureSetEnable(CamDataSO.CamTexture, CamDataSO.CCOnDestroy);
    }
}
