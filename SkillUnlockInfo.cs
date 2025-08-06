namespace terraclasses1dot4
{
    public struct SkillUnlockInfo
    {
        uint ID;
        string ModID;
        public uint GetID => ID;
        public string GetModID => ModID;

        public SkillUnlockInfo(uint ID, string ModID = "")
        {
            if (string.IsNullOrEmpty(ModID))
            {
                ModID = terraclasses.GetModName;
            }
            this.ID = ID;
            this.ModID = ModID;
        }
    }
}