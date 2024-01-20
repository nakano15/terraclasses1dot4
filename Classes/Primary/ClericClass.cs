using Microsoft.Xna.Framework;

namespace terraclasses.Classes.Primary
{
    public class ClericClass : ClassBase
    {
        public override string Name => "Cleric";
        public override string Description => "Devout persons who prays for the well being of their allies.";
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(0, 255, 243);
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;
    }
}