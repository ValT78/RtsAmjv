using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopBot : BotBase
{

    private void Update()
    {
        UpdateBehavior();

    }
    public override void UpdateBehavior()
    {
        base.UpdateBehavior();
        // Logique spécifique aux troupes, si nécessaire.
    }

}
