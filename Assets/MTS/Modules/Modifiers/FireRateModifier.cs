using MTS.Data;
using MTS.Modules.Core;
using UnityEngine;

namespace MTS.Modules.Modifiers
{
    [CreateAssetMenu(menuName = "Towers/Modules/Modifiers/FireRateModifier")]
    public class FireRateModifier : ModifierModule
    {
        public float fireRate = 1.5f;

        public override void Install(TowerContext context)
        {
            base.Install(context);
            Context.StatManager.AddModifier("FireRate", v => v * fireRate);
        }

        public override void Uninstall(TowerContext context)
        {
            Context.StatManager.RemoveModifier("FireRate", v => v * fireRate);
            base.Uninstall(context);
        }
    }
}
