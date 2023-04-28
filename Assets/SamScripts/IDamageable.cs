using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int dmg, int count = 1, bool pierce = false, bool sharp = false, bool heavy = false, float crit = 0.01f);

    void Kill();
}
