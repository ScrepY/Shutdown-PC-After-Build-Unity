# Shutdown PC After Build Unity(WIP)

This Unity script allows you to toggle an option to automatically shut down your computer after a build completes. The script adds a menu item in Unity's **File > Build Settings Custom** to enable or disable this post-build action.

---

## Installation

1. **Download** the `CustomBuildProcessor.cs` file or copy the code provided below.
2. In your **Unity project**, create a folder named `Editor`:
   - Navigate to *Assets > Create > Folder* and name it **Editor**.
3. **Move** the `CustomBuildProcessor.cs` file into the `Editor` folder.

---

## Usage

After installing, you can toggle the **Shutdown PC After Build** option in Unity:
- Go to **File > Build Settings Custom > Enable Shutdown PC After Build**.
- A checkmark next to this option indicates that the shutdown process is enabled.
[![Alt text](https://cdn.discordapp.com/attachments/1303482075331956757/1303495217886003361/Screenshot_7.png?ex=672bf605&is=672aa485&hm=ea71f8990af28fd730b8351716435204b46412cff3b454ffabed23d00c52bda8&)](https://example.com)

### Post-Build Actions
When enabled, the script will:
- **Shut down the computer** immediately after the build process completes.

### Important Note
- Ensure that you save all your work before running a build with the shutdown option enabled, as this will close all applications and shut down the system.

---

## Code

Below is the complete code for `CustomBuildProcessor.cs`:

```csharp
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
```
