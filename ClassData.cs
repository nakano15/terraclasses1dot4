using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace terraclasses
{
    public class ClassData
    {
        ClassBase _Base = null;
        public ClassBase Base { get 
        {
            if (_Base == null)
                _Base = ClassContainer.GetClass(ID, ModID);
            return _Base;
        }}
        public uint GetID { get { return ID; } }
        public string GetModID { get { return ModID; } }
        public ClassID GetClassID => new ClassID(ID, ModID);
        public string Name => Base.Name;
        public int MaxLevel => Base.MaxLevel;
        public bool IsMastered => Level >= MaxLevel;
        public bool IsClassChangeLevel => Level >= Base.ClassChangeLevel;
        uint ID = 0;
        string ModID = "";
        byte Level = 0;
        int Exp = 0, MaxExp = 0;
        int SkillPoint = 0;
        bool Unlocked = false;
        SkillData[] Skills = new SkillData[0];
        public byte GetLevel => Level;
        public int GetExp => Exp;
        public int GetMaxExp => MaxExp;
        public SkillData[] GetSkills => Skills;
        public bool IsUnlocked => Unlocked;
        public int GetSkillPoints => SkillPoint;

        public void ChangeSkillPoints(int Change)
        {
            SkillPoint = (int)System.MathF.Max(0, SkillPoint + Change);
        }

        public void ChangeClass(uint ID, string ModID = "")
        {
            if(ModID == "")
                ModID = terraclasses.GetModName;
            if (!Main.gameMenu)
            {
                for (int s = 0; s < Skills.Length; s++)
                {
                    Skills[s].Base.OnEndUse(Skills[s], true);
                    Skills[s] = null;
                }
            }
            this.ModID = ModID;
            this.ID = ID;
            _Base = ClassContainer.GetClass(ID, ModID);
            List<SkillUnlockInfo> skills = Base.GetSkills;
            Skills = new SkillData[skills.Count];
            for (int i = 0; i < skills.Count; i++)
            {
                Skills[i] = SkillContainer.GetSkill(skills[i].GetID, skills[i].GetModID).GetSkillData;
                Skills[i].ChangeSkillIDs(skills[i].GetID, skills[i].GetModID);
                Skills[i].SetSkillInfosBasedOnUnlockInfo(skills[i]);
            }
            Unlocked = true;
            UpdateMaxExp();
        }

        internal void SetSkillCaster(Player player)
        {
            foreach (SkillData skill in Skills)
            {
                skill.ChangeCaster(player);
            }
        }

        public bool IsSameID(uint ID, string ModID = "")
        {
            if (ModID == "") ModID = terraclasses.GetModName;
            return Unlocked && this.ID == ID && this.ModID == ModID;
        }

        public void Update(PlayerMod player)
        {
            //Update what?
        }

        internal void TakeActiveSkills(List<SkillData> Skills, Player player)
        {
            foreach (SkillData sd in this.Skills)
            {
                if (sd.IsUnlocked)
                {
                    if (sd.SkillType == SkillTypes.Passive)
                    {
                        sd.UpdatePassiveSkill(player);
                    }
                    if (sd.IsActive)
                        Skills.Add(sd);
                }
            }
        }

        public bool HasSkill(uint ID, string ModID = "")
        {
            foreach (SkillData sd in Skills)
            {
                if (sd.IsSameID(ID, ModID)) return true;
            }
            return false;
        }

        public bool SpendSkillPointOnSkillAttribute(int SkillIndex, int AttributeIndex)
        {
            if (SkillPoint <= 0 || SkillIndex < 0 || SkillIndex >= Skills.Length) return false;
            SkillData skill = Skills[SkillIndex];
            if (AttributeIndex < 0 || AttributeIndex >= skill.Base.GetSkillAttributes.Length) return false;
            int PointsToUse = skill.Base.GetSkillAttributes[AttributeIndex].PointsUsed;
            if (SkillPoint < PointsToUse) return false;
            skill.ChangeAttributeLevel(AttributeIndex, 1);
            skill.UpdateSkillUnlockedState();
            ChangeSkillPoints(-PointsToUse);
            return true;
        }

        public SkillData GetSkillOrNull(uint ID, string ModID = "")
        {
            foreach (SkillData sd in Skills)
            {
                if (sd.IsSameID(ID, ModID)) return sd;
            }
            return null;
        }

        public bool GetCP(int CP)
        {
            Exp += CP;
            bool LeveledUp = false;
            while (Exp >= MaxExp && Level < MaxLevel)
            {
                Exp -= MaxExp;
                Level++;
                LeveledUp = true;
                SkillPoint++;
                UpdateMaxExp();
            }
            return LeveledUp;
        }

        public void UpdateMaxExp()
        {
            MaxExp = Base.GetMaxExp(Level);
        }

        public string GetLevelString(bool MaxLevelToo = false)
        {
            if (Level < MaxLevel)
            {
                return "Level [" + GetLevel + (MaxLevelToo ? "/" + MaxLevel : "") + "]";
            }
            return "[Mastered]";
        }

        public string GetExpString()
        {
            if (Level < MaxLevel)
            {
                return "Exp ["+GetExp+"/" + GetMaxExp +"]";
            }
            return "Exp ["+GetMaxExp+"]";
        }
    }
}