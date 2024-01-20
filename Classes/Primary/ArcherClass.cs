using Microsoft.Xna.Framework;

namespace terraclasses.Classes.Primary
{
    public class ArcherClass : ClassBase
    {
        public override string Name => "Archer";
        public override string Description => "Experienced with Bow and Arrow usage.";
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(40, 168, 12);
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;
    }
}