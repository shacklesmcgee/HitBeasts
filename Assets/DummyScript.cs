using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    public int getHealth()
    {
        return health;
    }

    //Used for both damaging the player and healing the player
    public void modifyHealth(int healthChange, bool isDamage)
    {
        if(isDamage == true)
        {
            health -= healthChange;
        }
        else
        {
            health += healthChange;
        }
    }

    public int getDefence()
    {
        return defence;
    }

    //returns first the lowest possible attack value, then the highest
    public (int, int) getAttack()
    {
        return (attackLower, attackUpper);
    }

    public int getHeal()
    {
        return heal;
    }

    public void changePoints(int pointChange, bool wonGame)
    {
        if(wonGame == true)
        {
            points += pointChange;
        }
        else
        {
            points -= pointChange;
        }
    }

    int health = 100;
    int defence = 5;
    int attackLower = 10;
    int attackUpper = 13;
    int heal = 20;

    int points = 100;
}
