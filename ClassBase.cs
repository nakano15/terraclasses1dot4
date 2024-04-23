using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace terraclasses
{
    public class ClassBase
    {
        public virtual string Name => "Unknown";
        public virtual string Description => "";
        public virtual ClassTypes ClassType => ClassTypes.Normal;
        public virtual byte MaxLevel => 50;
        protected virtual SkillUnlockInfo[] SetSkills => new SkillUnlockInfo[0];
        public List<SkillUnlockInfo> GetSkills => _Skills; //Should let people also alter skills class has, which is good.
        List<SkillUnlockInfo> _Skills = new List<SkillUnlockInfo>();
        protected virtual Microsoft.Xna.Framework.Color SkillColor => Microsoft.Xna.Framework.Color.Purple;

        bool InvalidClass = false;
        public bool IsInvalidClass => InvalidClass;

        public virtual void GetClassIcon(out Texture2D Texture, out Rectangle Rect)
        {
            Texture = terraclasses.ClassIconsTexture.Value;
            Rect = new Rectangle(0, 0, 48, 48);
        }

        public virtual int GetMaxExp(int Level)
        {
            return 100 * ((Level + 1) * (Level + 1));
        }

        internal void Initialize()
        {
            _Skills.AddRange(SetSkills);
        }

        internal ClassBase SetInvalidClass()
        {
            InvalidClass = true;
            return this;
        }
    }
}