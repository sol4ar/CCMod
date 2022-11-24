﻿using CCMod.Common;
using CCMod.Common.ModSystems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CCMod.Content.Items.Weapons.Magic
{
    public class HexedSkyBlades : ModItem, IMadeBy, IChestItem
    {
        public string CodedBy => "sucss";
        public string SpritedBy => "person_";

        public int ChestTypeChestItem => 21; // 21 is one of the chest types
        public int ChestStyleChestItem => 13; // 13 is the skyware chest style
        public int StackChestItem => 1;
        public bool ShouldSpawnChestItem => Main.rand.NextBool(3); // 1/3 chance to spawn in the chest

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hexed Skyblades");
            Tooltip.SetDefault("[c/6dc7d1:The blades deal heavy damage] [c/e84343:but can also hurt the player...]\n[c/f3fa4f:\"Do you believe in \'gravity\'?\"]");
        }

        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 46;

            Item.crit = 7;
            Item.damage = 18;
            Item.knockBack = 4f;
            Item.DamageType = DamageClass.Magic;

            Item.UseSound = SoundID.Item63;
            Item.useTime = 8;
            Item.useAnimation = Item.useTime;
            Item.autoReuse = true;

            Item.useStyle = ItemUseStyleID.Shoot;

            Item.noMelee = true;

            Item.value = Item.sellPrice(0, 0, 65, 0);
            Item.rare = ItemRarityID.Blue;

            Item.shoot = ModContent.ProjectileType<HexedSkyBladesProjectile>();
            Item.shootSpeed = 30f;

            Item.noUseGraphic = true;
        }

        static int direction = 1;
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position += direction * velocity.RotatedBy(MathHelper.PiOver2).Normalized() * 65 - velocity.Normalized() * 35;
            direction = -direction;

            velocity = position.DirectionTo(Main.MouseWorld) * Item.shootSpeed;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            CCModUtils.NewDustCircular(position, 10, DustID.SilverFlame, 16, minMaxSpeedFromCenter: (6, 6), dustAction: d => d.noGravity = true);
            return true;
        }
    }
}
