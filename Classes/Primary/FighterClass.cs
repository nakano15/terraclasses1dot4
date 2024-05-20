using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace terraclasses.Classes.Primary
{
    public class FighterClass : ClassBase
    {
        public override string Name => "Fighter";
        public override string Description => "A basic combat class. Versatile with Melee and Ranged combat.";
        public override byte MaxLevel => 50;
        public override ClassTypes ClassType => ClassTypes.Starter;
        protected override Color SkillColor => new Color(204, 81, 23);
        protected override SkillUnlockInfo[] SetSkills => new SkillUnlockInfo[]
        {
            new SkillUnlockInfo(SkillDB.SwordMastery)
        };
        public override void GetClassIcon(out Texture2D Texture, out Rectangle Rect)
        {
            Texture = terraclasses.ClassIconsTexture.Value;
            Rect = new Rectangle(48 * 5, 0, 48, 48);
        }
    }
}