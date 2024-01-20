using Microsoft.Xna.Framework;

namespace terraclasses.Classes.Primary
{
    public class FighterClass : ClassBase
    {
        public override string Name => "Fighter";
        public override string Description => "A basic combatant class.";
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(204, 81, 23);
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;
    }
}