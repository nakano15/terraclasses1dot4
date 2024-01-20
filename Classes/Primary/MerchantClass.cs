using Microsoft.Xna.Framework;

namespace terraclasses.Classes.Primary
{
    public class MerchantClass : ClassBase
    {
        public override string Name => "Merchant";
        public override string Description => "Uses their knowledge to increase gold earning.";
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(221, 212, 17);
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;
    }
}