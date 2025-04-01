using UnityEditor;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UIElements;

public class DialogueGraphEditor : EditorWindow
{
    private DialogueGraphView _graphView;
    private string savePath = "Assets/_Dialogues/DialogueGraph.json";

    [MenuItem("Tools/Dialogue Graph Editor")]
    public static void OpenGraphWindow()
    {
        var window = GetWindow<DialogueGraphEditor>();
        window.titleContent = new GUIContent("Dialogue Graph");
        window.Show();
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }

    private void ConstructGraphView()
    {
        _graphView = new DialogueGraphView
        {
            name = "Dialogue Graph"
        };
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void GenerateToolbar()
    {
        var toolbar = new UnityEditor.UIElements.Toolbar();

        var saveButton = new Button(() => SaveGraphData())
        {
            text = "Save"
        };
        toolbar.Add(saveButton);

        var loadButton = new Button(() => LoadGraphData())
        {
            text = "Load"
        };
        toolbar.Add(loadButton);
        var addNodeButton = new Button(() => _graphView.CreateDialogueNode("New Dialogue", false, new Vector2(300, 300)))
        {
            text = "Add Node"
        };
        toolbar.Add(addNodeButton);

        rootVisualElement.Add(toolbar);
    }

    private void SaveGraphData()
    {
        DialogueGraphData graphData = _graphView.ExtractGraphData();
        string json = JsonConvert.SerializeObject(graphData, Formatting.Indented);
        File.WriteAllText(savePath, json);
        AssetDatabase.Refresh();
        Debug.Log($"✅ Dialogue Graph saved to {savePath}");
    }

    private void LoadGraphData()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("⚠ No save file found!");
            return;
        }

        string json = File.ReadAllText(savePath);
        DialogueGraphData graphData = JsonConvert.DeserializeObject<DialogueGraphData>(json);
        _graphView.LoadGraphData(graphData);
        Debug.Log("✅ Dialogue Graph loaded successfully!");
    }
}
