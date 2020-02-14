using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    private int health = 100;
    private int defenceLower = 12;
    private int defenceUpper = 14;
    private int attackLower = 15;
    private int attackUpper = 19;
    private int specialLower = 17;
    private int specialUpper = 18;
    private int heal = 20;
    private int luck = 17;

    private int points = 3;

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int change)
    {
        health = health + change;
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

    //returns first the lowest possible attack value, then the highest
    public (int, int) getDefence()
    {
        return (defenceLower, defenceUpper);
    }

    public void setDefence(int change)
    {
        defenceLower = defenceLower + change;
        defenceUpper = defenceUpper + change;

    }
    //returns first the lowest possible attack value, then the highest
    public (int, int) getAttack()
    {
        return (attackLower, attackUpper);
    }

    public void setAttack(int change)
    {
        attackLower = attackLower + change;
        attackUpper = attackUpper + change;
    }

    //returns first the lowest possible attack value, then the highest
    public (int, int) getSpecial()
    {
        return (specialLower, specialUpper);
    }

    public void setSpecial(int change)
    {
        specialLower = specialLower + change;
        specialUpper = specialUpper + change;
    }

    public int getHeal()
    {
        return heal;
    }

    public void setHeal(int change)
    {
        heal = heal + change;
    }

    public int getLuck()
    {
        return luck;
    }

    public void setLuck(int change)
    {
        luck = luck + change;
    }

    public void changePoints(int pointChange, bool wonGame, bool betting)
    {
        if(wonGame || betting)
        {
            points += pointChange;
        }

        else
        {
            points -= pointChange;
        }
    }

}
