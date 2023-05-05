using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeams
{
    Vector3 pos, dir;
    GameObject laserObj;
    LineRenderer laser;
    List<Vector3> laserIndicies = new List<Vector3>();
    public LaserBeams(Vector3 pos, Vector3 dir, Material material){
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "LaserBeam";
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;

        this.laser.startColor = Color.green;
        this.laser.endColor = Color.green;

        CastRay(pos, dir, laser);
    }
    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser){
        laserIndicies.Add(pos);
        
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 30, 1)){
        }
        else{
            laserIndicies.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    void UpdateLaser(){
        int count = 0;
        laser.positionCount = laserIndicies.Count;

        foreach(Vector3 idx in laserIndicies){
            laser.SetPosition(count, idx);
            count++;
        }
    }
    void Checkhit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser){

    }
}
