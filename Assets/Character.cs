using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string characterName = "blank";
    private int maxHealth = 100;
    private int currentHealth = 100;
    private int defenceLower = 12;
    private int defenceUpper = 14;
    private int attackLower = 15;
    private int attackUpper = 19;
    private int specialLower = 17;
    private int specialUpper = 18;
    private int heal = 20;
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

    //Used for both damaging the player and healing the player
    public void setCurrentHealth(int change, bool damage)
    {
        if (damage)
        {
            currentHealth -= change;
        }

        else
        {
            currentHealth += change;
        }

        print(this.characterName + " : " + currentHealth);
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

    public int getHeal()
    {
        return heal;
    }

    public void setHeal(int change)
    {
        heal += change;

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
