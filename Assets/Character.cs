using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string characterName = "";
    private int maxHealth = 5;
    private int currentHealth = 5;
    private int defenceLower = 2;
    private int defenceUpper = 8;
    private int attackLower = 1;
    private int attackUpper = 5;
    private int specialLower = 4;
    private int specialUpper = 7;
    private int healLower = 5;
    private int healUpper = 10;
    private int luck = 17;

    private int attackLvl = 0;
    private int defenceLvl = 0;
    private int maxHealthLvl = 0;
    private int specialLvl = 0;
    private int luckLvl = 0;

    private int points = 3;
    private int betPoints = 0;

    public string getCharacterName()
    {
        return characterName;
    }

    public void setCharacterName(string newName)
    {
        characterName = newName;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void setMaxHealth(int change)
    {
        maxHealth += change;

        points -= change;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void setCurrentHealth(int value)
    {
        currentHealth = value;
    }

    //Used for both damaging the player and healing the player
    public void changeCurrentHealth(int change, bool damage)
    {
        if (damage)
        {
            currentHealth -= change;
        }

        else
        {
            currentHealth += change;
        }
    }

    //returns first the lowest possible attack value, then the highest
    public (int, int) getDefence()
    {
        return (defenceLower, defenceUpper);
    }

    public void setDefence(int change)
    {
        defenceLower += change;
        defenceUpper += change;

        points -= change;

    }
    //returns first the lowest possible attack value, then the highest
    public (int, int) getAttack()
    {
        return (attackLower, attackUpper);
    }

    public void setAttack(int change)
    {
        attackLower += change;
        attackUpper += change;

        points -= change;
    }

    //returns first the lowest possible attack value, then the highest
    public (int, int) getSpecial()
    {
        return (specialLower, specialUpper);
    }

    public void setSpecial(int change)
    {
        specialLower += change;
        specialUpper += change;

        points -= change;
    }

    public (int, int) getHeal()
    {
        return (healLower, healUpper);
    }

    public void setHeal(int change)
    {
        healLower += change;
        healUpper += change;

        points -= change;
    }

    public int getLuck()
    {
        return luck;
    }

    public void setLuck(int change)
    {
        luck += change;

        points -= change;
    }

    public int getAttackLvl()
    {
        return attackLvl;
    }

    public void setAttackLvl(int change)
    {
        attackLvl += change;
    }

    public int getDefenceLvl()
    {
        return defenceLvl;
    }

    public void setDefenceLvl(int change)
    {
        defenceLvl += change;
    }

    public int getMaxHealthLvl()
    {
        return maxHealthLvl;
    }

    public void setMaxHealthLvl(int change)
    {
        maxHealthLvl += change;
    }

    public int getSpecialLvl()
    {
        return specialLvl;
    }

    public void setSpecialLvl(int change)
    {
        specialLvl += change;
    }

    public int getLuckLvl()
    {
        return luckLvl;
    }

    public void setLuckLvl(int change)
    {
        luckLvl += change;
    }

    public int getPoints()
    {
        return points;
    }

    public void setPoints(int change)
    {
        points += change;
    }

    public int getBetPoints()
    {
        return betPoints;
    }

    public void setBetPoints(int change)
    {
        betPoints += change;
    }

}
