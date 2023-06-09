using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    
    public static GameAssets i
    {
        get
        {
            if (_i == null)
            {
                _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            }
            return _i;
        }
    }
    
    public Transform pfDamagePopup;

    public GameObject Card1;
    public GameObject Card2;

    public GameObject SpeedParticle;
    public GameObject ShieldParticle;
    public GameObject HealParticle;
    public GameObject DamageParticle;
    
    public GameObject PointClickParticle;
    
}
