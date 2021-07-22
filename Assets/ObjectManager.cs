using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    PlanetManager planet;
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlanetManager>())
        {
            planet = other.GetComponentInParent<PlanetManager>();
            if (!planet.objectsOnPlanet.Contains(transform))
            {
                planet.objectsOnPlanet.Add(transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<PlanetManager>())
        {
            planet = other.GetComponentInParent<PlanetManager>();
            if (planet.objectsOnPlanet.Contains(transform))
            {
                planet.objectsOnPlanet.Remove(transform);
            }
        }
    }
}
