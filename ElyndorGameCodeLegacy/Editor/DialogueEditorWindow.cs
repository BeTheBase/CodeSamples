using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

public class DialogueEditorWindow : EditorWindow
{
    private List<DialogueEntryData> dialogues = new List<DialogueEntryData>();
    private string dialoguesPath = "Assets/Dialogues/";
    private string selectedFile = "";
    private Vector2 scrollPosition;

    [MenuItem("Tools/Dialogue Editor")]
    public static void ShowWindow()
    {
        GetWindow<DialogueEditorWindow>("Dialogue Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Dialogue Editor", EditorStyles.boldLabel);

        dialoguesPath = EditorGUILayout.TextField("Dialogue Files Path:", dialoguesPath);

        GUILayout.Space(10);
        if (GUILayout.Button("Select Dialogue File"))
        {
            selectedFile = EditorUtility.OpenFilePanel("Select Dialogue File", dialoguesPath, "json,yaml");
            if (!string.IsNullOrEmpty(selectedFile)) LoadDialogueFile();
        }
        GUILayout.Label("Selected File: " + (string.IsNullOrEmpty(selectedFile) ? "None" : Path.GetFileName(selectedFile)));

        GUILayout.Space(10);
        if (GUILayout.Button("New Dialogue Entry")) dialogues.Add(new DialogueEntryData());

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        for (int i = 0; i < dialogues.Count; i++)
        {
            DrawDialogueEntry(i);
        }
        GUILayout.EndScrollView();

        if (dialogues.Count > 0)
        {
            GUILayout.Space(10);
            if (GUILayout.Button("Save JSON")) SaveToFile(selectedFile, "json");
            if (GUILayout.Button("Save YAML")) SaveToFile(selectedFile, "yaml");
        }
    }

    private void DrawDialogueEntry(int index)
    {
        var entry = dialogues[index];
        GUILayout.BeginVertical("box");

        entry.dialogueID = EditorGUILayout.TextField("Dialogue ID:", entry.dialogueID);
        entry.dialogueText = EditorGUILayout.TextField("Dialogue Text:", entry.dialogueText);
        entry.audioClipName = EditorGUILayout.TextField("Audio Clip Name:", entry.audioClipName);
        entry.hasChoices = EditorGUILayout.Toggle("Has Choices:", entry.hasChoices);

        if (entry.hasChoices)
        {
            GUILayout.Label("Choices:");
            for (int i = 0; i < entry.choices.Count; i++)
            {
                GUILayout.BeginHorizontal();
                entry.choices[i].choiceText = EditorGUILayout.TextField("Choice Text:", entry.choices[i].choiceText);
                entry.choices[i].nextDialogue = EditorGUILayout.TextField("Next Dialogue:", entry.choices[i].nextDialogue);
                entry.choices[i].conditionID = (DialogueID)EditorGUILayout.EnumPopup("Condition:", entry.choices[i].conditionID);
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    entry.choices.RemoveAt(i);
                    break;
                }
                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Choice")) entry.choices.Add(new DialogueChoiceData());
        }

        if (GUILayout.Button("Remove Dialogue Entry")) dialogues.RemoveAt(index);

        GUILayout.EndVertical();
    }

    private void LoadDialogueFile()
    {
        if (string.IsNullOrEmpty(selectedFile)) return;
        string fileContent = File.ReadAllText(selectedFile);
        dialogues = JsonConvert.DeserializeObject<List<DialogueEntryData>>(fileContent) ?? new List<DialogueEntryData>();
        Debug.Log("Loaded dialogue file: " + selectedFile);
    }

    private void SaveToFile(string fileName, string format)
    {
        if (dialogues.Count == 0 || string.IsNullOrEmpty(fileName)) return;
        string path = dialoguesPath + Path.GetFileName(fileName);
        string data = format == "json" ? JsonConvert.SerializeObject(dialogues, Formatting.Indented)
                                       : SerializationUtility.ToYaml(dialogues);

        File.WriteAllText(path, data);
        AssetDatabase.Refresh();
        Debug.Log($"Dialogue saved to {path}");
    }
}