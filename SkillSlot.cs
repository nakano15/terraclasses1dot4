using System;

namespace terraclasses1dot4
{
    public struct SkillSlot
    {
        public int Slot;
        public int ClassIndex;

        public SkillSlot()
        {
            Slot = -1;
            ClassIndex = -1;
        }

        public SkillSlot(int ClassIndex, int SkillSlot)
        {
            Slot = SkillSlot;
            this.ClassIndex = ClassIndex;
        }

        public static bool IsSlotForSkill(SkillData skill, int Index)
        {
            switch (skill.SkillType)
            {
                case SkillTypes.Active:
                case SkillTypes.Toggle:
                    return Index < 4;
                case SkillTypes.Combat:
                    return Index == 4;
            }
            return false;
        }
    }
}