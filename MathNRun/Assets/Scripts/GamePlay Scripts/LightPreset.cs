using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Scriptables/Lighting Preset")]
public class LightPreset : ScriptableObject
{
    public Gradient AmbientColour;
    public Gradient SunColour;
    public Gradient FogColour;
}
