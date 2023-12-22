using TMPro;
using UnityEngine;
using ZXing;

public static class StandartStandaloneEasyReader_CameraSetter
{
    public static void CamTextureSetEnable(WebCamTexture camTextureInput, CameraTextureContoller controllerState)
    {
        if (camTextureInput == null) return;

        switch (controllerState)
        {
            case CameraTextureContoller.Start:
                if (!camTextureInput.isPlaying)
                {
                    camTextureInput.Play();
                }
                break;
            case CameraTextureContoller.Pause:
                if (camTextureInput.isPlaying)
                {
                    camTextureInput.Pause();
                }
                break;
            case CameraTextureContoller.Stop:
                camTextureInput.Stop();
                break;
            default:
                break;
        }
    }

    public static void SetupWebcamTexture(ref WebCamTexture camTextureOutput, int selectedWebcamIndexInput)
    {
        WebCamDevice[] webCamDevices = WebCamTexture.devices;
        WebCamTexture newCameraTexture;
        if (selectedWebcamIndexInput > webCamDevices.Length)
        {
            newCameraTexture = new(webCamDevices[webCamDevices.Length].name);
            Debug.Log($"Selected webcam index \"{selectedWebcamIndexInput}\" does not exist. Selected the least indexed camera: \"{webCamDevices.Length}\"");
        }
        else
        {
            newCameraTexture = new(webCamDevices[selectedWebcamIndexInput].name);
        }

        camTextureOutput = newCameraTexture;
        camTextureOutput.requestedHeight = Screen.height;
        camTextureOutput.requestedWidth = Screen.width;
    }

    public static Color32[] PlayWebcamTexture(WebCamTexture webCamTexture)
    {
        if (webCamTexture != null)
        {
            webCamTexture.Play();
            return new Color32[webCamTexture.width * webCamTexture.height];
        }
        else { return null; }
    }
    public static string ResultOutput(CameraData camDataInput, IBarcodeReader BarcodeReader)
    {
        var result = BarcodeReader.Decode(camDataInput.CamColors, camDataInput.Width, camDataInput.Height); // -> performance heavy method

        if (result != null)
        {
            return result.Text + " " + result.BarcodeFormat;
        }
        else
        {
            return "Result is null";
        }
    }

    public static void LogWebcamDevices(bool isEnabled)
    {
        if (isEnabled)
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            for (int i = 0; i < devices.Length; i++)
            {
                Debug.Log(devices[i].name);
            }
        }
    }
}
