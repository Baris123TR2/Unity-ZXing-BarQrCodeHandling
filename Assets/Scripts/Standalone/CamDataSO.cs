using TMPro;
using UnityEngine;
using ZXing;

[CreateAssetMenu(menuName = "AppKit/Cam Data", fileName = "Cam Data File")]
public class CamDataSO : ScriptableObject
{
    public CameraTextureContoller CCOnEnable = CameraTextureContoller.Start;
    public CameraTextureContoller CCOnDisable = CameraTextureContoller.Pause;
    public CameraTextureContoller CCOnDestroy = CameraTextureContoller.Stop;

    [HideInInspector] public string ResultOutput;
    [HideInInspector] public Rect screenRect;

    [HideInInspector] public WebCamTexture CamTexture;
    [HideInInspector] public Color32[] cameraColorData;
    [HideInInspector] public int width, height;
    Result Result;

    IBarcodeReader barcodeReader;
    [HideInInspector] public IBarcodeReader BarcodeReader
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

    public void UpdateCamData()
    {
        if (CamTexture != null)
        {
            if (CamTexture.isPlaying)
            {
                // decoding from camera image
                CamTexture.GetPixels32(cameraColorData); // -> performance heavy method 
                
                if (Result != null)
                {
                    ResultOutput = $"{Result.Text} {Result.BarcodeFormat}";
                    Debug.Log(ResultOutput);
                }
            }
        }
        else
        {
            Debug.Log($"Cam texture is null");
        }
    }
    public void Decode(TextMeshProUGUI lastResultOutput)
    {
        Result = BarcodeReader.Decode(cameraColorData, width, height); // -> performance heavy method

        if (Result != null)
        {
            if (lastResultOutput != null)
            {
                lastResultOutput.text = Result.Text;
            }
            else
            {
                Debug.Log($"Last result indicator text has not been referenced");
            }
        }
        else
        {
            Debug.Log($"No result captured");
        }
    }
}
