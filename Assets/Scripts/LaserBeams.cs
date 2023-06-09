using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeams
{
    Vector3 pos, dir;
    GameObject laserObj;
    LineRenderer laser;
    List<Vector3> laserIndicies = new List<Vector3>();
    private GameObject laserGoal;
    private bool isGoalReached = false;

    public LaserBeams(Vector3 pos, Vector3 dir, Material material){
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "LaserBeam";
        this.laserObj.tag = "LaserBeam";
        this.laserObj.layer = LayerMask.NameToLayer("Default"); // Set the layer to default
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;

        this.laser.startColor = Color.red;
        this.laser.endColor = Color.red;

        CastRay(pos, dir, laser);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser){
        laserIndicies.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        int layerMask = (1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("Laser")); // Add the layer mask that includes the layer of the "LaserGoal" game object

        if(Physics.Raycast(ray, out hit, 30, layerMask)){
            Checkhit(hit, dir, laser);
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
        if(hitInfo.collider.gameObject.tag == "Mirror"){
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);

            CastRay(pos, dir, laser);
        }
        else if(hitInfo.collider.gameObject.tag == "LaserGoal"){
            if (!isGoalReached) {
                isGoalReached = true;
                LaserGoal laserGoalScript = hitInfo.collider.gameObject.GetComponent<LaserGoal>();
                laserGoalScript.trigger = true;
            }
            laserIndicies.Add(hitInfo.point);
            UpdateLaser();
        }
        else {
            laserIndicies.Add(hitInfo.point);
            UpdateLaser();
        }
    }

    public void Destroy(){
        GameObject.Destroy(laserObj);
    }
}