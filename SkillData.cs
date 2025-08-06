using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Collections.ObjectModel;
using Terraria.DataStructures;

namespace terraclasses1dot4
{
    public class SkillData
    {
        SkillBase _Base = null;
        public SkillBase Base {
            get
            {
                if (_Base == null)
                    _Base = SkillContainer.GetSkill(ID, ModID);
                return _Base;
            }
        }
        uint ID = 0;
        string ModID = "";
        bool Unlocked = false;
        public uint GetID => ID;
        public string GetModID => ModID;
        int[] AttributeLevels = new int[0];
        bool Active = false;
        byte Step = 0;
        int Time = int.MinValue, StepStartTime = int.MinValue;
        Player Caster;
        public bool CheckIfSkillUnlocked { get
        {
            if (Base.EffectiveAtLevel0) return true;
            for (int i = 0; i < AttributeLevels.Length; i++)
            {
                if (AttributeLevels[i] > 0) return true;
            }
            return false;
        }}
        public string Name => Base.Name;
        public string Description => Base.Description;
        public SkillTypes SkillType => Base.SkillType;

        public bool IsActive => Active;
        public bool IsUnlocked => Unlocked;
        public byte GetStep => Step;
        public int GetStepTime => Time - StepStartTime;
        public int GetTime => Time;
        public bool StepStart => GetTime == StepStartTime;
        public Player GetCaster => Caster;
        
        Dictionary<int, int> NpcDamageCooldown = new Dictionary<int, int>(),
            PlayerDamageCooldown = new Dictionary<int, int>();
        List<int> NpcInteraction = new List<int>(),
            PlayerInteraction = new List<int>();

        internal void ChangeSkillIDs(uint ID, string ModID)
        {
            if (ModID == "") ModID = terraclasses.GetModName;
            this.ID = ID;
            this.ModID = ModID;
            AttributeLevels = new int[Base.GetSkillAttributes.Length];
        }

        internal void ChangeCaster(Player player)
        {
            Caster = player;
        }

        public int GetAttributeLevel(int Index)
        {
            if(Index >= AttributeLevels.Length || Index < 0) return 0;
            return AttributeLevels[Index];
        }

        public float GetSkillAttributeValue(byte Index)
        {
            if (Index >= AttributeLevels.Length) return 0;
            return Base.GetSkillAttributes[Index].Value(AttributeLevels[Index]);
        }

        public void ChangeAttributeLevel(int Index, int Increase)
        {
            if(Index >= AttributeLevels.Length || Index < 0) return;
            AttributeLevels[Index] += Increase;
        }

        internal void SetSkillInfosBasedOnUnlockInfo(SkillUnlockInfo info)
        {
            
        }

        public void UseSkill(Player player)
        {
            if(!CheckIfSkillUnlocked) return;
            SkillTypes SkillType = Base.SkillType;
            Caster = player;
            if (SkillType != SkillTypes.Active && SkillType != SkillTypes.Toggle)
                return;
            SkillBase.ChangeSkillData(this);
            if (SkillType == SkillTypes.Toggle && Active)
            {
                EndUse();
                return;
            }
            Time = StepStartTime = int.MinValue;
            Step = 0;
            NpcDamageCooldown.Clear();
            PlayerDamageCooldown.Clear();
            NpcInteraction.Clear();
            PlayerInteraction.Clear();
            Base.OnStartUse();
            SkillBase.ChangeSkillData(null);
            Active = true;
        }

        public void EndUse(bool DoCooldown = true)
        {
            SkillTypes SkillType = Base.SkillType;
            if (SkillType == SkillTypes.Passive || SkillType == SkillTypes.Combat) return;
            SkillBase.ChangeSkillData(this);
            Base.OnEndUse(false);
            SkillBase.ChangeSkillData(null);
            Active = false;
        }

        internal void UpdatePassiveSkill(Player player)
        {
            bool SetActive = IsUnlocked;
            if (SetActive != Active)
            {
                SkillBase.ChangeSkillData(this);
                if (!Active)
                {
                    Caster = player;
                    Base.OnStartUse();
                }
                else
                {
                    Base.OnEndUse(true);
                }
                SkillBase.ChangeSkillData(null);
            }
            Active = SetActive;
        }

        internal void UpdateAnimation()
        {
            SkillBase.ChangeSkillData(this);
            Base.UpdateAnimation();
            SkillBase.ChangeSkillData(null);
        }

        internal void UpdateStatus()
        {
            SkillBase.ChangeSkillData(this);
            Base.UpdateStatus();
            SkillBase.ChangeSkillData(null);
        }

        internal void UpdateItemUse(bool JustUsed)
        {
            SkillBase.ChangeSkillData(this);
            Base.UpdateItemUse(JustUsed);
            SkillBase.ChangeSkillData(null);
        }

        internal bool BeforeShooting(Item weapon, ref int type, ref int damage, ref float knockback, ref Microsoft.Xna.Framework.Vector2 Position, ref Microsoft.Xna.Framework.Vector2 Velocity)
        {
            SkillBase.ChangeSkillData(this);
            bool Shoot = Base.BeforeShooting(weapon, ref type, ref damage, ref knockback, ref Position, ref Velocity);
            SkillBase.ChangeSkillData(null);
            return Shoot;
        }

        internal void OnHitNpc(NPC Target, NPC.HitInfo Hit, int DamageDone)
        {
            SkillBase.ChangeSkillData(this);
            Base.OnHitNPC(Target, Hit, DamageDone);
            SkillBase.ChangeSkillData(null);
        }

        internal void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            SkillBase.ChangeSkillData(this);
            Base.OnHitNPCWithProj(proj, target, hit, damageDone);
            SkillBase.ChangeSkillData(null);
        }

        internal void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            SkillBase.ChangeSkillData(this);
            Base.OnHitByNPC(npc, hurtInfo);
            SkillBase.ChangeSkillData(null);
        }

        internal void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            SkillBase.ChangeSkillData(this);
            Base.OnHitByProjectile(proj, hurtInfo);
            SkillBase.ChangeSkillData(null);
        }

        internal void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            SkillBase.ChangeSkillData(this);
            Base.ModifyHitNPC(target, ref modifiers);
            SkillBase.ChangeSkillData(null);
        }

        internal void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            SkillBase.ChangeSkillData(this);
            Base.ModifyHitNPCWithItem(item, target, ref modifiers);
            SkillBase.ChangeSkillData(null);
        }

        internal void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            SkillBase.ChangeSkillData(this);
            Base.ModifyHitNPCWithProj(proj, target, ref modifiers);
            SkillBase.ChangeSkillData(null);
        }

        internal void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            SkillBase.ChangeSkillData(this);
            Base.ModifyHitByNPC(npc, ref modifiers);
            SkillBase.ChangeSkillData(null);
        }

        internal void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            SkillBase.ChangeSkillData(this);
            Base.ModifyHitByProjectile(proj, ref modifiers);
            SkillBase.ChangeSkillData(null);
        }

        internal void DrawBehindPlayer(ref PlayerDrawSet drawInfo)
        {
            SkillBase.ChangeSkillData(this);
            Base.DrawBehindPlayer(ref drawInfo);
            SkillBase.ChangeSkillData(null);
        }

        internal void DrawInFrontOfPlayer(ref PlayerDrawSet drawInfo)
        {
            SkillBase.ChangeSkillData(this);
            Base.DrawInFrontOfPlayer(ref drawInfo);
            SkillBase.ChangeSkillData(null);
        }

        internal void DrawInFrontOfEverything()
        {
            SkillBase.ChangeSkillData(this);
            Base.DrawInFrontOfEverything();
            SkillBase.ChangeSkillData(null);
        }

        public void UpdateSkill(Player player)
        {
            if (Active)
            {
                SkillBase.ChangeSkillData(this);
                Base.Update();
                Time++;
                UpdateCooldowns();
                SkillBase.ChangeSkillData(null);
            }
        }

        void UpdateCooldowns()
        {
            int[] keys = NpcDamageCooldown.Keys.ToArray();
            foreach (int k in keys)
            {
                NpcDamageCooldown[k]--;
                if (NpcDamageCooldown[k] <= 0)
                    NpcDamageCooldown.Remove(k);
            }
            keys = PlayerDamageCooldown.Keys.ToArray();
            foreach (int k in keys)
            {
                PlayerDamageCooldown[k]--;
                if (PlayerDamageCooldown[k] <= 0)
                    PlayerDamageCooldown.Remove(k);
            }
        }

        public void UpdateSkillUnlockedState()
        {
            Unlocked = CheckIfSkillUnlocked;
        }

        public bool IsSameID(uint ID, string ModID = "")
        {
            if (ModID == "") ModID = terraclasses.GetModName;
            return this.ID == ID && this.ModID == ModID;
        }

        public void ChangeStep()
        {
            ChangeStep((byte)(GetStep + 1));
        }

        public void ChangeStep(byte NewStepID)
        {
            Step = NewStepID;
            StepStartTime = Time + 1;
            SkillBase.ChangeSkillData(this);
            Base.OnStepChange();
            SkillBase.ChangeSkillData(null);
        }

        public int HurtPlayer(Player player, DamageClass damageClass, float damagePercentage, int DamageDirection, int Cooldown = 8, bool Critical = false, bool CountDefense = true, string DeathReason = "was slain by [caster] ability.")
        {
            return HurtPlayer(player, GetHighestDamageOfDamageClass(damageClass, damagePercentage), DamageDirection, Cooldown, Critical, CountDefense, DeathReason);
        }

        public int HurtPlayer(Player player, int Damage, int DamageDirection, int Cooldown = 8, bool Critical = false, bool CountDefense = true, string DeathReason = "was slain by [caster] ability.")
        {
            if (PlayerDamageCooldown.ContainsKey((byte)player.whoAmI))
                return 0;
            if(Caster.whoAmI != Main.myPlayer)
            {
                return 0;
            }
            int NewDamage = Damage - (CountDefense ? player.statDefense / 2 : 0);
            if (NewDamage < 1)
                NewDamage = 1;
            if (Critical)
                NewDamage *= 2;
            player.statLife -= NewDamage;
            if (player.statLife <= 0)
                player.KillMe(Terraria.DataStructures.PlayerDeathReason.ByCustomReason(player.name + " " + DeathReason.Replace("[caster]", Caster.name)), NewDamage, DamageDirection, true);
            else
            {
                if (player.stoned)
                {
                    SoundEngine.PlaySound(SoundID.Shatter, player.position);
                }
                else if (player.frostArmor)
                {
                    SoundEngine.PlaySound(Terraria.ID.SoundID.Item27, player.position);
                }
                else if ((player.wereWolf || player.forceWerewolf) && !player.hideWolf)
                {
                    SoundEngine.PlaySound(SoundID.NPCHit6, player.position);
                }
                else if (player.boneArmor)
                {
                    SoundEngine.PlaySound(SoundID.NPCHit2, player.position);
                }
                else if (!player.Male)
                {
                    SoundEngine.PlaySound(SoundID.FemaleHit, player.position);
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.PlayerHit, player.position);
                }
            }
            CombatText.NewText(player.getRect(), Color.MediumPurple, NewDamage);
            ApplyCooldownToPlayer(player, Cooldown);
            return NewDamage;
        }

        public int HurtNpc(NPC npc, DamageClass damageClass, float damagePercentage, int DamageDirection, float Knockback, int Cooldown = 8, bool Critical = false, bool CountDefense = true)
        {
            return HurtNpc(npc, GetHighestDamageOfDamageClass(damageClass, damagePercentage), DamageDirection, Knockback, Cooldown, Critical, CountDefense);
        }

        int GetHighestDamageOfDamageClass(DamageClass damageClass, float damagePercentage = 1f)
        {
            int HighestDamage = 0;
            int HighestDamageOfClass = 0;
            for (int i = 0; i < 10; i++)
            {
                if (Caster.inventory[i].type > 0 && Caster.inventory[i].damage > 0)
                {
                    int DamageValue = (int)(Caster.inventory[i].damage * ((float)Caster.inventory[i].useTime / 60));
                    if (damageClass is GenericDamageClass || Caster.inventory[i].DamageType.CountsAsClass(damageClass))
                    {
                        if (DamageValue > HighestDamageOfClass)
                            HighestDamageOfClass = DamageValue;
                    }
                    else
                    {
                        if (DamageValue > HighestDamage)
                            HighestDamage = DamageValue;
                    }
                }
            }
            int FinalDamage = HighestDamageOfClass > 0 ? HighestDamageOfClass : (int)(HighestDamage * .75f);
            return (int)(Caster.GetTotalDamage(damageClass).ApplyTo(FinalDamage) * damagePercentage);
        }

        public int HurtNpc(NPC npc, int Damage, int DamageDirection, float Knockback, int Cooldown = 8, bool Critical = false, bool CountDefense = true)
        {
            if (NpcDamageCooldown.ContainsKey((byte)npc.whoAmI) || npc.immortal || npc.dontTakeDamage)
                return 0;
            if (Caster.whoAmI != Main.myPlayer)
            {
                return 0;
            }
            int NewDamage = Damage - (CountDefense ? npc.defense / 2 : 0);
            if (NewDamage < 1)
                NewDamage = 1;
            if (Critical)
                NewDamage *= 2;
            npc.life -= NewDamage;
            npc.ApplyInteraction(Caster.whoAmI);
            npc.checkDead();
            npc.HitEffect(DamageDirection, NewDamage);
            if (npc.life > 0)
            {
                SoundEngine.PlaySound(npc.HitSound, npc.Center);
                if (Knockback > 0 && npc.knockBackResist > 0)
                {
                    float NewKB = Knockback * npc.knockBackResist;
                    npc.velocity.X += DamageDirection * NewKB;
                    npc.velocity.Y -= (npc.noGravity ? 0.75f : 0.5f);
                }
            }
            CombatText.NewText(npc.getRect(), Color.MediumPurple, NewDamage);
            ApplyCooldownToNPC(npc, Cooldown);
            return NewDamage;
        }

        public void ApplyCooldownToPlayer(Player player, int Cooldown)
        {
            if (PlayerDamageCooldown.ContainsKey(player.whoAmI))
                PlayerDamageCooldown[player.whoAmI] = Cooldown;
            else
                PlayerDamageCooldown.Add(player.whoAmI, Cooldown);
        }

        public void ApplyCooldownToNPC(NPC npc, int Cooldown)
        {
            if (NpcDamageCooldown.ContainsKey(npc.whoAmI))
                NpcDamageCooldown[npc.whoAmI] = Cooldown;
            else
                NpcDamageCooldown.Add(npc.whoAmI, Cooldown);
        }

        public void ApplyInteractionToPlayer(Player player)
        {
            if (!PlayerInteraction.Contains(player.whoAmI))
                PlayerInteraction.Add(player.whoAmI);
        }

        public void ApplyInteractionToNpc(NPC npc)
        {
            if (!NpcInteraction.Contains(npc.whoAmI))
                NpcInteraction.Add(npc.whoAmI);
        }

        public bool IsPlayerOnCooldown(Player player)
        {
            return PlayerDamageCooldown.ContainsKey(player.whoAmI);
        }

        public bool IsNPCOnCooldown(NPC npc)
        {
            return NpcDamageCooldown.ContainsKey(npc.whoAmI);
        }

        public bool HasPlayerInteraction(Player player)
        {
            return PlayerInteraction.Contains(player.whoAmI);
        }

        public bool HasNPCInteraction(NPC npc)
        {
            return NpcInteraction.Contains(npc.whoAmI);
        }
    }
}