using UnityEngine;
using ZXing;
public class StandaloneEasyReaderSampleGeneric : MonoBehaviour
{
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

    protected string lastResult;
    protected Rect screenRect;

    protected WebCamTexture CamTexture;
    protected Color32[] cameraColorData;
    protected int width, height;
    protected Result Result;
}
public class StandaloneEasyReaderSample : StandaloneEasyReaderSampleGeneric
{
    [SerializeField] bool logAvailableWebcams;
    [SerializeField] int selectedWebcamIndex;
    public string Output => lastResult;

    private void Awake() {
        StandartStandaloneEasyReader_CameraSetter.LogWebcamDevices(logAvailableWebcams);
        StandartStandaloneEasyReader_CameraSetter.SetupWebcamTexture(ref CamTexture, selectedWebcamIndex);
        StandartStandaloneEasyReader_CameraSetter.PlayWebcamTexture(CamTexture, ref width, ref height, ref cameraColorData);

        lastResult = "Last Result";
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    private void OnEnable()
    {
        StandartStandaloneEasyReader_CameraSetter.CamTextureSetEnable(CamTexture, CameraTextureContoller.Start);
    }

    private void OnDisable() 
    {
        StandartStandaloneEasyReader_CameraSetter.CamTextureSetEnable(CamTexture, CameraTextureContoller.Pause);
    }

    private void Update() 
    {
        if (CamTexture != null)
        {
            if (CamTexture.isPlaying)
            {
                // decoding from camera image
                CamTexture.GetPixels32(cameraColorData); // -> performance heavy method 
                Result = BarcodeReader.Decode(cameraColorData, width, height); // -> performance heavy method
                if (Result != null)
                {
                    lastResult = Result.Text + " " + Result.BarcodeFormat;
                    print(lastResult);
                }
            }
        }
    }

    private void OnGUI() 
    {
        GUI.DrawTexture(screenRect, CamTexture, ScaleMode.ScaleToFit); // show camera image on screen
        GUI.TextField(new Rect(10, 10, 256, 25), lastResult); // show decoded text on screen
    }

    private void OnDestroy()
    {
        StandartStandaloneEasyReader_CameraSetter.CamTextureSetEnable(CamTexture, CameraTextureContoller.Stop);
    }
}
