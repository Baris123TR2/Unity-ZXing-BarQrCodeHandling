using UnityEngine;
using ZXing;
public struct CameraData
{
    public WebCamTexture CamTexture;
    public Color32[] CamColors => new Color32[CamTexture.width * CamTexture.height];
    public int Width => CamTexture.width;
    public int Height => CamTexture.height;
}
public class StandaloneEasyReaderSample : MonoBehaviour
{
    [SerializeField] CameraTextureUpdaterSO CameraTextureUpdaterSO;
    WebCamTexture CamTexture { get => CameraTextureUpdaterSO.CamTexture; set => CameraTextureUpdaterSO.CamTexture = value; }

    private void Awake() {
        StandartStandaloneEasyReader_CameraSetter.LogWebcamDevices(CameraTextureUpdaterSO.logAvailableWebcams);
        StandartStandaloneEasyReader_CameraSetter.SetupWebcamTexture(ref CameraTextureUpdaterSO.CamTexture, CameraTextureUpdaterSO.selectedWebcamIndex);

        CameraTextureUpdaterSO.lastResult = "Last Result";
        CameraTextureUpdaterSO.screenRect = new Rect(0, 0, Screen.width, Screen.height);
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
        CameraTextureUpdaterSO.UpdateCameraTexture();
    }


    /*private void OnGUI() 
    {
        GUI.DrawTexture(screenRect, CamTexture, ScaleMode.ScaleToFit); // show camera image on screen
        GUI.TextField(new Rect(10, 10, 256, 25), lastResult); // show decoded text on screen
    }*/

    private void OnDestroy()
    {
        StandartStandaloneEasyReader_CameraSetter.CamTextureSetEnable(CamTexture, CameraTextureContoller.Stop);
    }
}
