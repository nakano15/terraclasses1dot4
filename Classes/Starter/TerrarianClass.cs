using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace terraclasses.Classes.Starter
{
    public class TerrarianClass : ClassBase
    {
        public override string Name => "Terrarian";
        public override string Description => "";
        public override ClassTypes ClassType => ClassTypes.Starter;
        public override byte MaxLevel => 10;
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;

        public override void GetClassIcon(out Texture2D Texture, out Rectangle Rect)
        {
            Texture = terraclasses.ClassIconsTexture.Value;
            Rect = new Rectangle(48 * 3, 0, 48, 48);
        }
    }
}