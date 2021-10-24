using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

    public class BuildScript : MonoBehaviour
    {
        [MenuItem("BuildTools/Init Build Data")]
        public static void InitGame()
        {
            PlayerSettings.companyName = "Name";
        }
        
        [MenuItem("BuildTools/Build/Android")]
        public static void Build_Android()
        {
            string currentVersion = PlayerSettings.bundleVersion;

            Debug.Log(currentVersion);
            int major = Convert.ToInt32(currentVersion.Split('.')[0]);
            int minor = Convert.ToInt32(currentVersion.Split('.')[1]) + 1;
            PlayerSettings.bundleVersion = major + "." + minor;
            PlayerSettings.Android.bundleVersionCode = minor;

            string path2 = "./Builds";
            Debug.Log(path2);
            string[] levels = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => scene.path)
                .ToArray();

            BuildPipeline.BuildPlayer(levels, path2 + "/Build_" + PlayerSettings.bundleVersion + ".apk",
                BuildTarget.Android, BuildOptions.None);
        }
    }
