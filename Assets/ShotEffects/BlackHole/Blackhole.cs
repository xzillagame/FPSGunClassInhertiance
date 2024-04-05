using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Blackhole : SpecialEffect
{
    [SerializeField] float LifeTime = 2f;
    private float currentLifeTime = 0f;


    [SerializeField] float ProjectileSpeed = 10f;


    [SerializeField] float GravityForce = 75f;
    List<Rigidbody> entites = new List<Rigidbody>();


    [SerializeField] SphereCollider sphereCollider;

    [SerializeField] LayerMask StopOnCollisionLayer;

    [SerializeField] private bool modeFlip = true;

    [SerializeField][Range(0f,100f)] private float RBVelocityClampMax;

    public void InitializeBlackHole(float life = 2f, float gravForce = 75f, float projSpeed = 10f)
    {
        LifeTime = life;
        GravityForce = gravForce;
        ProjectileSpeed = projSpeed;
        GetComponent<Rigidbody>().velocity = transform.forward * projSpeed;


    }




    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            
            if(!entites.Contains(rb))
            {
                entites.Add(rb);
                //rb.velocity = (transform.position - rb.transform.position).normalized * ProjectileSpeed;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            if(entites.Contains(rb))
            {
                rb.velocity = rb.velocity * 0.5f;
                entites.Remove(rb);
            }
        }
    }

    [SerializeField][Range(0f,1f)] float slider;

    private void FixedUpdate()
    {
       for(int i = 0; i < entites.Count; i++)
        {
            Vector3 rbLocation = entites[i].position;
            Vector3 centerLocation = transform.position;
            Vector3 forceDirection = (centerLocation - rbLocation).normalized;



            float NewtonsGravityForce = 1 / Mathf.Pow(forceDirection.magnitude, 2);
            float scaledForce = NewtonsGravityForce * GravityForce;


            forceDirection *= scaledForce;


            Vector3 tempvel = entites[i].velocity;

            entites[i].AddForce(forceDirection, ForceMode.Force);

            entites[i].velocity = Vector3.ClampMagnitude(entites[i].velocity, RBVelocityClampMax);

            //float distanceFromCenter = (centerLocation - rbLocation).magnitude;//(rbLocation - centerLocation).magnitude;

            //distanceFromCenter = Mathf.Clamp(distanceFromCenter, 1f, Mathf.Infinity);

            //float forceScaler = Mathf.Lerp(0.0f, 1f, distanceFromCenter / sphereCollider.radius);



            //Vector3 previousVelocity = entites[i].velocity;
            //entites[i].velocity = entites[i].velocity.normalized;
            //entites[i].velocity = Vector3.Lerp(entites[i].velocity, Vector3.zero, 0.1f);
            //entites[i].AddForce((forceDirection) * forceScaler * GravityForce, ForceMode.Acceleration);
            //Debug.Log(forceDirection / distanceFromCenter);

            //entites[i].AddForce(forceDirection / (distanceFromCenter * distanceFromCenter) * GravityForce, ForceMode.Acceleration);

            Vector3 gravityForce =

            entites[i].velocity = Vector3.ClampMagnitude(entites[i].velocity, GravityForce);
                //entites[i].AddForce( (GravityForce / distanceFromCenter) * (forceDirection / distanceFromCenter), ForceMode.Force);
            //if (modeFlip) 
            //{
            //}
            //else
            //{
            //    entites[i].AddForce(forceDirection / distanceFromCenter * (GravityForce / distanceFromCenter), ForceMode.Acceleration);
            //}
            //entites[i].velocity = previousVelocity;
        }
    }


    private void OnDestroy()
    {
        for(int i = 0; i < entites.Count; i++)
        {
            entites[i].velocity = entites[i].velocity * 0.5f;
        }
    }


    private void Update()
    {
        if(currentLifeTime >= LifeTime)
        {
            Destroy(this.gameObject);
        }

        currentLifeTime += Time.deltaTime;

        //transform.position += ProjectileSpeed * Time.deltaTime * (transform.forward);

    }


}
