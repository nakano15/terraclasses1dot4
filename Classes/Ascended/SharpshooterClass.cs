using Microsoft.Xna.Framework;

namespace terraclasses1dot4.Classes.Aspect
{
    public class SharpshooterClass : ClassBase
    {
        public override string Name => "Sharpshooter";
        public override string Description => "Experienced in guns usage.";
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(57, 71, 77);
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;
    }
}