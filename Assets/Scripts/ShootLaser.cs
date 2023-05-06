using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    List<LaserBeams> activeLasers = new List<LaserBeams>();

    // Update is called once per frame
    void Update()
    {
        // Destroy all active lasers
        foreach (LaserBeams laser in activeLasers) {
            laser.Destroy();
        }
        activeLasers.Clear();

        // Create a new laser
        LaserBeams beam = new LaserBeams(gameObject.transform.position, gameObject.transform.forward, material);
        activeLasers.Add(beam);
    }
}