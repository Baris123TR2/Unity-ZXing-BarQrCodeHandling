using TMPro;
using UnityEngine;
using ZXing;

[CreateAssetMenu(menuName = "App/QR Applications/Camera Texture Updater", fileName = "Camera Texture Updater")]
public class CameraTextureUpdaterSO : ScriptableObject
{
    CameraData CamData => new CameraData() { CamTexture = CamTexture};
    
    public string lastResult = "Last Result";
    public Rect screenRect;

    public WebCamTexture CamTexture;
    public int Width, Height;
    public Result Result;

    public string Output => lastResult;

    public bool logAvailableWebcams;
    public bool CamTextureInputEnabled = true;
    public int selectedWebcamIndex;
    IBarcodeReader barcodeReader;
    IBarcodeReader BarcodeReader
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
    public void UpdateCameraTexture()
    {
        if (CamTexture != null)
        {
            if (CamTexture.isPlaying)
            {
                // decoding from camera image
                if (CamTextureInputEnabled)
                {
                    CamTexture.GetPixels32(StandartStandaloneEasyReader_CameraSetter.PlayWebcamTexture(CamTexture)); // -> performance heavy method 
                }
            }
        }
    }
    public void GetResult(TextMeshProUGUI outputText)
    {
        var result = StandartStandaloneEasyReader_CameraSetter.ResultOutput(CamData, BarcodeReader);
        Debug.Log(result);

        if (outputText != null)
        {
            outputText.text = result;
        }
    }
}
