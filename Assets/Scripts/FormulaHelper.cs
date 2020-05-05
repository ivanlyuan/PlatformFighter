using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormulaHelper
{
    static public Vector2 GetKnockBack(Hitbox hitbox, Character target,Vector2 DI)
    {
        Vector2 oriDir = (((hitbox.hitboxData.kbAngle.normalized) + (DI * hitbox.hitboxData.sdiFactor)).normalized);
        Vector2 diDir = (DI * hitbox.hitboxData.sdiFactor);
        float power = (hitbox.hitboxData.bkb + hitbox.hitboxData.kbg * target.charState.damagePercentage / 100);

        if (!hitbox.parentChar.charState.isFacingRight)
        {
            oriDir.x = -oriDir.x;
        }

        return (oriDir + diDir) * power;
        
    }

    static public float GetHitstunDuration(Hitbox hitbox,Character character)
    {
        return (hitbox.hitboxData.bkb + hitbox.hitboxData.kbg * character.charState.damagePercentage / 100) * 0.0012f;
    }

    static public int GetFramesOfHitLag(HitboxData hitboxData)
    {
        int result = (int)hitboxData.damage / 8;
        if (result >= 10)
        {
            return 10;
        }
        else if (result <= 3)
        {
            return 3;
        }
        return result;
    }
	
}
