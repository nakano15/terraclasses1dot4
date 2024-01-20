using Microsoft.Xna.Framework;

namespace terraclasses.Classes.Aspect
{
    public class KnightClass : ClassBase
    {
        public override string Name => "Knight";
        public override string Description => "Valiant warriors who have sworn to protect someone.";
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(58, 131, 153);
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;
    }
}