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
        public virtual byte ClassChangeLevel
        {
            get
            {
                return (byte)System.MathF.Min(System.MathF.Max(10, MaxLevel - 10), MaxLevel);
            }
        }
        protected virtual SkillUnlockInfo[] SetSkills => new SkillUnlockInfo[0];
        public List<SkillUnlockInfo> GetSkills => _Skills; //Should let people also alter skills class has, which is good.
        List<SkillUnlockInfo> _Skills = new List<SkillUnlockInfo>();
        protected virtual Color SkillColor => Color.Purple;

        bool InvalidClass = false;
        public bool IsInvalidClass => InvalidClass;

        public virtual void GetClassIcon(out Texture2D Texture, out Rectangle Rect)
        {
            Texture = terraclasses.ClassIconsTexture.Value;
            Rect = new Rectangle(0, 0, 48, 48);
        }

        public virtual int GetMaxExp(int Level)
        {
            return GetDefaultMaxExp((byte)ClassType, Level);
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

        public int GetDefaultMaxExp(byte ClassGrade, int Level)
        {
            switch (ClassGrade)
            {
                default:
                    return 100 * ((Level + 1) * (Level + 1));
                case 0:
                    {
                        const int a = 5, b = 3, c = 10;
                        return a * Level + b * (Level * Level) + c;
                    }
                case 1:
                    {
                        const int a = 12, b = 8, c = 30;
                        return a * Level + b * (Level * Level) + c;
                    }
                case 2:
                    {
                        const int a = 21, b = 15, c = 50;
                        return a * Level + b * (Level * Level) + c;
                    }
                case 3:
                    {
                        const int a = 28, b = 22, c = 100;
                        return a * Level + b * (Level * Level) + c;
                    }
            }
        }
    }
}