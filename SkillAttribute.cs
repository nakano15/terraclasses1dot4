using Terraria;
using Terraria.ModLoader;

namespace terraclasses
{
    public class SkillAttribute
    {
        public virtual string AttributeDescription(int Level)
        {
            return "Does something.";
        }
        public virtual int MaxLevel => 5;
        public virtual int PointsUsed => 1;

        public virtual float Value(int Level)
        {
            return 0;
        }
    }
}