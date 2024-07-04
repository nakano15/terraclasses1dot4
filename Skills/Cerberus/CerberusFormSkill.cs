using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using System;
using Mono.CompilerServices.SymbolWriter;

namespace terraclasses.Skills.Cerberus
{
    public class CerberusFormSkill : SkillBase
    {
        public override string Name => "Cerberus Form";
        public override string Description => "Changes yourself to a Cerberus avatar.";
        public override SkillTypes SkillType => SkillTypes.Passive;
        public override bool EffectiveAtLevel0 => true;
        public override SkillData GetSkillData => new CerberusFormData();
        protected override SkillAttribute[] SetSkillAttributes => new SkillAttribute[]
        {
            new UnlockAttribute(),
            new FireDamageImmunity()
        };

        public override void Update(SkillData Data)
        {
            Player Caster = Data.GetCaster;
            int FireImLv = Data.GetAttributeLevel(1);
            CerberusFormData data = (CerberusFormData)Data;
            for (byte i = 0; i < 3; i++)
            {
                if (data.HeadFrame[i] > 0)
                    data.HeadFrame[i]--;
            }
            if (FireImLv-- > -1)
            {
                Caster.fireWalk = true;
            }
            if (FireImLv-- > -1)
            {
                if (Caster.HasBuff(BuffID.OnFire))
                {
                    Caster.lifeRegen += 8;
                    data.FireHealingTimer++;
                    if (data.FireHealingTimer >= 10)
                    {
                        data.FireHealingTimer -= 10;
                        if (Caster.statLife < Caster.statLifeMax2)
                        {
                            int HealthRegen = (int)System.MathF.Max(1, Caster.statLifeMax2 / Caster.statLifeMax);
                            Caster.statLife += HealthRegen;
                            CombatText.NewText(Caster.getRect(), CombatText.HealLife, HealthRegen, false, true);
                        }
                    }
                }
            }
            if (FireImLv-- > -1)
                Caster.lavaImmune = true;
        }

        public static void DarkenSkin(ref Color color)
        {
            color.R = (byte)(color.R * .3f);
            color.G = (byte)(color.G * .3f);
            color.B = (byte)(color.B * .3f);
        }

        public class CerberusFormData : SkillData
        {
            public byte[] HeadFrame = new byte[] { 0, 0, 0 };
            public byte FireHealingTimer = 0;
        }
        
        public class CerberusHeadLayer : PlayerDrawLayer
        {
            public override string Name => "TerraClasses: Cerberus Head";
            public override bool IsHeadLayer => true;

            public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
            {
                return PlayerMod.PlayerHasSkill(drawInfo.drawPlayer, SkillDB.CerberusForm);
            }

            public override Position GetDefaultPosition()
            {
                return new AfterParent(PlayerDrawLayers.Head);
            }

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Player player = drawInfo.drawPlayer;
                CerberusFormData data = PlayerMod.PlayerGetSkillOrNull(player, SkillDB.CerberusForm) as CerberusFormData;
                Texture2D CerberusTexture = terraclasses.CerberusTexture.Value;
                int maxc = drawInfo.DrawDataCache.Count - 1;
                for (int t = maxc; t > 0; t--)
                {
                    DrawData dd = drawInfo.DrawDataCache[t];
                    if (dd.texture == TextureAssets.Players[0, 0].Value)
                    {
                        for (byte i = 0; i < 3; i++)
                        {
                            Rectangle rect = RevampRect(dd.sourceRect.Value);
                            switch(i)
                            {
                                case 0:
                                    rect.X += 2 * 40;
                                    break;
                                case 1:
                                    rect.X += 3 * 40;
                                    break;
                                case 2:
                                    rect.X += 1 * 40;
                                    break;
                            }
                            if (data.HeadFrame[i] > 0)
                            {
                                rect.Y = 19 * rect.Height;
                            }
                            DrawData ndd = new DrawData(CerberusTexture, dd.position, rect, drawInfo.colorBodySkin, dd.rotation, dd.origin, dd.scale, dd.effect, 0);
                            switch (i)
                            {
                                case 0:
                                    drawInfo.DrawDataCache[t] = ndd;
                                    break;
                                case 1:
                                    drawInfo.DrawDataCache.Insert(t + 1, ndd);
                                    break;
                                case 2:
                                    drawInfo.DrawDataCache.Insert(t - 1, ndd);
                                    break;
                            }
                        }
                        t--;
                    }
                    else if (drawInfo.cHead > 0 && dd.texture == TextureAssets.ArmorHead[drawInfo.cHead].Value)
                    {
                        drawInfo.DrawDataCache.RemoveAt(t);
                    }
                    else if ((!drawInfo.fullHair && dd.texture == TextureAssets.PlayerHairAlt[player.hair].Value) || dd.texture == TextureAssets.PlayerHair[player.hair].Value)
                    {
                        drawInfo.DrawDataCache.RemoveAt(t);
                    }
                    else if (dd.texture == TextureAssets.Players[drawInfo.skinVar, PlayerTextureID.Eyes].Value)
                    {
                        Rectangle rect = RevampRect(new Rectangle(dd.sourceRect.Value.X + 4 * 40, dd.sourceRect.Value.Y, dd.sourceRect.Value.Width, dd.sourceRect.Value.Height));
                        DrawData ndd = new DrawData(CerberusTexture, dd.position, rect, dd.color, dd.rotation, dd.origin, dd.scale, dd.effect, 0);
                        drawInfo.DrawDataCache[t] = ndd;
                    }
                    else if (dd.texture == TextureAssets.Players[drawInfo.skinVar, PlayerTextureID.EyeWhites].Value)
                    {
                        Rectangle rect = RevampRect(new Rectangle(dd.sourceRect.Value.X + 5 * 40, dd.sourceRect.Value.Y, dd.sourceRect.Value.Width, dd.sourceRect.Value.Height));
                        DrawData ndd = new DrawData(CerberusTexture, dd.position, rect, dd.color, dd.rotation, dd.origin, dd.scale, dd.effect, 0);
                        drawInfo.DrawDataCache[t] = ndd;
                    }
                    else if (dd.texture == TextureAssets.Players[drawInfo.skinVar, PlayerTextureID.LegSkin].Value)
                    {
                        Rectangle rect = RevampRect(new Rectangle(dd.sourceRect.Value.X, dd.sourceRect.Value.Y, dd.sourceRect.Value.Width, dd.sourceRect.Value.Height));
                        DrawData ndd = new DrawData(CerberusTexture, dd.position, rect, drawInfo.colorBodySkin, dd.rotation, dd.origin, dd.scale, dd.effect, 0);
                        drawInfo.DrawDataCache.Insert(t, ndd);
                    }
                }
            }

            Rectangle RevampRect(Rectangle Original)
            {
                if (Original.Y == 1064)
                {
                    return new Rectangle(Original.X, 0, Original.Width, Original.Height);
                }
                return Original;
            }
        }

        class UnlockAttribute : SkillAttribute
        {
            public override string Name => "Form Enabled";

            public override string AttributeDescription(int Level)
            {
                return "Form Enabled";
            }

            public override int MaxLevel => 0;
        }

        class FireDamageImmunity : SkillAttribute
        {
            public override string Name => "Fire Immunity";
            public override int MaxLevel => 3;
            public override string AttributeDescription(int Level)
            {
                switch (Level)
                {
                    case 0:
                        return "Still vulnerable to fire damages.";
                    case 1:
                        return "Fire Blocks no longer harm you.";
                    case 2:
                        return "Fire Blocks no longer harm you, and On Fire! heals you.";
                    case 3:
                        return "Fire Blocks and Lava no longer harm you, and On Fire! heals you.";
                }
                return base.AttributeDescription(Level);
            }
        }
    }
}