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
    private bool triggerCreateNextFrame = false;

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
        GUILayout.Label("Create New Script", EditorStyles.boldLabel);

        GUI.SetNextControlName("ScriptNameField");
        scriptName = EditorGUILayout.TextField("Script Name", scriptName);
        selectedBaseClassIndex = EditorGUILayout.Popup("Inherits From", selectedBaseClassIndex, availableBaseClasses.ToArray());
        selectedFolder = EditorGUILayout.TextField("Folder", selectedFolder);

        if (GUILayout.Button("Select Folder"))
        {
            string path = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath, "");
            if (!string.IsNullOrEmpty(path) && path.StartsWith(Application.dataPath))
            {
                selectedFolder = "Assets" + path.Substring(Application.dataPath.Length);
            }
            else
            {
                EditorUtility.DisplayDialog("Invalid Folder", "Please select a folder inside the Assets directory.", "OK");
            }
        }

        if (autoFocus)
        {
            EditorGUI.FocusTextInControl("ScriptNameField");
            autoFocus = false;
        }

        // 🔥 Detect Enter key and delay execution
        if (Event.current.keyCode == KeyCode.Return)
        {
            GUI.FocusControl(null);           // Commit edits
            triggerCreateNextFrame = true;    // Flag for next frame
            Event.current.Use();              // Consume event
        }

        if (GUILayout.Button("Create Script"))
        {
            CreateNewScript();
        }

        // 🔁 Run script creation on next frame
        if (triggerCreateNextFrame)
        {
            triggerCreateNextFrame = false;
            CreateNewScript();
        }
    }

    private void LoadBaseClasses()
    {
        availableBaseClasses.Clear();
        availableBaseClasses.Add("MonoBehaviour");
        availableBaseClasses.Add("ScriptableObject");

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
        if (string.IsNullOrWhiteSpace(scriptName))
        {
            EditorUtility.DisplayDialog("Invalid Name", "Script name cannot be empty.", "OK");
            return;
        }

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
