using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class DialogueGraphView : GraphView
{
    public Dictionary<string, List<string>> graphConnections = new Dictionary<string, List<string>>();

    public DialogueGraphView()
    {
        styleSheets.Add(Resources.Load<StyleSheet>("DialogueGraph")); // Load custom styles

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        // Background grid
        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        // Background image (optional)
        var background = new VisualElement();
        background.style.backgroundImage = new StyleBackground(Resources.Load<Texture2D>("GraphBackground"));
        background.style.position = Position.Absolute;
        background.style.width = Length.Percent(100);
        background.style.height = Length.Percent(100);
        Insert(0, background);

        // Enable a custom edge connector to allow valid connections
        this.nodeCreationRequest += context => AddDialogueNode(context.screenMousePosition);

        AddElement(GenerateEntryPointNode());

        graphViewChanged += OnGraphViewChanged; // Handle edge connections
    }
    private void AddDialogueNode(Vector2 position)
    {
        var node = CreateDialogueNode("New Dialogue", false, position);
        AddElement(node);
    }

    /// <summary>
    /// Handles new connections between nodes
    /// </summary>
    private GraphViewChange OnGraphViewChanged(GraphViewChange change)
    {
        if (change.edgesToCreate != null)
        {
            foreach (var edge in change.edgesToCreate)
            {
                var fromNode = edge.output.node as DialogueNode;
                var toNode = edge.input.node as DialogueNode;

                if (fromNode == null || toNode == null)
                {
                    Debug.LogWarning("⚠ Connection failed: One or both nodes are null!");
                    continue;
                }

                Debug.Log($"🔗 Attempting connection from {fromNode.GUID} (port: {edge.output.portName}) ➝ {toNode.GUID} (port: {edge.input.portName})");

                // Ensure our graph stores multiple connections properly
                if (!graphConnections.ContainsKey(fromNode.GUID))
                {
                    graphConnections[fromNode.GUID] = new List<string>();
                }
                graphConnections[fromNode.GUID].Add(toNode.GUID);

                Debug.Log($"✅ Connection stored successfully: {fromNode.GUID} ➝ {toNode.GUID}");
            }
        }
        return change;
    }
     
    /// <summary>
    /// Extracts Graph Data for saving
    /// </summary>
    public DialogueGraphData ExtractGraphData()
    {
        DialogueGraphData graphData = new DialogueGraphData();

        foreach (var node in nodes)
        {
            if (node is DialogueNode dialogueNode)
            {
                var nodeData = new DialogueNodeData
                {
                    GUID = dialogueNode.GUID,
                    dialogueText = dialogueNode.DialogueText,
                    posX = dialogueNode.GetPosition().x,
                    posY = dialogueNode.GetPosition().y,
                    isEntryPoint = dialogueNode.EntryPoint,
                    choices = dialogueNode.ExtractChoices(graphConnections)
                };

                graphData.nodes.Add(nodeData);
            }
        }

        return graphData;
    }

    /// <summary>
    /// Generates the Entry Point node (Start)
    /// </summary>
    private DialogueNode GenerateEntryPointNode()
    {
        var node = new DialogueNode
        {
            title = "Start",
            GUID = Guid.NewGuid().ToString(),
            DialogueText = "Begin van de dialoog",
            EntryPoint = true
        };

        var outputPort = node.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
        outputPort.portName = "Start";
        node.outputContainer.Add(outputPort);

        node.RefreshExpandedState();
        node.RefreshPorts();
        node.SetPosition(new Rect(200, 200, 150, 200));

        AddElement(node);
        return node;
    }

    /// <summary>
    /// Enables right-click menu for adding dialogue nodes
    /// </summary>
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        evt.menu.AppendAction("Add Dialogue Node", actionEvent => CreateDialogueNode("New Dialogue", false, evt.localMousePosition));
    }

    /// <summary>
    /// Loads saved graph data into the editor
    /// </summary>
    public void LoadGraphData(DialogueGraphData graphData)
    {
        DeleteElements(graphElements);
        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

        foreach (var nodeData in graphData.nodes)
        {
            var newNode = CreateDialogueNode(nodeData.dialogueText, nodeData.isEntryPoint);
            newNode.SetPosition(new Rect(new Vector2(nodeData.posX, nodeData.posY), new Vector2(150, 200)));
            AddElement(newNode);
            nodeLookup[nodeData.GUID] = newNode;
        }

        foreach (var nodeData in graphData.nodes)
        {
            if (nodeLookup.TryGetValue(nodeData.GUID, out var fromNode))
            {
                foreach (var choice in nodeData.choices)
                {
                    if (nodeLookup.TryGetValue(choice.nextDialogue, out var toNode))
                    {
                        var fromPort = fromNode.outputContainer.Children()
                            .FirstOrDefault(port => port is Port && (port as Port).portName == choice.choiceText) as Port;

                        var toPort = toNode.inputContainer[0] as Port;

                        if (fromPort != null && toPort != null)
                        {
                            var edge = new Edge
                            {
                                output = fromPort,
                                input = toPort
                            };

                            fromPort.Connect(edge);
                            toPort.Connect(edge);
                            AddElement(edge);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Creates a new dialogue node
    /// </summary>
    public DialogueNode CreateDialogueNode(string text, bool isEntryPoint = false, Vector2 position = default)
    {
        var node = new DialogueNode
        {
            title = isEntryPoint ? "Start" : "Dialogue",
            DialogueText = text,
            GUID = Guid.NewGuid().ToString(),
            EntryPoint = isEntryPoint
        };

        if (!isEntryPoint)
        {
            var addChoiceButton = new Button(() => node.AddChoicePort())
            {
                text = "Add Choice"
            };
            node.mainContainer.Add(addChoiceButton);
        }

        if (position != default)
        {
            node.SetPosition(new Rect(position, new Vector2(150, 200)));
        }

        AddElement(node);
        return node;
    }
}
