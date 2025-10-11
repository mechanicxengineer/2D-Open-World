using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Unity.VisualScripting;

public class CreateScript : EditorWindow
{
    private string scriptName = "NewScript";
    private string selectedFolder = "Assets/SourceCode";
    private List<string> availableBaseClasses = new List<string>();
    private int selectedBaseClassIndex = 0;
    private bool autoFocus = true;

    [MenuItem("Component/Scripts/New Script %&n")]
    public static void ShowWindow()
    {
        var window = GetWindow<CreateScript>();
        window.titleContent = new GUIContent("Create Script");
        window.minSize = new Vector2(400, 140);
        window.maxSize = new Vector2(400, 140);
        window.Center();
        ((CreateScript)window).LoadBaseClasses();
    }

    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        GUI.SetNextControlName(scriptName);

        GUILayout.Label("Create New Script", EditorStyles.boldLabel);
        scriptName = EditorGUILayout.TextField("Script Name", scriptName);
        selectedBaseClassIndex = EditorGUILayout.Popup("Inherits From", selectedBaseClassIndex, availableBaseClasses.ToArray());
        selectedFolder = EditorGUILayout.TextField("Folder", selectedFolder);

        if (GUILayout.Button("Select Folder"))
        {
            string path = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath, "");
            if (!string.IsNullOrEmpty(path))
            {
                if (path.StartsWith(Application.dataPath))
                {
                    selectedFolder = "Assets" + path.Substring(Application.dataPath.Length);
                }
                else
                {
                    EditorUtility.DisplayDialog("Invalid Folder", "Please select a folder inside the Assets directory.", "OK");
                }
            }
        }

        if (autoFocus)
        {
            EditorGUI.FocusTextInControl(scriptName);
            autoFocus = false;
        }

        if (GUILayout.Button("Create Script") || (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return))
        {
            EditorGUI.EndChangeCheck();
            CreateNewScript();
        }
    }

    private void LoadBaseClasses()
    {
        availableBaseClasses.Clear();
        availableBaseClasses.Add("MonoBehaviour"); // Default

        string[] files = Directory.GetFiles("Assets/SourceCode", "*.cs", SearchOption.AllDirectories);
        Regex classRegex = new Regex(@"public\s+class\s+(\w+)", RegexOptions.Compiled);

        foreach (string file in files)
        {
            string[] lines = File.ReadAllLines(file);
            foreach (string line in lines)
            {
                Match match = classRegex.Match(line);
                if (match.Success)
                {
                    string className = match.Groups[1].Value;
                    if (!availableBaseClasses.Contains(className))
                    {
                        availableBaseClasses.Add(className);
                    }
                    break;
                }
            }
        }
    }

    private void CreateNewScript()
    {
        if (!Directory.Exists(selectedFolder))
        {
            Directory.CreateDirectory(selectedFolder);
        }

        string baseClass = availableBaseClasses[selectedBaseClassIndex];
        string cleanName = scriptName.Replace(" ", "");
        string scriptPath = Path.Combine(selectedFolder, cleanName + ".cs");

        string template = $"using UnityEngine;\n\npublic class {cleanName} : {baseClass}\n{{\n\tvoid Start()\n\t{{\n\t\t\n\t}}\n\n\tvoid Update()\n\t{{\n\t\t\n\t}}\n}}";

        File.WriteAllText(scriptPath, template);
        AssetDatabase.Refresh();

        Object createdScript = AssetDatabase.LoadAssetAtPath<Object>(scriptPath);
        Selection.activeObject = createdScript;
        EditorGUIUtility.PingObject(createdScript);
        AssetDatabase.OpenAsset(createdScript);

        Close();
    }
}
