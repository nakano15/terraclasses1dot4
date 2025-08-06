using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace terraclasses1dot4.Classes.Aspect
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

        public override void GetClassIcon(out Texture2D Texture, out Rectangle Rect)
        {
            Texture = terraclasses.ClassIconsTexture.Value;
            Rect = new Rectangle(48 * 4, 0, 48, 48);
        }
    }
}