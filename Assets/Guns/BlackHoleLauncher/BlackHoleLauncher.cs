using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleLauncher : Gun
{

    [SerializeField] Blackhole BlackHoleProjectile;

    [Header("Blackhole Launcher Stats")]
    [SerializeField] float DamagePerShot;
    [SerializeField] float BulletSpeed = 50f;
    [SerializeField] float BulletLifeTime = 1.25f;
    [SerializeField] float BulletKnockBackForce = 0f;

    [SerializeField] Transform LeftBarrelEnd;

    [System.Serializable] private struct BlackHoleProperties
    {
        public float life;
        public float gravityForce;
        public float speed;
    }
    [SerializeField] private BlackHoleProperties BlackHoleProjectileProperties;


    public override bool AttemptFire()
    {
        if(!base.AttemptFire())
        {
            return false;
        }



        Blackhole blackhole = Instantiate(BlackHoleProjectile, gunBarrelEnd.position, gunBarrelEnd.rotation);
        blackhole.InitializeBlackHole(BlackHoleProjectileProperties.life, BlackHoleProjectileProperties.gravityForce,
                                                BlackHoleProjectileProperties.speed);


        //Blackhole blackhole2 = Instantiate(BlackHoleProjectile, LeftBarrelEnd.position, LeftBarrelEnd.rotation);
        //blackhole2.InitializeBlackHole(BlackHoleProjectileProperties.life, BlackHoleProjectileProperties.gravityForce,
        //                                        BlackHoleProjectileProperties.speed);




        anim.SetTrigger("shoot");
        elapsed = 0f;
        ammo -= 1;
        return true;
    }








}
