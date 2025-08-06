using Microsoft.Xna.Framework;

namespace terraclasses1dot4.Classes.Special
{
    public class HopliteClass : ClassBase
    {
        public override string Name => "Hoplite";
        public override string Description => "You are not entertained.";
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(213, 176, 14);
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;
    }
}