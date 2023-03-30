using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public static class MyNoise
{
    public static float RemapValue(float value, float outputMin, float outputMax, float initialMin, float initalMax) // general method for remapping value from one range to another
    {
        return outputMin + (value - initialMin) * (outputMax - outputMin) / (initalMax - initialMin);
    }

    public static float RemapValue(float value, float outputMin, float outputMax) // used remapping the noisemap to the chunk height e.g. .0 - 1.0 to 0 - 100
    {
        return outputMin + (value - 0) * (outputMax - outputMin) / (1 - 0);
    }

    public static int RemapValueFromPerlinToInt(float value, float outputMin, float outputMax) // to use with chunks
    {
        return (int) RemapValue(value, outputMin, outputMax);
    }

    public static float Redistribution(float noise, NoiseSettings settings) // easing the noise
    {
        return Mathf.Pow(noise * settings.redistributionModifier, settings.exponent);
    }
    
    public static float OctavePerlin(float x, float z, NoiseSettings settings)
    {
        x *= settings.noiseZoom;
        z *= settings.noiseZoom;
        x += settings.noiseZoom; // this is for keeping it a float
        z += settings.noiseZoom; 
        
        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float amplitudeSum = 0; // Used for normalizing the result to .0 - 1.0 range
        
        for (int i = 0; i < settings.octaves; i++) // generating through the octaves and combining them
        {
            total += Mathf.PerlinNoise((settings.offset.x + settings.worldOffset.x + x) * frequency,
                (settings.offset.y + settings.worldOffset.y + z) * frequency) * amplitude;

            amplitudeSum += amplitude;
            amplitude *= settings.persistence;
            frequency *= 2;
        }

        return total / amplitudeSum;
    }
}
