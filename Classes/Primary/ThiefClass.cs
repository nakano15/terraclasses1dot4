using Microsoft.Xna.Framework;

namespace terraclasses.Classes.Primary
{
    public class ThiefClass : ClassBase
    {
        public override string Name => "Thief";
        public override string Description => "Sneaky characters who tend to avoid attention to subtract things from others.";
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(83, 38, 162);
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;
    }
}