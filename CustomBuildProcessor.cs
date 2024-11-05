using UnityEditor;

#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif

using System.Diagnostics;
using Debug = UnityEngine.Debug;

#if UNITY_EDITOR
public class CustomBuildProcessor : IPostprocessBuildWithReport
{
    private const string EnableShutdownPCAfterBuild = "EnableShutdownPCAfterBuild";

    public int callbackOrder => 0;

    public void OnPostprocessBuild(BuildReport report)
    {
        if (EditorPrefs.GetBool(EnableShutdownPCAfterBuild, true))
        {
            // Call the method to shut down the computer
            ShutdownComputer();
        }
        else
        {
            Debug.Log("Post-build process is disabled.");
        }
    }

    // Method to shut down the computer
    private static void ShutdownComputer()
    {
        var psi = new ProcessStartInfo("shutdown", "/s /t 0")
        {
            CreateNoWindow = true,
            UseShellExecute = false
        };
        Process.Start(psi);
    }

    // Method to add a menu item in File > Build Settings Custom
    [MenuItem("File/Build Settings Custom/Enable Shutdown PC After Build")]
    public static void TogglePostBuildProcess()
    {
        bool current = EditorPrefs.GetBool(EnableShutdownPCAfterBuild, true);
        EditorPrefs.SetBool(EnableShutdownPCAfterBuild, !current);
    }

    // Add a checkmark to the menu item
    [MenuItem("File/Build Settings Custom/Enable Shutdown PC After Build", true)]
    public static bool TogglePostBuildProcessValidate()
    {
        Menu.SetChecked("File/Build Settings Custom/Enable Shutdown PC After Build", EditorPrefs.GetBool(EnableShutdownPCAfterBuild, true));
        return true;
    }
}
#endif
