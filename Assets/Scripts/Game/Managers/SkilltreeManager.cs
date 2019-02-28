using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilltreeManager : MonoBehaviour
{
    #region Singleton

    public static SkilltreeManager manager;

    private void Awake()
    {
        manager = this;
    }

    #endregion    

    //LIGHT ATTACK
    public bool lightTroop = false; 
    public bool lightTroopDamageSpell = false; 
    public float lightTroopDamageSpellDamage = 1;
    public float lightTroopBonusLife = 1; 
    public float lightTroopBonusSpeed = 1; 
    public float lightTroopBonusDamage = 1;
    public bool mobileMedicalStation = false; 


    //HEAVY ATTACK
    public bool bomber = false; 
    public float bomberMissileSpell = 0;
    public float bomberBonusDamage = 1;
    public float bomberBonusLife = 1; 
    public float bomberProductionSpeed = 1; 

    //R&D
    public bool hacker = false; 
    public bool hackerHackSpell = false; 
    public bool hackerIEMSpell = false; 
    public float hackerVisionSpell = 0; 
    public float hackerBonusSpeed = 1; 

    //DESTROYEUR
    public bool destroyer = false; 
    public float destroyerBonusLife = 1; 
    public float destroyerSupportSpell = 1; 
    public bool destroyerBanner = false; 

    //ECO
    public float constructionSpeed = 1; 
    public float miningSpeed = 1; 
    public float laboratorySpeed = 1; 
    public float energyFarmSpeed = 1; 

    //DEFENSE
    public float radar = 0;
    public float radarRange = 1; 
    public bool turrel = false;
    public float turrelDamage = 1;
    public float turrelRange = 1;
}
