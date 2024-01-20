namespace terraclasses
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

        public SkillSlot(int NewSlot, int ClassIndex)
        {
            Slot = NewSlot;
            this.ClassIndex = ClassIndex;
        }
    }
}