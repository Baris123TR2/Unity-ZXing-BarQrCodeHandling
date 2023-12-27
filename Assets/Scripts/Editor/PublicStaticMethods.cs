using System.Diagnostics;
using System.IO;

public static class PublicStaticMethods
{
    public static void OpenFolder(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = folderPath;

            Process.Start(info);
        }
    }
}
