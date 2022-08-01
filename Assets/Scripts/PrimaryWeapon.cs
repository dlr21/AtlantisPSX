using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryWeapon : MonoBehaviour
{

    public bool onAir;
    public bool canShoot;

    public GameObject boomerang;
    public GameObject grenades;
    public GameObject handfulofRocks;
    public GameObject boltStaff;
    public GameObject flareGun;

    private Quaternion startingAngle = Quaternion.AngleAxis(-40, Vector3.up);
    private Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    public Vector3 enemyPosition=new Vector3(99, 99, 99);

    void Start()
    {
        onAir = false;
        canShoot = false;
    }

    public void Boomerang()
    {
        if (canShoot) { 
            enemyPosition = DetectEnemy();
            if (enemyPosition != new Vector3(99, 99, 99))
            {
                Instantiate(boomerang, transform.position + new Vector3(0, 2.5f, 0), transform.rotation);
                onAir = true;
                canShoot = false;
            }
        }
    }

    public void Grenades()
    {
        if (canShoot)
        {
            enemyPosition = DetectEnemy();
            if (enemyPosition != new Vector3(99, 99, 99))
            {
                Instantiate(grenades, transform.position + new Vector3(0, 2.5f, 0), transform.rotation);
                onAir = true;
                canShoot = false;
            }
        }
    }

    public void HandfulofRocks()
    {
        if (canShoot)
        {
            enemyPosition = DetectEnemy();
            if (enemyPosition != new Vector3(99, 99, 99))
            {
                Instantiate(handfulofRocks, transform.position + new Vector3(0, 2.5f, 0), transform.rotation);
                onAir = true;
                canShoot = false;
            }
        }
    }

    public void BoltStaff()
    {
        if (canShoot)
        {
            enemyPosition = DetectEnemy();
            if (enemyPosition != new Vector3(99, 99, 99))
            {
                Instantiate(boltStaff, transform.position + new Vector3(0, 2.5f, 0), transform.rotation);
                onAir = true;
                canShoot = false;
            }
        }
    }

    public void FlareGun()
    {
        if (canShoot)
        {
            enemyPosition = DetectEnemy();
            if (enemyPosition != new Vector3(99, 99, 99))
            {
                Instantiate(flareGun, transform.position + new Vector3(0, 2.5f, 0), transform.rotation);
                onAir = true;
                canShoot = false;
            }
        }
    }



    private Vector3 DetectEnemy()
    {

        for (var j = 0; j < 6; j++)
        {
            RaycastHit hit;
            var angle = transform.rotation * startingAngle;
            var direction = angle * Vector3.forward;
            var pos = transform.position + new Vector3(0, 0.5f, 0) * j;

            for (var i = 0; i < 16; i++)
            {
                if (Physics.Raycast(pos, direction, out hit, 20))
                {
                    var enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy)
                    {
                        Debug.Log("detectado");
                        return enemy.transform.position;
                        //Enemy was seen
                    }
                }
                direction = stepAngle * direction;
            }
        }
        return new Vector3(99, 99, 99);
    }

}
