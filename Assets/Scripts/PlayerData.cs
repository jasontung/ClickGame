using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PlayerData : ScriptableObject {
    public int lv;
    public int attack;
    public int exp;
    public int coin;
    public int stageIndex;

    private void Reset()
    {
        lv = 1;
        attack = 1;
    }
}
