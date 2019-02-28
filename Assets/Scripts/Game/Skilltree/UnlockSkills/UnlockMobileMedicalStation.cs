using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockMobileMedicalStation : UnlockSkill
{
    public override void Effect()
    {
        Unlock(ref SkilltreeManager.manager.mobileMedicalStation);
    }
}
