using Terraria.ModLoader;
using System.Collections.Generic;

namespace terraclasses
{
    public class SkillContainer
    {
        static Dictionary<string, SkillContainer> SkillContainers = new Dictionary<string, SkillContainer>();
        static SkillBase DefaultSkill = new SkillBase().SetInvalid();
        Dictionary<uint, SkillBase> SkillDictionary = new Dictionary<uint, SkillBase>();

        public static bool AddSkillContainer(Mod mod, SkillContainer container)
        {
            if (SkillContainers.ContainsKey(mod.Name)) return false;
            container.LoadSkills();
            SkillContainers.Add(mod.Name, container);
            return true;
        }

        public static SkillBase GetSkill(uint ID, string ModID = "")
        {
            if (ModID == "") ModID = terraclasses.GetModName;
            if (SkillContainers.ContainsKey(ModID))
            {
                if (SkillContainers[ModID].SkillDictionary.ContainsKey(ID))
                {
                    return SkillContainers[ModID].SkillDictionary[ID];
                }
            }
            return DefaultSkill;
        }

        protected virtual void LoadSkills()
        {

        }

        protected bool AddSkill(uint ID, SkillBase Skill)
        {
            if (SkillDictionary.ContainsKey(ID)) return false;
            SkillDictionary.Add(ID, Skill);
            Skill.Initialize();
            return true;
        }
        
        internal void OnUnload()
        {
            SkillDictionary.Clear();
            SkillDictionary = null;
        }

        internal static void Unload()
        {
            foreach (string s in SkillContainers.Keys)
            {
                SkillContainers[s].OnUnload();
            }
            SkillContainers.Clear();
            SkillContainers = null;
            DefaultSkill = null;
        }
    }
}