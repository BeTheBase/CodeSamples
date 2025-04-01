using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using System;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System.Linq;

public class DialogueNode : Node
{
    public string GUID;
    public string DialogueText;
    public bool EntryPoint = false;
    public AudioClip audioClip;
    private List<Port> choicePorts = new List<Port>(); // Track dynamic choice ports
    public Port InputPort; // Input port to receive connections
    public Port DefaultOutputPort; // Default output for linear progression

    public DialogueNode()
    {
        title = "Dialogue Node";
        AddToClassList("dialogue-node");

        // Create Input Port
        InputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(DialogueNode));
        InputPort.portName = "Input";
        inputContainer.Add(InputPort);
        Debug.Log($"🔵 Created Input Port for node {GUID}");

        // Create Default Output Port
        DefaultOutputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(DialogueNode));
        DefaultOutputPort.portName = "Next";
        outputContainer.Add(DefaultOutputPort);
        Debug.Log($"🟢 Created Output Port 'Next' for node {GUID}");

        // Dialogue Text
        var textField = new TextField("Dialogue Text:");
        textField.RegisterValueChangedCallback(evt => DialogueText = evt.newValue);
        mainContainer.Add(textField);

        // Audio Clip Selector
        var audioField = new ObjectField("Audio Clip")
        {
            objectType = typeof(AudioClip),
            allowSceneObjects = false
        };
        audioField.RegisterValueChangedCallback(evt => audioClip = (AudioClip)evt.newValue);
        mainContainer.Add(audioField);

        // Add Choice Button (only for non-entry nodes)
        if (!EntryPoint)
        {
            var addChoiceButton = new Button(() => AddChoicePort())
            {
                text = "Add Choice"
            };
            mainContainer.Add(addChoiceButton);

            // NEW: Button to create a linked node
            var createLinkedNodeButton = new Button(() => CreateLinkedNode())
            {
                text = "Create Linked Node"
            };
            mainContainer.Add(createLinkedNodeButton);
        }

        // Hover Effects
        RegisterCallback<MouseEnterEvent>(evt => style.backgroundColor = new StyleColor(new Color(0.5f, 0.5f, 0.5f, 1f)));
        RegisterCallback<MouseLeaveEvent>(evt => style.backgroundColor = new StyleColor(new Color(0.3f, 0.3f, 0.3f, 1f)));
    }

    /// <summary>
    /// Allows input and output ports to connect properly
    /// </summary>
    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);

        foreach (var port in outputContainer.Children().OfType<Port>())
        {
            port.portType = typeof(float); // Ensure port compatibility
        }

        InputPort.portType = typeof(float); // Ensure input port is valid
    }

    /// <summary>
    /// Adds a new choice port dynamically
    /// </summary>
    public void AddChoicePort()
    {
        var port = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(DialogueNode));
        port.portName = $"Choice {choicePorts.Count + 1}";

        // Ensure port compatibility
        Debug.Log($"🟡 Creating Choice Port '{port.portName}' for node {GUID}");

        // Add a text field for the choice text
        var choiceTextField = new TextField("Choice:");
        choiceTextField.RegisterValueChangedCallback(evt =>
        {
            port.portName = evt.newValue;
            Debug.Log($"✏️ Updated Choice Port Name: {port.portName}");
        });

        // Add a delete button
        var deleteButton = new Button(() => RemoveChoicePort(port))
        {
            text = "X"
        };

        var choiceContainer = new VisualElement();
        choiceContainer.Add(choiceTextField);
        choiceContainer.Add(deleteButton);

        port.contentContainer.Add(choiceContainer);
        outputContainer.Add(port);
        choicePorts.Add(port);

        RefreshExpandedState();
        RefreshPorts();
    }


    /// <summary>
    /// Removes a choice port
    /// </summary>
    public void RemoveChoicePort(Port port)
    {
        outputContainer.Remove(port);
        choicePorts.Remove(port);
        RefreshExpandedState();
        RefreshPorts();
    }

    /// <summary>
    /// Extracts choice data for saving into JSON
    /// </summary>
    public List<DialogueGraphChoiceData> ExtractChoices(Dictionary<string, List<string>> graphConnections)
    {
        List<DialogueGraphChoiceData> choices = new List<DialogueGraphChoiceData>();

        foreach (Port port in choicePorts)
        {
            string nextDialogueGUID = "";

            // Check if this node has a stored connection
            if (graphConnections.TryGetValue(GUID, out List<string> connectedNodes))
            {
                if (choicePorts.IndexOf(port) < connectedNodes.Count)
                {
                    nextDialogueGUID = connectedNodes[choicePorts.IndexOf(port)];
                }
            }

            choices.Add(new DialogueGraphChoiceData
            {
                choiceText = port.portName,
                nextDialogue = nextDialogueGUID
            });
        }

        return choices;
    }

    /// <summary>
    /// Creates a new dialogue node and connects it automatically
    /// </summary>
    private void CreateLinkedNode()
    {
        DialogueGraphView graphView = (DialogueGraphView)this.GetFirstAncestorOfType<DialogueGraphView>();
        if (graphView == null) return;

        // Create a new node
        var newNode = graphView.CreateDialogueNode("New Linked Dialogue", false, GetPosition().position + new Vector2(300, 0));
        graphView.AddElement(newNode);

        // Create an edge (connection) from this node to the new node
        var newEdge = new Edge
        {
            output = this.DefaultOutputPort,
            input = newNode.InputPort
        };

        newEdge.output.Connect(newEdge);
        newEdge.input.Connect(newEdge);
        graphView.AddElement(newEdge);

        // Store the connection in the graph
        if (!graphView.graphConnections.ContainsKey(GUID))
        {
            graphView.graphConnections[GUID] = new List<string>();
        }
        graphView.graphConnections[GUID].Add(newNode.GUID);
    }
}
