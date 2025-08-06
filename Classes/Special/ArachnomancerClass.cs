using Microsoft.Xna.Framework;

namespace terraclasses1dot4.Classes.Special
{
    public class ArachnomancerClass : ClassBase
    {
        public override string Name => "Arachnomancer";
        public override string Description => "Masters of Spiders controlling.";
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(90, 13, 110);
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;
    }
}