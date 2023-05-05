using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    LaserBeams beam;

    // Update is called once per frame
    void Update()
    {
        Destroy(GameObject.Find("LaserBeam"));
        beam = new LaserBeams(gameObject.transform.position, gameObject.transform.forward, material);
    }
}
