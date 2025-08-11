using Microsoft.Xna.Framework;

namespace terraclasses1dot4.Classes.Primary
{
    public class MageClass : ClassBase
    {
        public override string Name => "Magician";
        public override string Description => "Apprentices in magic usage. The lack in spell channeling speed is compensated by high damage.";
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(15, 46, 153);
        protected override SkillUnlockInfo[] SetSkills => new SkillUnlockInfo[]
        {
            new SkillUnlockInfo(SkillDB.MagicMastery),
            new SkillUnlockInfo(SkillDB.Fireball)
        };
    }
}