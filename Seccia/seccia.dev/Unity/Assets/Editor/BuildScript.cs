using UnityEditor;
using System;

public class BuildScript
{
	static string[] m_scenes = { "Assets/scene.unity" };
	
	static void Build()
	{
        AssetDatabase.ImportAsset("Assets/Behaviors/CameraBehavior.cs", ImportAssetOptions.ForceUpdate);
        AssetDatabase.ImportAsset("Assets/Behaviors/FileBehavior.cs", ImportAssetOptions.ForceUpdate);
        AssetDatabase.ImportAsset("Assets/Behaviors/InitBehavior.cs", ImportAssetOptions.ForceUpdate);
        AssetDatabase.ImportAsset("Assets/Behaviors/KeyboardBehavior.cs", ImportAssetOptions.ForceUpdate);
        AssetDatabase.ImportAsset("Assets/Behaviors/SavegameBehavior.cs", ImportAssetOptions.ForceUpdate);
        AssetDatabase.ImportAsset("Assets/Behaviors/WebForm.cs", ImportAssetOptions.ForceUpdate);

		string[] args = Environment.GetCommandLineArgs();
		string arg = args[args.Length-1];
		char[] sep = { ',' };
		string[] platforms = arg.Split(sep);
		for ( int i=0 ; i<platforms.Length ; i++ )
		{
			switch ( platforms[i] )
			{
			case "windows":
				BuildPipeline.BuildPlayer(m_scenes, "Builds/WINDOWS/runtime.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
				break;
			case "web":
				BuildPipeline.BuildPlayer(m_scenes, "Builds/WEB", BuildTarget.WebGL, BuildOptions.None);
				break;
			case "ios":
				BuildPipeline.BuildPlayer(m_scenes, "Builds/IOS", BuildTarget.iOS, BuildOptions.None);
				break;
			case "android":
				EditorUserBuildSettings.buildAppBundle = true;
				//System.IO.Directory.CreateDirectory("Builds/ANDROID");
				//BuildPipeline.BuildPlayer(m_scenes, "Builds/ANDROID", BuildTarget.Android, BuildOptions.None);
				BuildPipeline.BuildPlayer(m_scenes, "Builds/ANDROID/runtime.aab", BuildTarget.Android, BuildOptions.None);
				break;
			}
		}
	}
}
