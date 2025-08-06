using Terraria;
using Terraria.ModLoader;

namespace terraclasses1dot4
{
    public class SkillAttribute
    {
        public virtual string Name => "Unnamed";
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