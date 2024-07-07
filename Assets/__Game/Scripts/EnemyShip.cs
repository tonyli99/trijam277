using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class EnemyShip : Ship
    {
        protected override void OnDied()
        {
            base.OnDied();
            Destroy(gameObject);
        }
    }

}
