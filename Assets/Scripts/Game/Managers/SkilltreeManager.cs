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

    //DAMAGE
    public bool basicTroop = false;
    public bool basicTroopSpeedBoost = false;
    public float basicTroopBonusDamage = 1;
    public float basicTroopBonusLife = 1;

    //DEFENSE
    public bool Bomber = false;
    public float BomberRange = 1;
    public float buildingBonusLife = 1;
    public int radarMax = 0;
    public float radarRange = 1;

    //TECH
    public bool hacker = false; //Movable unit
    public bool hackerSpellVision = false; //
    public float speedBuff = 1;
    public bool spellStun = false;

    //ECO
    public float constructionSpeed = 1;
    public float productionSpeed = 1;
    public float miningSpeed = 1;
    public float farmingSpeed = 1;
    public float laboratorySpeed = 1;
    public float energyFarmSpeed = 1;
}
