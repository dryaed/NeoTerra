using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Noise Settings", menuName = "Data/Noise Settings")]
public class NoiseSettings : ScriptableObject
{
    public float noiseZoom;
    public int octaves;
    public Vector2Int offset;
    public Vector2Int worldOffset;
    [FormerlySerializedAs("persistance")] public float persistence;
    public float redistributionModifier;
    public float exponent;
}
