using System.IO;
using UnityEditor;
using UnityEngine;

#pragma warning disable 0649
namespace Editor
{
    public static class UnityPackageUtil
    {
        [MenuItem("Tools/Package Signals")]
        public static void CreateUnityPackage()
        {
            var outPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../NepixSignals.unitypackage"));
            AssetDatabase.ExportPackage("Assets/Plugins/NepixSignals", outPath, ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
            EditorUtility.RevealInFinder(outPath);
            Debug.Log("Exported to " + outPath);
        }
    }
}