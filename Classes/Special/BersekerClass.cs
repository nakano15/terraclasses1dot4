using Microsoft.Xna.Framework;

namespace terraclasses.Classes.Special
{
    public class BersekerClass : ClassBase
    {
        public override string Name => "Berseker";
        public override string Description => "Extremely aggressive class which disregards defense in combat.";
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(194, 33, 8);
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;
    }
}