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

    protected WebCamTexture camTexture;
    protected Color32[] cameraColorData;
    protected int width, height;
    protected Result result;
}
public class StandaloneEasyReaderSample : StandaloneEasyReaderSampleGeneric
{
    [SerializeField] bool logAvailableWebcams;
    [SerializeField] int selectedWebcamIndex;
    public string Output => lastResult;

    private void Awake() {
        CameraSetter.LogWebcamDevices(logAvailableWebcams);
        CameraSetter.SetupWebcamTexture(ref camTexture, selectedWebcamIndex);
        CameraSetter.PlayWebcamTexture(camTexture, ref width, ref height, ref cameraColorData);

        lastResult = "Last Result";
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    private void OnEnable()
    {
        CameraSetter.CamTextureSetEnable(camTexture, CameraTextureContoller.Start);
    }

    private void OnDisable() 
    {
        CameraSetter.CamTextureSetEnable(camTexture, CameraTextureContoller.Pause);
    }

    private void Update() 
    {
        if (camTexture != null)
        {
            if (camTexture.isPlaying)
            {
                // decoding from camera image
                camTexture.GetPixels32(cameraColorData); // -> performance heavy method 
                result = BarcodeReader.Decode(cameraColorData, width, height); // -> performance heavy method
                if (result != null)
                {
                    lastResult = result.Text + " " + result.BarcodeFormat;
                    print(lastResult);
                }
            }
        }
    }

    private void OnGUI() 
    {
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit); // show camera image on screen
        GUI.TextField(new Rect(10, 10, 256, 25), lastResult); // show decoded text on screen
    }

    private void OnDestroy()
    {
        CameraSetter.CamTextureSetEnable(camTexture, CameraTextureContoller.Stop);
    }
}
