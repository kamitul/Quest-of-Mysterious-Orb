﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using DG.Tweening;
using System;

public class PatrolEnemy : EnemyGameObject<PatrolEnemyData>, IUpdatable, ILateUpdatable, IFixedUpdateable, IEnableable, IDisaable
{
    //GREEN ORB
    [SerializeField]
    private Transform[] points;

    [SerializeField]
    private Transform target;
    private int destPoint = 0;
    private int index = 0;

    private float startTime;

    private float distance;

    private void OnEnable() {
        startTime = Time.time;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void RotateTowardsPoint(Vector3 targetPos, float rotationSpeed)
    {
        Vector3 dir = targetPos - transform.position;

        var quaternionToRotate = Quaternion.FromToRotation(transform.right, dir) * transform.rotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, quaternionToRotate, rotationSpeed);
    }


    protected override void OnCollisionEnter(Collision collision)
    {
        
    }

    protected override void OnTriggerEneter(Collider collider)
    {
        
    }

    public void OnILateUpdate()
    {
        
    }

    public void OnIFixedUpdate()
    {
        
    }

    public void OnIUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance > 6f) {
            transform.position = Vector3.Lerp(transform.position, points[index].position, (EnemyData as PatrolEnemyData).MovingSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, points[index].position) < 2f) { 
                index++;
                index %= 3;
            }
        }
        else {
            if(distance > 2.5f) {
                transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * (EnemyData as PatrolEnemyData).MovingSpeed/2);
            }
        }
    }

    public void OnIDisable() {

    }

    public void OnIEnable() {

    }

    public override void ProcessHitOrb(OrbData orbData) {
        enemyHealth -= orbData.DamageGiven;
        if(enemyHealth < 0f) {
            Die();
        }
        else {
            Hit();
        }
    }

    private void Hit()
    {
        HitEffect.time = 0;
        HitEffect.Play();
    }

    private void Die()
    {
       var objectToSpawn = MyObjectPoolManager.Instance.GetObject("BounceOrb", true);
       objectToSpawn.transform.position = gameObject.transform.position;
       StartCoroutine(DieBehaviour());
    }

    private IEnumerator DieBehaviour()
    {
        DestroyEffect.time = 0;
        DestroyEffect.Play();
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}