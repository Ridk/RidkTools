using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class AssetBundleCreate : EditorWindow
{
    enum RidkBuildTarget
    {
        Windows = BuildTarget.StandaloneWindows,
        WebGL = BuildTarget.WebGL,
        iOS = BuildTarget.iOS,
        Android = BuildTarget.Android,
        macOS = BuildTarget.StandaloneOSX
    }

    private string abName;
    private bool isSingle;
    private bool isBatch = false;
    private bool isMSG = false;
    private RidkBuildTarget _buildTarget = RidkBuildTarget.WebGL;
    private BuildAssetBundleOptions _babOptions = BuildAssetBundleOptions.None;
    string message;
    MessageType msgType;

    [MenuItem("Window/AssetBundle")]
    public static void CreateWindow()
    {
        AssetBundleCreate window = (AssetBundleCreate) GetWindow(typeof(AssetBundleCreate), false, "Ridk");
        window.Show();
    }
  private void OnGUI()
    {
        EditorGUILayout.Space();
        abName =
            EditorGUILayout.TextField("资源包名字:", abName, GUILayout.MinWidth(100));
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        isSingle = EditorGUILayout.Toggle("只打包所选文件:", isSingle);
        if (isSingle)
        {
            isBatch = EditorGUILayout.Toggle("打包子目录(只限一级)", isBatch);
        }

        EditorGUILayout.EndHorizontal();
        var s = new GUIStyle
        {
            fontStyle = FontStyle.Bold,
            fontSize = 16,
            margin = new RectOffset(10, 10, 10, 10),
            alignment = TextAnchor.MiddleCenter
        };

        EditorGUILayout.TextField("选项", s);
        _babOptions = (BuildAssetBundleOptions) EditorGUILayout.EnumPopup("压缩方式", _babOptions, GUILayout.MinWidth(40));
        EditorGUILayout.Space();
        _buildTarget = (RidkBuildTarget) EditorGUILayout.EnumPopup("目标平台", _buildTarget, GUILayout.MinWidth(40));
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextField("", s);
        if (GUILayout.Button("一键打包", GUILayout.MinWidth(40), GUILayout.MaxWidth(100), GUILayout.MinHeight(50)))
        {
            if (isBatch)
            {
                if (Selection.objects.Length > 1)
                {
                    isMSG = true;
                    message = "请选择单个有效文件夹";
                    msgType = MessageType.Error;
                    return;
                }
            }

            isMSG = false;
            try
            {
                BuildAssetBundle((BuildTarget) _buildTarget, _babOptions);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                isMSG = true;
                message = "打包出现错误!!请查看控制台信息.";
                msgType = MessageType.Error;
                return;
            }

            isMSG = true;
            message = "一键打包完成!!文件保存在\"AssetsBundle\"文件夹下的相对应平台文件夹.";
            msgType = MessageType.Info;
        }

        EditorGUILayout.TextField("", s);
        EditorGUILayout.EndHorizontal();
        if (isMSG)
        {
            EditorGUILayout.HelpBox(message, msgType);
        }
        //TODO  是否实现点击按钮打开资源文件夹
        //System.Diagnostics.Process.Start("explorer.exe", v_OpenFolderPath);
        Repaint();
    }

    private void BuildAssetBundle(BuildTarget target, BuildAssetBundleOptions babo)
    {
        if (isSingle)
            BuildPipeline.BuildAssetBundles(CheckPath("Assets/AssetsBundle/" + target), GetAssetBundleBuilds(), babo , target);
        else
            BuildPipeline.BuildAssetBundles(CheckPath("Assets/AssetsBundle/" + target), babo, target);

        abName = "";
    }

    private string CheckPath(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        
        return path;
    }

    private AssetBundleBuild[] GetAssetBundleBuilds()
    {
        if (Selection.objects == null) return null;
        AssetBundleBuild[] abs = null;

        if (isBatch)
        {
            var folders = AssetDatabase.GetSubFolders(AssetDatabase.GetAssetPath(Selection.activeObject));
            abs = new AssetBundleBuild[folders.Length];
            for (int i = 0; i < folders.Length; i++)
            {
                abs[i] = AutoGetAssets(folders[i]);
            }
        }
        else
        {
            abs = new AssetBundleBuild[1];
            abs[0] = AutoGetAssets();
        }

        return abs;
    }

    private AssetBundleBuild AutoGetAssets()
    {
        var name = "";
        var paths = new string[Selection.objects.Length];
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = AssetDatabase.GetAssetPath(Selection.objects[i]);
        }

        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i].EndsWith(".prefab"))
            {
                var fileInfo = new FileInfo(paths[i]);
                name = fileInfo.Name.Replace(".prefab", "");
            }
        }

        var abs = new AssetBundleBuild
        {
            assetBundleName = (abName == "" ? (name == "" ? Selection.objects[0].name : name) : abName) + ".unity3d",
            assetNames = paths
        };

        return abs;
    }

    private AssetBundleBuild AutoGetAssets(string path)
    {
        var name = "";
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        var objects = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
        List<string> lpath = new List<string>();
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].Name.Contains("._") || objects[i].Extension == ".meta")
                continue;
            if (objects[i].FullName.EndsWith(".prefab"))
                name = objects[i].Name.Replace(objects[i].Extension, "");
            string strTempPath = objects[i].FullName.Replace(@"\", "/");
            strTempPath = strTempPath.Substring(strTempPath.IndexOf("Assets", StringComparison.Ordinal));
            lpath.Add(strTempPath);
        }

        string[] paths = new string[lpath.Count];
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = lpath[i];
            Debug.Log(paths[i]);
        }

        var abs = new AssetBundleBuild
        {
            assetBundleName = (abName == "" ? (name =="" ? directoryInfo.Name : name) : abName) + ".unity3d",
            assetNames = paths
        };
        return abs;
    }
}