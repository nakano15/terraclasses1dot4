using Microsoft.Xna.Framework;

namespace terraclasses.Classes.Aspect
{
    public class CerberusClass : ClassBase
    {
        public override string Name => "Cerberus";
        public override string Description => "Ditch your real form to earn aspect of the guardian of the underworld.";
        public override ClassTypes ClassType => ClassTypes.Aspect;
        public override byte MaxLevel => 50;
        protected override Color SkillColor => new Color(92, 23, 12);
        protected override SkillUnlockInfo[] SetSkills => new SkillUnlockInfo[]
        {
            new SkillUnlockInfo(SkillDB.CerberusForm),
            new SkillUnlockInfo(SkillDB.CerberusHead)
        };
    }
}