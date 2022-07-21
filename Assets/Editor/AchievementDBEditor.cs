using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(AchievementDatabase))]
public class AchievementDBEditor : Editor
{
    private AchievementDatabase database;

    private void OnEnable()
    {
        database = (AchievementDatabase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate Enum"))
        {
            GenerateEnum();
        }
    }

    private void GenerateEnum()
    {
        string filepath = Path.Combine(Application.dataPath, "Achievements.cs");
        string code = "public enum Achievements {";
        foreach(Achievement achievement in database.Achievements)
        {
            code += achievement.id + ",";
        }
        code += "}";
        File.WriteAllText(filepath, code);
        AssetDatabase.ImportAsset("Assets/Achievements.cs");
    }
}
