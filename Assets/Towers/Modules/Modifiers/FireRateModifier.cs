using Towers.Core;
using Towers.Modules.Core;
using UnityEngine;

namespace Towers.Modules.Modifiers
{
    [CreateAssetMenu(menuName = "Towers/Modules/Modifiers/FireRateModifier")]
    public class FireRateModifier : ModifierModule
    {
        public float fireRateMultiplier = 1.5f;

        public override void Install(TowerContext context)
        {
            base.Install(context);
            //Context.Stats.AddMultiplier("FireRateMult", fireRateMultiplier);
        }

        public override void Uninstall(TowerContext context)
        {
            //Context.Stats.RemoveMultiplier("FireRateMult", fireRateMultiplier);
            base.Uninstall(context);
        }
    }
}
