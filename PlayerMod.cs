using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using System;

namespace terraclasses1dot4
{
    public class PlayerMod : ModPlayer
    {
        protected override bool CloneNewInstances => false;
        public override bool IsCloneable => false;
        ClassData[] Classes = new ClassData[MaxClasses];
        public const int MaxClasses = 5;
        List<SkillData> ActiveSkills = new List<SkillData>();
        public List<SkillData> GetActiveSkills => ActiveSkills;
        SkillSlot[] ActiveSkillSlots = new SkillSlot[0];
        SkillSlot CombatSkillSlot = new SkillSlot();
        public const int MaxActiveSkillSlots = 4;

        internal ClassData[] GetClasses => Classes;

        public PlayerMod()
        {
            for (int i = 0; i < MaxClasses; i++)
            {
                Classes[i] = new ClassData();
            }
            ActiveSkillSlots = new SkillSlot[4];
            for(int i = 0; i < ActiveSkillSlots.Length; i++)
            {
                ActiveSkillSlots[i] = new SkillSlot();
            }
        }

        public override void Initialize()
        {
            Classes[0].ChangeClass(0);
        }

        public override void PreUpdate()
        {
            UpdateActiveSkillsList();
        }

        void UpdateActiveSkillsList()
        {
            ActiveSkills.Clear();
            foreach (ClassData cd in Classes)
            {
                if (cd.IsUnlocked)
                    cd.TakeActiveSkills(ActiveSkills, Player);
            }
        }

        public override void OnEnterWorld()
        {
            foreach (ClassData cd in Classes)
            {
                foreach(SkillData sd in cd.GetSkills)
                {
                    sd.UpdateSkillUnlockedState();
                }
            }
            //Classes[1].ChangeClass(ClassDB.Fighter);
        }

        public ClassID[] GetUnlockedClassIDs()
        {
            List<ClassID> Ids = new List<ClassID>();
            foreach (ClassData cd in Classes)
            {
                if (cd.IsUnlocked)
                {
                    Ids.Add(cd.GetClassID);
                }
            }
            return Ids.ToArray();
        }

        internal static void AddSkillToActiveSkills(Player player, SkillData sd)
        {
            player.GetModPlayer<PlayerMod>().ActiveSkills.Add(sd);
        }

        public override void PostUpdateMiscEffects()
        {
            for (int i = 0; i < MaxClasses; i++)
            {
                Classes[i].Update(this);
            }
            foreach (SkillData sd in ActiveSkills)
            {
                sd.UpdateStatus();
            }
        }

        public override void SetControls()
        {
            for (int i = 0; i < 4; i++)
            {
                if (terraclasses.SkillSlot[i].JustPressed)
                {
                    SkillData sd = GetSkillFromSlot(ActiveSkillSlots[i]);
                    if (sd != null)
                    {
                        sd.UseSkill(Player);
                    }
                }
            }
        }

        public bool SetSkillToSlot(int Index, int ClassPosition, int SkillPosition)
        {
            if (Index >= 0 && Index < 5)
            {
                if (ClassPosition >= 0 && SkillPosition >= 0 && ClassPosition < MaxClasses && Classes[ClassPosition].IsUnlocked && SkillPosition < Classes[ClassPosition].GetSkills.Length)
                {
                    if (Index == 4)
                    {
                        CombatSkillSlot = new SkillSlot(ClassPosition, SkillPosition);
                    }
                    else
                    {
                        ActiveSkillSlots[Index] = new SkillSlot(ClassPosition, SkillPosition);
                    }
                }
            }
            return false;
        }

        public SkillData GetSkillFromSlot(int Index)
        {
            SkillSlot? Slot = GetSlotFromIndex(Index);
            if (Slot.HasValue)
            {
                return GetSkillFromSlot(Slot.Value);
            }
            return null;
        }

        public SkillData GetSkillFromSlot(SkillSlot slot)
        {
            if (slot.Slot > -1 && slot.ClassIndex > -1 && slot.ClassIndex < Classes.Length && Classes[slot.ClassIndex].IsUnlocked)
            {
                if (slot.Slot < Classes[slot.ClassIndex].GetSkills.Length)
                {
                    return Classes[slot.ClassIndex].GetSkills[slot.Slot];
                }
            }
            return null;
        }

        public SkillSlot? GetSlotFromIndex(int Index)
        {
            if (Index == 4)
            {
                return CombatSkillSlot;
            }
            else if (Index < 4)
                return ActiveSkillSlots[Index];
            return null;
        }

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            bool AllowFiring = true;
            foreach (SkillData sd in ActiveSkills)
            {
                if (!sd.BeforeShooting(item, ref type, ref damage, ref knockback, ref position, ref velocity))
                {
                    AllowFiring = false;
                }
            }
            return AllowFiring;
        }

        public override void PostItemCheck()
        {
            bool JustUsed = Player.itemAnimation > 0 && Player.itemAnimation == Player.itemAnimationMax - 1;
            foreach (SkillData sd in ActiveSkills)
            {
                sd.UpdateItemUse(JustUsed);
            }
        }

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            foreach (SkillData sd in ActiveSkills)
            {
                sd.UpdateAnimation();
            }
            ClassData cd = GetClassDataOrNull(ClassDB.Cerberus);
            {
                if (HasSkill(SkillDB.CerberusForm))
                {
                    Skills.Cerberus.CerberusFormSkill.DarkenSkin(ref drawInfo.colorBodySkin);
                }
            }
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            foreach (SkillData sd in ActiveSkills)
            {
                sd.OnHitByNPC(npc, hurtInfo);
            }
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            foreach (SkillData sd in ActiveSkills)
            {
                sd.OnHitByProjectile(proj, hurtInfo);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            foreach (SkillData sd in ActiveSkills)
            {
                sd.OnHitNpc(target, hit, damageDone);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            foreach (SkillData sd in ActiveSkills)
            {
                sd.OnHitNPCWithProj(proj, target, hit, damageDone);
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            foreach (SkillData sd in ActiveSkills)
            {
                sd.ModifyHitNPC(target, ref modifiers);
            }
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            foreach (SkillData sd in ActiveSkills)
            {
                sd.ModifyHitNPCWithItem(item, target, ref modifiers);
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            foreach (SkillData sd in ActiveSkills)
            {
                sd.ModifyHitNPCWithProj(proj, target, ref modifiers);
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            foreach (SkillData sd in ActiveSkills)
            {
                sd.ModifyHitByNPC(npc, ref modifiers);
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            foreach (SkillData sd in ActiveSkills)
            {
                sd.ModifyHitByProjectile(proj, ref modifiers);
            }
        }

        public override void PostUpdate()
        {
            foreach (SkillData sd in ActiveSkills)
            {
                sd.UpdateSkill(Player);
            }
        }

        public static bool PlayerHasClass(Player player, uint ID, string ModID = "")
        {
            return player.GetModPlayer<PlayerMod>().HasClass(ID, ModID);
        }

        public bool HasClass(uint ID, string ModID = "")
        {
            foreach (ClassData cd in Classes)
            {
                if(cd.IsSameID(ID, ModID)) return true;
            }
            return false;
        }

        public static bool PlayerHasSkill(Player player, uint ID, string ModID = "")
        {
            return player.GetModPlayer<PlayerMod>().HasSkill(ID, ModID);
        }

        public bool HasSkill(uint ID, string ModID = "")
        {
            foreach (ClassData cd in Classes)
            {
                if (cd.HasSkill(ID, ModID)) return true;
            }
            return false;
        }

        public static SkillData PlayerGetSkillOrNull(Player player, uint ID, string ModID = "")
        {
            return player.GetModPlayer<PlayerMod>().GetSkillOrNull(ID, ModID);
        }

        public SkillData GetSkillOrNull(uint ID, string ModID = "")
        {
            foreach (ClassData cd in Classes)
            {
                SkillData sd = cd.GetSkillOrNull(ID, ModID);
                if (sd != null) return sd;
            }
            return null;
        }

        public ClassData GetClassDataOrNull(uint ID, string ModID = "")
        {
            foreach (ClassData cd in Classes)
            {
                if(cd.IsSameID(ID, ModID)) return cd;
            }
            return null;
        }

        public static bool PlayerAddClass(Player player, uint ID, string ModID = "")
        {
            return player.GetModPlayer<PlayerMod>().AddClass(ID, ModID);
        }

        public static void GiveCP(Player player, int CP, Rectangle? sourceRect = null)
        {
            PlayerMod pm = player.GetModPlayer<PlayerMod>();
            ClassData cd = pm.GetLastClass;
            bool LevelUp = cd.GetCP(CP);
            if (player.whoAmI == Main.myPlayer)
            {
                if (!sourceRect.HasValue)
                    sourceRect = player.Hitbox;
                if(LevelUp)
                {
                    CombatText.NewText(sourceRect.Value, Color.MediumPurple, "Class Level Up!", true);
                }
                else
                {
                    if (terraclasses.ShowExpRewardAsPercentage)
                    {
                        float Percentage = (float)Math.Round((float)CP * 100 / cd.GetMaxExp, 2);
                        CombatText.NewText(sourceRect.Value, Color.MediumPurple, Percentage + "% CP", true);
                    }
                    else
                    {
                        CombatText.NewText(sourceRect.Value, Color.MediumPurple, CP + " CP", true);
                    }
                }
            }
        }

        public bool AddClass(uint ID, string ModID = "")
        {
            if (!HasClass(ID, ModID))
            {
                if (ModID == "") ModID = terraclasses.GetModName;
                foreach(ClassData cd in Classes)
                {
                    if (!cd.IsUnlocked)
                    {
                        cd.ChangeClass(ID, ModID);
                        cd.SetSkillCaster(Player);
                        return true;
                    }
                }
            }
            return false;
        }

        public ClassData GetLastClass
        {
            get
            {
                ClassData Last = Classes[0];
                for (int i = 1; i < MaxClasses; i++)
                {
                    if (Classes[i].IsUnlocked)
                        Last = Classes[i];
                }
                return Last;
            }
        }
    }
}