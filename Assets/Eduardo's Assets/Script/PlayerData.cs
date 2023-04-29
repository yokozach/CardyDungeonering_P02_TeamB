using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static bool shareData = false;

    public static int curHP { get; set; }
    public static int curShield { get; set; }
    public static int maxHP { get; set; }
    public static int maxShield { get; set; }
    public static int baseAttack { get; set; }
    public static int baseDefense { get; set; }
    public static int numberOfAttacks { get; set; }
    public static bool pierce { get; set; }
    public static bool sharp { get; set; }
    public static bool heavy { get; set; }
    public static float baseCritChance { get; set; }
    public static List<int> deckNumInv { get; set; }

    public static void StoreData(int hp, int sh, int mhp, int msh, int att, int def, int num, float C, bool P, bool S, bool H, List<GameObject> curInv)
    {
        curHP = hp;
        curShield = sh;
        maxHP = mhp;
        maxShield = msh;
        baseAttack = att;
        baseDefense = def;
        numberOfAttacks = num;
        baseCritChance = C;
        pierce = P;
        sharp = S;
        heavy = H;
        deckNumInv = new List<int>(curInv.Count);
        foreach (GameObject cardPrefab in curInv)
        {
            deckNumInv.Add(cardPrefab.GetComponent<InvCard>().deckNum);
        }
        shareData = true;
    }


}
