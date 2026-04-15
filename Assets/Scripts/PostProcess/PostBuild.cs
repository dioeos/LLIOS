#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using UnityEngine;

public class PostBuild
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            string plistPath = path + "/Info.plist";

            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            PlistElementDict rootDict = plist.root;

            rootDict.SetBoolean("UIFileSharingEnabled", true);
            rootDict.SetBoolean("LSSupportsOpeningDocumentsInPlace", true);

            File.WriteAllText(plistPath, plist.WriteToString());
            Debug.Log("Successfully updated Info.plist to enable UIFileSharingEnabled and LSSupportsOpeningDocumentsInPlace");
        }
    }
}
#endif
