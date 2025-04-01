using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConceptArtData", menuName = "Narrative/ConceptArtData", order = 2)]
public class ConceptArtData : ScriptableObject
{
    public List<Sprite> conceptArtImages; // List of concept art images
}
