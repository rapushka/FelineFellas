using UnityEditor;

namespace FelineFellas.Editor
{
    public class ForceRecompile : EditorWindow
    {
        [MenuItem("375/Scripts/Force Recompile Scripts")]
        public static void ForceRecompileScripts()
        {
            UnlockAssemblies();

            EditorUtility.RequestScriptReload();
            AssetDatabase.Refresh();
        }

        [MenuItem("375/Scripts/Unlock Reload Assemblies")]
        public static void UnlockAssemblies()
        {
            EditorApplication.UnlockReloadAssemblies();
        }
    }
}