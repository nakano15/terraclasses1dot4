namespace terraclasses.Classes.Starter
{
    public class TerrarianClass : ClassBase
    {
        public override string Name => "Terrarian";
        public override string Description => "";
        public override ClassTypes ClassType => ClassTypes.Starter;
        public override byte MaxLevel => 10;
        protected override SkillUnlockInfo[] SetSkills => base.SetSkills;
    }
}