using terraclasses1dot4.Skills;

namespace terraclasses1dot4
{
    public class SkillDB : SkillContainer
    {
        public const uint CerberusForm = 1,
            CerberusHead = 2,
            SwordMastery = 3,
            Endure = 4,
            Cleave = 5,
            FirstAid = 6,
            ThrowRock = 7,
            Fireball = 8,
            MagicMastery = 9;

        protected override void LoadSkills()
        {
            AddSkill(CerberusForm, new Skills.Cerberus.CerberusFormSkill());
            AddSkill(CerberusHead, new Skills.Cerberus.CerberusHeadSkill());
            AddSkill(SwordMastery, new Skills.Fighter.SwordMastery());
            AddSkill(Endure, new Skills.Fighter.Endure());
            AddSkill(Cleave, new Skills.Fighter.Cleave());
            AddSkill(FirstAid, new Skills.Terrarian.FirstAid());
            AddSkill(ThrowRock, new Skills.Terrarian.ThrowRock());
            AddSkill(Fireball, new Skills.Mage.Fireball());
            AddSkill(MagicMastery, new Skills.Mage.MagicMastery());
        }
    }
}