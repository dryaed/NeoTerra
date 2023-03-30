using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainWarping : MonoBehaviour
{
    public NoiseSettings noiseDomainX, noiseDomainY;
    public int amplitudeX = 20, amplitudeY = 20;
    
    public float GenerateDomainNoise(int x, int y, NoiseSettings defaultNoiseSettings)
    {
        Vector2 domainOffset = GenerateDomainOffset(x, y);
        return MyNoise.OctavePerlin(x + domainOffset.x, y + domainOffset.y, defaultNoiseSettings);
    }

    public Vector2 GenerateDomainOffset(int x, int y)
    {
        var noiseX = MyNoise.OctavePerlin(x, y, noiseDomainX) * amplitudeX;
        var noiseY = MyNoise.OctavePerlin(x, y, noiseDomainY) * amplitudeY;
        return new Vector2(noiseX, noiseY);
    }

    public Vector2Int GenerateDomainOffsetInt(int x, int y) // Calculating the center of a biome
    {
        return Vector2Int.RoundToInt(GenerateDomainOffset(x, y));
    }
}
