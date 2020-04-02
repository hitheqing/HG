using System.IO;
using UnityEditor;
using UnityEngine;

namespace HG
{
    public class PathUtil
    {
        public static string PathCombine(params string[] parts)
        {
            var path = parts[0];
            for (var i = 1; i < parts.Length; ++i)
                path = Path.Combine(path, parts[i]);
            return path;
        }
        
        public static string GetUnityFolder()
        {
            var editorAppPath = EditorApplication.applicationPath;
            if (Application.platform == RuntimePlatform.WindowsEditor)
                // ReSharper disable once AssignNullToNotNullAttribute
                return Path.Combine(Path.GetDirectoryName(editorAppPath), "Data");
            else if (Application.platform == RuntimePlatform.OSXEditor)
            {
#if UNITY_5_4_OR_NEWER
                return Path.Combine(editorAppPath, "Contents");
#else
                return Path.Combine(editorAppPath, Path.Combine("Contents", "Frameworks"));
#endif
            }
            else // Linux...?
                // ReSharper disable once AssignNullToNotNullAttribute
                return Path.Combine(Path.GetDirectoryName(editorAppPath), "Data");
        }
    }
}