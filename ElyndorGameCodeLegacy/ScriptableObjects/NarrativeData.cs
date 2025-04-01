using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NarrativeData", menuName = "Narrative/NarrativeData", order = 1)]
public class NarrativeData : ScriptableObject
{
    [TextArea(3, 10)]
    public List<string> narrativeTexts; // Stores all the texts in a list
}
