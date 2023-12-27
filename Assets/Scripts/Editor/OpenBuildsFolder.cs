using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class OpenBuildsFolder : EditorWindow
{
    [MenuItem("AppKit/Open Builds Folder")]
    public static void OpenDirectory()
    {
        PublicStaticMethods.OpenFolder(Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Build"));
    }
    
}
