using UnityEngine;
using UnityEngine.UI;

public static class CameraSetter
{

    public static void StreamWebcamTextureToRawImage(WebCamTexture webCamTextureInput, RawImage ramImageInput)
    {
        ramImageInput.texture = webCamTextureInput;

        var camTextureDimensions = new Vector2(webCamTextureInput.width, webCamTextureInput.height);

        ramImageInput.rectTransform.sizeDelta = camTextureDimensions;
    }
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

    public static void PlayWebcamTexture(WebCamTexture camTextureInput, ref int widthOutput, ref int heightOutput, ref Color32[] colorOutput)
    {
        if (camTextureInput != null)
        {
            camTextureInput.Play();
            widthOutput = camTextureInput.width;
            heightOutput = camTextureInput.height;
            colorOutput = new Color32[widthOutput * heightOutput];
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
