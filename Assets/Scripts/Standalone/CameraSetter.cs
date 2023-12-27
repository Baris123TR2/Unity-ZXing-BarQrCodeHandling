using UnityEngine;
using UnityEngine.UI;
public static class CameraSetter
{
    public static void StreamWebcamTextureToRawImage(ref WebCamTexture webCamTextureInput, RawImage ramImageInput, int selectedCameraIndex)
    {
        ramImageInput.texture = webCamTextureInput;

        var camTextureDimensions = new Vector2(webCamTextureInput.width, webCamTextureInput.height);

        ramImageInput.rectTransform.sizeDelta = camTextureDimensions;
    }
    public static void CamTextureSetEnable(WebCamTexture camTextureInput, CameraTextureContoller controllerState, int selectedCameraIndex)
    {
        if (camTextureInput != null)
        {
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
        else
        {
            Debug.Log($"Cam texture is null");
        }
    }

    public static void SetupWebcamTexture(ref WebCamTexture camTextureOutput, int selectedWebcamIndexInput)
    {
        if (WebCamTexture.devices != null)
        {
            if (WebCamTexture.devices.Length > 0)
            {
                WebCamDevice[] allCamDevicesAvailable = WebCamTexture.devices;

                Debug.Log($"All cam devices list: {allCamDevicesAvailable}");

                for (int i = 0; i < allCamDevicesAvailable.Length; i++)
                {
                    Debug.Log($"Available camera index {i + 1}: {allCamDevicesAvailable[i].name}");
                }

                WebCamDevice selectedCamera;
                if (selectedWebcamIndexInput > allCamDevicesAvailable.Length)
                {
                    selectedCamera = allCamDevicesAvailable[allCamDevicesAvailable.Length];
                    Debug.Log($"Selected webcam index \"{selectedWebcamIndexInput}\" does not exist. Selected the least indexed camera: \"{allCamDevicesAvailable.Length}\"");
                }
                else
                {
                    selectedCamera = allCamDevicesAvailable[selectedWebcamIndexInput];
                }

                Debug.Log($"Selected camera device is: {selectedCamera.name}");

                WebCamTexture newCameraTexture = new(selectedCamera.name);

                camTextureOutput = newCameraTexture;
                camTextureOutput.requestedHeight = Screen.height;
                camTextureOutput.requestedWidth = Screen.width;
            }
            else
            {
                Debug.Log($"Webcam devices count is: {WebCamTexture.devices.Length}");
            }
        }
        else
        {
            Debug.Log("There is not any existing webcam device");
        }
    }
}
