using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator 
{
    ShapeSettings shapeSettings;
    NoiseFilter noiseFilter;

    public ShapeGenerator(ShapeSettings shapeSettings)
    {
        this.shapeSettings = shapeSettings;
        noiseFilter = new NoiseFilter(shapeSettings.noiseSettings);
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float elevation = noiseFilter.Evaluate(pointOnUnitSphere);
        return pointOnUnitSphere * shapeSettings.planetRadius * (1 + elevation);
    }
}
