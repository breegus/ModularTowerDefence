using MTS.Data;
using MTS.Modules.Core;
using UnityEngine;

namespace MTS.Modules.Modifiers
{
    [CreateAssetMenu(menuName = "Towers/Modules/Modifiers/DamageModifier")]
    public class DamageModifier : ModifierModule
    {
        public float damage = 5f;

        public override void Install(TowerContext context)
        {
            base.Install(context);
            Context.StatManager.AddModifier("Damage", v => v + damage);
        }

        public override void Uninstall(TowerContext context)
        {
            Context.StatManager.RemoveModifier("Damage", v => v + damage);
            base.Uninstall(context);
        }
    }
}
