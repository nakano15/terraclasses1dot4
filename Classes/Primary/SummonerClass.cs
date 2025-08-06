using Microsoft.Xna.Framework;

namespace terraclasses1dot4.Classes.Primary
{
    public class SummonerClass : ClassBase
    {
        public override string Name => "Summoner";
        public override string Description => "Able to invoke creatures to aid them.";
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(43, 138, 179);
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;
    }
}