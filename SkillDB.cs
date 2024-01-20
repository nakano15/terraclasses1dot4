using terraclasses.Skills;

namespace terraclasses
{
    public class SkillDB : SkillContainer
    {
        public const uint CerberusForm = 1,
            CerberusHead = 2;
            
        protected override void LoadSkills()
        {
            AddSkill(CerberusForm, new Skills.Cerberus.CerberusFormSkill());
            AddSkill(CerberusHead, new Skills.Cerberus.CerberusHeadSkill());
        }
    }
}