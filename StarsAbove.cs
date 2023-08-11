using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using StarsAbove.SceneEffects.CustomSkies;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using Terraria.ID;
using StarsAbove.Items;
using ReLogic.Content.Sources;

using StarsAbove.Systems;
using StarsAbove.NPCs.WarriorOfLight;
using StarsAbove.NPCs.Vagrant;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.NPCs.Dioskouroi;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.NPCs;
using Terraria.GameContent.UI.Elements;
using Terraria.IO;
using StarsAbove.UI.Starfarers;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Events;
using Terraria.GameContent.NetModules;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Light;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Net;
using Terraria.Net.Sockets;
using Terraria.Social;
using Terraria.Utilities;
using Terraria.WorldBuilding;
using static Mono.Cecil.Cil.OpCodes;

namespace StarsAbove
{

    public class StarsAbove : Mod
	{
        //Sublib
        private static ILHook tcpSocketHook;
        private static ILHook socialSocketHook;

        public static ModKeybind novaKey;
		public static ModKeybind weaponActionKey;

		public static bool sharedAudio;
		public static StarsAbove Instance { get; set; }
		
		public StarsAbove() => Instance = this;

		//Video player code sourced from Terraria Overhaul- I can't thank you enough!
		public override IContentSource CreateDefaultContentSource()
		{
			if(!Main.dedServ)
            {
				AddContent(new OgvReader());

			}

			return base.CreateDefaultContentSource();
		}

		public override void Load()
        {
            Logger.InfoFormat("'I've always wanted to write text in crash logs!' -A", Name);
            Logger.InfoFormat("'Eh? Doesn't that make you look like you caused it..?' -E", Name);

            ModLoader.TryGetMod("Wikithis", out Mod wikithis);
            if (wikithis != null && !Main.dedServ)
            {
                wikithis.Call("AddModURL", this, "terrariamods.wiki.gg$The_Stars_Above");
            }

            if (Main.netMode != NetmodeID.Server)
            {

                //GameShaders.Misc["StarsAbove:DeathAnimation"] = new MiscShaderData(new Ref<Effect>(ModContent.Request<Effect>("Effects/EffectDeath").Value), "DeathAnimation");
                Terraria.Graphics.Effects.Filters.Scene["StarsAbove:Void"] = new Filter(new ScreenShaderData("FilterMoonLord"), EffectPriority.Medium);
                SkyManager.Instance["StarsAbove:Void"] = new VoidSky();
                //Filters.Scene["StarsAbove:EverlastingLight"] = new Filter(new ScreenShaderData("FilterMoonLord"), EffectPriority.Low);
                SkyManager.Instance["StarsAbove:EverlastingLight"] = new EverlastingLightSky();
                SkyManager.Instance["StarsAbove:EverlastingLightPreview"] = new EverlastingLightSkyPreview();

                SkyManager.Instance["StarsAbove:CorvusSky"] = new CorvusSky();

                SkyManager.Instance["StarsAbove:ObservatorySkyDay"] = new ObservatorySkyDay();
                SkyManager.Instance["StarsAbove:EdinGenesisQuasarSky"] = new EdinGenesisQuasarSky();


                Terraria.Graphics.Effects.Filters.Scene["StarsAbove:MoonSky"] = new Filter(new ScreenShaderData("FilterTower").UseColor(0f, 0.5f, 1f).UseOpacity(0.5f), EffectPriority.High);
                SkyManager.Instance["StarsAbove:MoonSky"] = new MoonSky();


                Ref<Effect> screenRef = new Ref<Effect>(ModContent.Request<Effect>("StarsAbove/Effects/ShockwaveEffect", AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
                Terraria.Graphics.Effects.Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.High);
                Terraria.Graphics.Effects.Filters.Scene["Shockwave"].Load();

                var MiscEffect = new Ref<Effect>(Assets.Request<Effect>("Effects/MiscEffect", AssetRequestMode.ImmediateLoad).Value);
                GameShaders.Misc["CyclePass"] = new MiscShaderData(MiscEffect, "CyclePass");

                GameShaders.Misc["StarsAbove:DeathAnimation"] = new MiscShaderData(
                  new Ref<Effect>(ModContent.Request<Effect>("StarsAbove/Effects/EffectDeath", AssetRequestMode.ImmediateLoad).Value),
                  "DeathAnimation"
                );

                //Shelved for later. Doesn't work yet.
                //Terraria.GameContent.UI.Elements.On_UICharacterListItem.ctor += Hook_UICharacterList;
            }

            novaKey = KeybindLoader.RegisterKeybind(this, "Stellar Nova", "Z");
            weaponActionKey = KeybindLoader.RegisterKeybind(this, "Weapon Action", "X");

            SubworldLibrary();
        }

        private void SubworldLibrary()
        {
            FieldInfo current = typeof(SubworldSystem).GetField("current", BindingFlags.NonPublic | BindingFlags.Static);
            FieldInfo cache = typeof(SubworldSystem).GetField("cache", BindingFlags.NonPublic | BindingFlags.Static);
            FieldInfo hideUnderworld = typeof(SubworldSystem).GetField("hideUnderworld");
            MethodInfo normalUpdates = typeof(Subworld).GetMethod("get_NormalUpdates");
            MethodInfo shouldSave = typeof(Subworld).GetMethod("get_ShouldSave");

            if (Main.dedServ)
            {
                IL_Main.DedServ_PostModLoad += il =>
                {
                    ConstructorInfo gameTime = typeof(GameTime).GetConstructor(Type.EmptyTypes);
                    MethodInfo update = typeof(Main).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
                    FieldInfo saveTime = typeof(Main).GetField("saveTime", BindingFlags.NonPublic | BindingFlags.Static);

                    var c = new ILCursor(il);
                    if (!c.TryGotoNext(MoveType.After, i => i.MatchStindI1()))
                    {
                        return;
                    }

                    c.Emit(OpCodes.Call, typeof(SubworldSystem).GetMethod("LoadIntoSubworld", BindingFlags.NonPublic | BindingFlags.Static));
                    var skip = c.DefineLabel();
                    c.Emit(Brfalse, skip);

                    c.Emit(Ldarg_0);
                    c.Emit(Newobj, gameTime);
                    c.Emit(Callvirt, update);

                    c.Emit(Newobj, typeof(Stopwatch).GetConstructor(Type.EmptyTypes));
                    c.Emit(Stloc_1);
                    c.Emit(Ldloc_1);
                    c.Emit(Callvirt, typeof(Stopwatch).GetMethod("Start"));

                    c.Emit(Ldc_I4_0);
                    c.Emit(Stsfld, typeof(Main).GetField("gameMenu"));

                    c.Emit(Ldc_R8, 16.666666666666668);
                    c.Emit(Stloc_2);
                    c.Emit(Ldloc_2);
                    c.Emit(Stloc_3);

                    var loopStart = c.DefineLabel();
                    c.Emit(Br, loopStart);

                    var loop = c.DefineLabel();
                    c.MarkLabel(loop);

                    c.Emit(OpCodes.Call, typeof(Main).Assembly.GetType("Terraria.ModLoader.Engine.ServerHangWatchdog").GetMethod("Checkin", BindingFlags.NonPublic | BindingFlags.Static));

                    c.Emit(Ldsfld, typeof(Netplay).GetField("HasClients"));
                    var label = c.DefineLabel();
                    c.Emit(Brfalse, label);

                    c.Emit(Ldarg_0);
                    c.Emit(Newobj, gameTime);
                    c.Emit(Callvirt, update);
                    var label2 = c.DefineLabel();
                    c.Emit(Br, label2);

                    c.MarkLabel(label);

                    c.Emit(Ldsfld, saveTime);
                    c.Emit(Callvirt, typeof(Stopwatch).GetMethod("get_IsRunning"));
                    c.Emit(Brfalse, label2);

                    c.Emit(Ldsfld, saveTime);
                    c.Emit(Callvirt, typeof(Stopwatch).GetMethod("Stop"));
                    c.Emit(Br, label2);

                    c.MarkLabel(label2);

                    c.Emit(Ldloc_1);
                    c.Emit(Ldloc_2);
                    c.Emit(Ldloca, 3);
                    c.Emit(OpCodes.Call, typeof(StarsAbove).GetMethod("Sleep", BindingFlags.NonPublic | BindingFlags.Static));

                    c.MarkLabel(loopStart);

                    c.Emit(Ldsfld, typeof(Netplay).GetField("Disconnect"));
                    c.Emit(Brfalse, loop);

                    c.Emit(Ret);

                    c.MarkLabel(skip);
                };

                if (!Program.LaunchParameters.ContainsKey("-subworld"))
                {
                    IL_NetMessage.CheckBytes += il =>
                    {
                        ILCursor c, cc;
                        if (!(c = new ILCursor(il)).TryGotoNext(MoveType.After, i => i.MatchCall(typeof(BitConverter), "ToUInt16"))
                        || !c.Instrs[c.Index].MatchStloc(out int index)
                        || !c.TryGotoNext(MoveType.After, i => i.MatchCallvirt(typeof(Stream), "get_Position"), i => i.MatchStloc(out _))
                        || !(cc = c.Clone()).TryGotoNext(i => i.MatchLdsfld(typeof(NetMessage), "buffer"), i => i.MatchLdarg(0), i => i.MatchLdelemRef(), i => i.MatchLdfld(typeof(MessageBuffer), "reader")))
                        {
                            Logger.Error("FAILED:");
                            return;
                        }

                        c.Emit(Ldsfld, typeof(NetMessage).GetField("buffer"));
                        c.Emit(Ldarg_0);
                        c.Emit(Ldelem_Ref);
                        c.Emit(Ldloc_2);
                        c.Emit(Ldloc, index);
                        c.Emit(OpCodes.Call, typeof(StarsAbove).GetMethod("DenyRead", BindingFlags.NonPublic | BindingFlags.Static));

                        var label = c.DefineLabel();
                        c.Emit(Brtrue, label);

                        cc.MarkLabel(label);
                    };

                    socialSocketHook = new ILHook(typeof(SocialSocket).GetMethod("Terraria.Net.Sockets.ISocket.AsyncSend", BindingFlags.NonPublic | BindingFlags.Instance), AsyncSend);
                    tcpSocketHook = new ILHook(typeof(TcpSocket).GetMethod("Terraria.Net.Sockets.ISocket.AsyncSend", BindingFlags.NonPublic | BindingFlags.Instance), AsyncSend);

                    void AsyncSend(ILContext il)
                    {
                        var c = new ILCursor(il);
                        if (!c.TryGotoNext(MoveType.After, i => i.MatchRet()))
                        {
                            return;
                        }
                        c.MoveAfterLabels();

                        c.Emit(Ldarg_0);
                        c.Emit(Ldarg_1);
                        c.Emit(Ldarg_2);
                        c.Emit(Ldarg_3);
                        c.Emit(Ldarga, 5);
                        c.Emit(OpCodes.Call, typeof(StarsAbove).GetMethod("DenySend", BindingFlags.NonPublic | BindingFlags.Static));
                        var label = c.DefineLabel();
                        c.Emit(Brfalse, label);
                        c.Emit(Ret);
                        c.MarkLabel(label);
                    }
                }
            }
            else
            {
                IL_Main.DoDraw += il =>
                {
                    var c = new ILCursor(il);
                    if (!c.TryGotoNext(MoveType.After, i => i.MatchStsfld(typeof(Main), "HoverItem")))
                    {
                        return;
                    }

                    c.Emit(Ldsfld, typeof(Main).GetField("gameMenu"));
                    var skip = c.DefineLabel();
                    c.Emit(Brfalse, skip);

                    c.Emit(Ldsfld, current);
                    var label = c.DefineLabel();
                    c.Emit(Brfalse, label);

                    c.Emit(Ldc_R4, 1f);
                    c.Emit(Dup);
                    c.Emit(Dup);
                    c.Emit(Stsfld, typeof(Main).GetField("_uiScaleWanted", BindingFlags.NonPublic | BindingFlags.Static));
                    c.Emit(Stsfld, typeof(Main).GetField("_uiScaleUsed", BindingFlags.NonPublic | BindingFlags.Static));
                    c.Emit(OpCodes.Call, typeof(Matrix).GetMethod("CreateScale", new Type[] { typeof(float) }));
                    c.Emit(Stsfld, typeof(Main).GetField("_uiScaleMatrix", BindingFlags.NonPublic | BindingFlags.Static));

                    c.Emit(Ldsfld, current);
                    c.Emit(Ldarg_0);
                    c.Emit(Callvirt, typeof(Subworld).GetMethod("DrawSetup"));
                    c.Emit(Ret);

                    c.MarkLabel(label);

                    c.Emit(Ldsfld, cache);
                    c.Emit(Brfalse, skip);

                    c.Emit(Ldc_R4, 1f);
                    c.Emit(Dup);
                    c.Emit(Dup);
                    c.Emit(Stsfld, typeof(Main).GetField("_uiScaleWanted", BindingFlags.NonPublic | BindingFlags.Static));
                    c.Emit(Stsfld, typeof(Main).GetField("_uiScaleUsed", BindingFlags.NonPublic | BindingFlags.Static));
                    c.Emit(OpCodes.Call, typeof(Matrix).GetMethod("CreateScale", new Type[] { typeof(float) }));
                    c.Emit(Stsfld, typeof(Main).GetField("_uiScaleMatrix", BindingFlags.NonPublic | BindingFlags.Static));

                    c.Emit(Ldsfld, cache);
                    c.Emit(Ldarg_0);
                    c.Emit(Callvirt, typeof(Subworld).GetMethod("DrawSetup"));
                    c.Emit(Ret);

                    c.MarkLabel(skip);
                };

                IL_Main.DrawBackground += il =>
                {
                    ILCursor c, cc;
                    if (!(c = new ILCursor(il)).TryGotoNext(i => i.MatchLdcI4(330))
                    || !(cc = c.Clone()).TryGotoNext(i => i.MatchStloc(out _), i => i.MatchLdcR4(255)))
                    {
                        Logger.Error("FAILED:");
                        return;
                    }

                    c.Emit(Ldsfld, hideUnderworld);
                    var skip = c.DefineLabel();
                    c.Emit(Brfalse, skip);

                    c.Emit(Conv_R8);
                    var label = c.DefineLabel();
                    c.Emit(Br, label);

                    c.MarkLabel(skip);

                    cc.MarkLabel(label);
                };

                IL_Main.OldDrawBackground += il =>
                {
                    ILCursor c, cc;
                    if (!(c = new ILCursor(il)).TryGotoNext(i => i.MatchLdcI4(230))
                    || !(cc = c.Clone()).TryGotoNext(i => i.MatchStloc(18), i => i.MatchLdcI4(0)))
                    {
                        Logger.Error("FAILED:");
                        return;
                    }

                    c.Emit(Ldsfld, hideUnderworld);
                    var skip = c.DefineLabel();
                    c.Emit(Brfalse, skip);

                    c.Emit(Conv_R8);
                    var label = c.DefineLabel();
                    c.Emit(Br, label);

                    c.MarkLabel(skip);

                    cc.MarkLabel(label);
                };

                IL_Main.UpdateAudio += il =>
                {
                    ILCursor c, cc;
                    if (!(c = new ILCursor(il)).TryGotoNext(MoveType.AfterLabel, i => i.MatchLdsfld(typeof(Main), "swapMusic"))
                    || !(cc = c.Clone()).TryGotoNext(MoveType.After, i => i.MatchCall(typeof(Main), "UpdateAudio_DecideOnNewMusic"))
                    || !cc.Instrs[cc.Index].MatchBr(out ILLabel label))
                    {
                        Logger.Error("FAILED:");
                        return;
                    }

                    c.Emit(OpCodes.Call, typeof(SubworldSystem).GetMethod("ChangeAudio", BindingFlags.NonPublic | BindingFlags.Static));
                    var skip = c.DefineLabel();
                    c.Emit(Brfalse, skip);

                    c.Emit(OpCodes.Call, typeof(SubworldSystem).GetMethod("ManualAudioUpdates", BindingFlags.NonPublic | BindingFlags.Static));
                    c.Emit(Brfalse, label);

                    var ret = c.DefineLabel();
                    ret.Target = c.Instrs[c.Instrs.Count - 1];
                    c.Emit(Leave, ret);

                    c.MarkLabel(skip);
                };

                IL_IngameOptions.Draw += il =>
                {
                    ILCursor c, cc, ccc, cccc;
                    if (!(c = new ILCursor(il)).TryGotoNext(i => i.MatchLdsfld(typeof(Lang), "inter"), i => i.MatchLdcI4(35))
                    || !(cc = c.Clone()).TryGotoNext(MoveType.After, i => i.MatchCallvirt(typeof(LocalizedText), "get_Value"))
                    || !(ccc = cc.Clone()).TryGotoNext(i => i.MatchLdnull(), i => i.MatchCall(typeof(WorldGen), "SaveAndQuit"))
                    || !(cccc = ccc.Clone()).TryGotoPrev(MoveType.AfterLabel, i => i.MatchLdloc(out _), i => i.MatchLdcI4(1), i => i.MatchAdd(), i => i.MatchStloc(out _)))
                    {
                        Logger.Error("FAILED:");
                        return;
                    }

                    ccc.Emit(Ldsfld, current);
                    var skip = ccc.DefineLabel();
                    ccc.Emit(Brfalse, skip);

                    ccc.Emit(OpCodes.Call, typeof(SubworldSystem).GetMethod("Exit"));
                    var label = ccc.DefineLabel();
                    ccc.Emit(Br, label);

                    ccc.MarkLabel(skip);

                    ccc.Index += 2;
                    ccc.MarkLabel(label);

                    cccc.Emit(Ldsfld, typeof(SubworldSystem).GetField("noReturn"));
                    cccc.Emit(Brtrue, label);

                    c.Emit(Ldsfld, current);
                    skip = c.DefineLabel();
                    c.Emit(Brfalse, skip);

                    c.Emit(Ldstr, "Mods.SubworldLibrary.Return");
                    c.Emit(OpCodes.Call, typeof(Language).GetMethod("GetTextValue", new Type[] { typeof(string) }));
                    label = c.DefineLabel();
                    c.Emit(Br, label);

                    c.MarkLabel(skip);

                    cc.MarkLabel(label);
                };

                IL_TileLightScanner.GetTileLight += il =>
                {
                    ILCursor c, cc, ccc;
                    if (!(c = new ILCursor(il)).TryGotoNext(MoveType.After, i => i.MatchStloc(1))
                    || !(cc = c.Clone()).TryGotoNext(MoveType.AfterLabel, i => i.MatchLdarg(2), i => i.MatchCall(typeof(Main), "get_UnderworldLayer"))
                    || !(ccc = cc.Clone()).TryGotoNext(MoveType.After, i => i.MatchCall(typeof(TileLightScanner), "ApplyHellLight")))
                    {
                        Logger.Error("FAILED:");
                        return;
                    }

                    c.Emit(Ldsfld, current);
                    var skip = c.DefineLabel();
                    c.Emit(Brfalse, skip);

                    c.Emit(Ldsfld, current);
                    c.Emit(Ldloc_0);
                    c.Emit(Ldarg_1);
                    c.Emit(Ldarg_2);
                    c.Emit(Ldloca, 1);
                    c.Emit(Ldarg_3);
                    c.Emit(Callvirt, typeof(Subworld).GetMethod("GetLight"));

                    c.Emit(Brfalse, skip);
                    c.Emit(Ret);

                    c.MarkLabel(skip);

                    cc.Emit(Ldsfld, hideUnderworld);
                    skip = cc.DefineLabel();
                    cc.Emit(Brtrue, skip);

                    ccc.MarkLabel(skip);
                };

                IL_Player.UpdateBiomes += il =>
                {
                    ILCursor c, cc;
                    if (!(c = new ILCursor(il)).TryGotoNext(MoveType.AfterLabel, i => i.MatchLdloc(out _), i => i.MatchLdfld(typeof(Point), "Y"), i => i.MatchLdsfld(typeof(Main), "maxTilesY"))
                    || !(cc = c.Clone()).TryGotoNext(i => i.MatchStloc(out _)))
                    {
                        Logger.Error("FAILED:");
                        return;
                    }

                    c.Emit(Ldsfld, hideUnderworld);
                    var skip = c.DefineLabel();
                    c.Emit(Brfalse, skip);

                    c.Emit(Ldc_I4_0);
                    var label = c.DefineLabel();
                    c.Emit(Br, label);

                    c.MarkLabel(skip);

                    cc.MarkLabel(label);
                };

                IL_Main.DrawUnderworldBackground += il =>
                {
                    var c = new ILCursor(il);

                    c.Emit(Ldsfld, hideUnderworld);
                    var skip = c.DefineLabel();
                    c.Emit(Brfalse, skip);
                    c.Emit(Ret);
                    c.MarkLabel(skip);
                };

                IL_Netplay.AddCurrentServerToRecentList += il =>
                {
                    var c = new ILCursor(il);

                    c.Emit(Ldsfld, current);
                    var skip = c.DefineLabel();
                    c.Emit(Brfalse, skip);
                    c.Emit(Ret);
                    c.MarkLabel(skip);
                };
            }

            IL_Main.EraseWorld += il =>
            {
                var c = new ILCursor(il);

                c.Emit(Ldarg_0);
                c.Emit(OpCodes.Call, typeof(SubworldSystem).GetMethod("EraseSubworlds", BindingFlags.NonPublic | BindingFlags.Static));
            };

            IL_Main.DoUpdateInWorld += il =>
            {
                ILCursor c, cc;
                if (!(c = new ILCursor(il)).TryGotoNext(MoveType.After, i => i.MatchCall(typeof(SystemLoader), "PreUpdateTime"))
                || !(cc = c.Clone()).TryGotoNext(i => i.MatchCall(typeof(SystemLoader), "PostUpdateTime")))
                {
                    Logger.Error("FAILED:");
                    return;
                }

                c.Emit(Ldsfld, current);
                var skip = c.DefineLabel();
                c.Emit(Brfalse, skip);

                c.Emit(Ldsfld, current);
                c.Emit(Callvirt, normalUpdates);
                var label = c.DefineLabel();
                c.Emit(Brfalse, label);

                c.MarkLabel(skip);

                cc.MarkLabel(label);
            };

            IL_WorldGen.UpdateWorld += il =>
            {
                var c = new ILCursor(il);
                if (!c.TryGotoNext(i => i.MatchCall(typeof(WorldGen), "UpdateWorld_Inner")))
                {
                    Logger.Error("FAILED:");
                    return;
                }

                c.Emit(Ldsfld, current);
                var skip = c.DefineLabel();
                c.Emit(Brfalse, skip);

                c.Emit(Ldsfld, current);
                c.Emit(Callvirt, normalUpdates);
                var label = c.DefineLabel();
                c.Emit(Brfalse, label);

                c.MarkLabel(skip);

                c.Index++;
                c.MarkLabel(label);
            };

            IL_Player.Update += il => UpdateGravity(il, 3, i => i.MatchLdarg(0), i => i.MatchLdarg(0), i => i.MatchLdfld(typeof(Player), "gravity"));
            IL_NPC.UpdateNPC_UpdateGravity += il => UpdateGravity(il, 1, i => i.MatchLdsfld(typeof(NPC), "gravity"));

            void UpdateGravity(ILContext il, int back, params Func<Instruction, bool>[] predicates)
            {
                ILCursor c, cc;
                if (!(c = new ILCursor(il)).TryGotoNext(MoveType.AfterLabel, i => i.MatchLdsfld(out _), i => i.MatchConvR4(), i => i.MatchLdcR4(4200))
                || !(cc = c.Clone()).TryGotoNext(MoveType.After, predicates)
                || !cc.Instrs[cc.Index].MatchLdloc(out int index))
                {
                    Logger.Error("FAILED:");
                    return;
                }

                c.Emit(Ldsfld, current);
                var skip = c.DefineLabel();
                c.Emit(Brfalse, skip);

                c.Emit(Ldsfld, current);
                c.Emit(Callvirt, normalUpdates);
                var label = c.DefineLabel();
                c.Emit(Brtrue, skip);

                c.Emit(Ldsfld, current);
                c.Emit(Ldarg_0);
                c.Emit(Callvirt, typeof(Subworld).GetMethod("GetGravity"));
                c.Emit(Stloc, index);
                c.Emit(Br, label);

                c.MarkLabel(skip);

                cc.Index -= back;
                cc.MarkLabel(label);
            }

            IL_Liquid.Update += il =>
            {
                var c = new ILCursor(il);
                if (!c.TryGotoNext(i => i.MatchLdarg(0), i => i.MatchLdfld(typeof(Liquid), "y"), i => i.MatchCall(typeof(Main), "get_UnderworldLayer")))
                {
                    Logger.Error("FAILED:");
                    return;
                }

                c.Emit(Ldsfld, current);
                var skip = c.DefineLabel();
                c.Emit(Brfalse, skip);

                c.Emit(Ldsfld, current);
                c.Emit(Callvirt, normalUpdates);
                c.Emit(Brfalse, (ILLabel)c.Instrs[c.Index + 3].Operand);

                c.MarkLabel(skip);
            };

            IL_Player.SavePlayer += il =>
            {
                ILCursor c, cc, ccc;
                if (!(c = new ILCursor(il)).TryGotoNext(i => i.MatchCall(typeof(Player), "InternalSaveMap"))
                || !(cc = c.Clone()).TryGotoNext(MoveType.AfterLabel, i => i.MatchLdsfld(typeof(Main), "ServerSideCharacter"))
                || !(ccc = cc.Clone()).TryGotoNext(MoveType.After, i => i.MatchCall(typeof(FileUtilities), "ProtectedInvoke")))
                {
                    Logger.Error("FAILED:");
                    return;
                }

                c.Index -= 3;

                c.Emit(Ldsfld, cache);
                var skip = c.DefineLabel();
                c.Emit(Brfalse, skip);

                c.Emit(Ldsfld, cache);
                c.Emit(Callvirt, shouldSave);
                var label = c.DefineLabel();
                c.Emit(Brfalse, label);

                c.MarkLabel(skip);

                cc.MarkLabel(label);

                cc.Emit(Ldsfld, cache);
                skip = cc.DefineLabel();
                cc.Emit(Brfalse, skip);

                cc.Emit(Ldsfld, cache);
                cc.Emit(Callvirt, typeof(Subworld).GetMethod("get_NoPlayerSaving"));
                label = cc.DefineLabel();
                cc.Emit(Brtrue, label);

                cc.MarkLabel(skip);

                ccc.MarkLabel(label);
            };

            IL_WorldFile.SaveWorld_bool_bool += il =>
            {
                var c = new ILCursor(il);

                c.Emit(Ldsfld, cache);
                var skip = c.DefineLabel();
                c.Emit(Brfalse, skip);
                c.Emit(Ldsfld, cache);
                c.Emit(Callvirt, shouldSave);
                c.Emit(Brtrue, skip);
                c.Emit(Ret);

                c.MarkLabel(skip);
            };
        }

        private void Hook_UICharacterList(On_UICharacterListItem.orig_ctor orig, UICharacterListItem self, PlayerFileData data, int snapPointIndex)
		{        //Thank you to tMod discord member pure_epic

			orig(self, data, snapPointIndex);
			if (data.Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
			{
				MainMenuIcon Icon = new MainMenuIcon();
				self.Append(Icon);
			}
			else if (data.Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
			{
				self.Append(MainMenuIcon.umbral);
			}
		}
		public override void Unload()
		{
			
			novaKey = null;
			weaponActionKey = null;

			base.Unload();
		}

        /*
		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			//SubworldSystem.MovePlayerToSubworld("Observatory", whoAmI);

			byte msgType = reader.ReadByte();
			switch (msgType)
			{
				case 0://Attempting to enter a suwbworld

					string id = reader.ReadString();
					//SubworldSystem.MovePlayerToSubworld("StarsAbove/" + id, whoAmI);

					/*
					switch (id)
					{
						case "Observatory":
							break;
						case "Test":
							break;
						default:
							Logger.WarnFormat("StarsAbove: Unknown Subworld");
							break;
					}

					break;
				default:
					Logger.WarnFormat("StarsAbove: Unknown Message type: {0}", msgType);
					break;
			}
		}
        */

		public override object Call(params object[] args)
		{
			// Make sure the call doesn't include anything that could potentially cause exceptions.
			if (args is null)
			{
				throw new ArgumentNullException(nameof(args), "Arguments cannot be null!");
			}

			if (args.Length == 0)
			{
				throw new ArgumentException("Arguments cannot be empty!");
			}

			// This check makes sure that the argument is a string using pattern matching.
			// Since we only need one parameter, we'll take only the first item in the array..
			if (args[0] is string content)
			{
				// ..And treat it as a command type.
				switch (content)
				{
					case "downedVagrant":
						return DownedBossSystem.downedVagrant;

					case "downedNalhaun":
						return DownedBossSystem.downedNalhaun;

					case "downedPenthesilea":
						return DownedBossSystem.downedPenth;

					case "downedArbitration":
						return DownedBossSystem.downedArbiter;

					case "downedWarriorOfLight":
						return DownedBossSystem.downedWarrior;

					case "downedDioskouroi":
						return DownedBossSystem.downedDioskouroi;

					case "downedTsukiyomi":
						return DownedBossSystem.downedTsuki;

				}
			}

			

			// If the arguments provided don't match anything we wanted to return a value for, we'll return a 'false' boolean.
			// This value can be anything you would like to provide as a default value.
			return false;
		}
		public override void PostSetupContent()
		{
			Mod bossChecklist;
			ModLoader.TryGetMod("BossChecklist", out bossChecklist);
			Mod recipeBrowser;
			ModLoader.TryGetMod("RecipeBrowser", out recipeBrowser);
			Mod musicDisplay;
			ModLoader.TryGetMod("MusicDisplay", out musicDisplay);

			Func<bool> CanSeeTsuki = () =>
			{
				bool WarriorDowned = DownedBossSystem.downedWarrior;

				bool canSeeBoss = WarriorDowned;

				return canSeeBoss;
			};
			/*
			if (recipeBrowser != null && !Main.dedServ)
			{
				recipeBrowser.Call(new object[5]
				{
				"AddItemCategory",
				"Astral",
				"Weapons",
				ModContent.Request<Texture2D>("StarsAbove/Items/Astral"), // 24x24 icon
				(Predicate<Item>)((Item item) =>
				{
				if (item.damage > 0)
				{
					return item.ModItem is Astral;
				}
				return false;
				})
				});
			}*/
			if (bossChecklist != null)
			{
				//Vagrant of Space and Time
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(VagrantBoss),
					2.9f,
					() => DownedBossSystem.downedVagrant,
					ModContent.NPCType<VagrantBoss>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.ShatteredDisk>(),
						["customPortrait"] = (SpriteBatch sb, Rectangle rect, Color color) =>
						{
							Texture2D texture = ModContent.Request<Texture2D>("StarsAbove/Bestiary/PerseusBossChecklist").Value;
							Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
							sb.Draw(texture, centered, color);
						}

					}
				);
				//Dioskouroi
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(PolluxBoss),
					9.5f,
					() => DownedBossSystem.downedDioskouroi,
					ModContent.NPCType<PolluxBoss>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.ShatteredDisk>(),
						["customPortrait"] = (SpriteBatch sb, Rectangle rect, Color color) =>
						{
							Texture2D texture = ModContent.Request<Texture2D>("StarsAbove/Bestiary/Dioskouroi_Bestiary").Value;
							Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
							sb.Draw(texture, centered, color);
						}
					}

				);
				//Penthesilea
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(Penthesilea),
					13.5f,
					() => DownedBossSystem.downedPenth,
					ModContent.NPCType<Penthesilea>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.UnsulliedCanvas>(),
					}
				);
				//Nalhaun
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(NalhaunBossPhase2),
					14.5f,
					() => DownedBossSystem.downedNalhaun,
					ModContent.NPCType<NalhaunBossPhase2>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.AncientShard>(),
						["customPortrait"] = (SpriteBatch sb, Rectangle rect, Color color) =>
						{
							Texture2D texture = ModContent.Request<Texture2D>("StarsAbove/Bestiary/Nalhaun_Bestiary").Value;
							Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
							sb.Draw(texture, centered, color);
						}
					}
				);
				//Arbitration
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(Arbitration),
					15.1f,
					() => DownedBossSystem.downedArbiter,
					ModContent.NPCType<Arbitration>(),
					new Dictionary<string, object>()
					{

					}
				);
				//The Warrior of Light
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(WarriorOfLightBoss),
					18.1f,
					() => DownedBossSystem.downedWarrior,
					ModContent.NPCType<WarriorOfLightBossFinalPhase>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.ProgenitorWish>(),
						["customPortrait"] = (SpriteBatch sb, Rectangle rect, Color color) =>
						{
							Texture2D texture = ModContent.Request<Texture2D>("StarsAbove/Bestiary/WarriorOfLight_Bestiary").Value;
							Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
							sb.Draw(texture, centered, color);
						}
					}
				);
				//Tsukiyomi
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(TsukiyomiBoss),
					18.3f,
					() => DownedBossSystem.downedTsuki,
					ModContent.NPCType<TsukiyomiBoss>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.MnemonicSigil>(),
						["availability"] = CanSeeTsuki,

					}
				);
			}
			if (musicDisplay != null)
            {
				/*
				void AddMusic(string path, string name) => musicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(this, path), name, "The Stars Above");

				AddMusic("Sounds/Music/CosmicWill", "PaperLuigi - Cosmic Will (Stars Above OST)");
				AddMusic("Sounds/Music/SunsetStardust", "PaperLuigi - Sunset, Stardust (Stars Above OST)");

				AddMusic("Sounds/Music/ElpisDay", "Masayoshi Soken - Sky Unsundered (FFXIV Endwalker OST)");
				AddMusic("Sounds/Music/MareLamentorum", "Masayoshi Soken - One Small Step (FFXIV Endwalker OST)");
				AddMusic("Sounds/Music/EverlastingLight", "Masayoshi Soken - Unmatching Pieces (FFXIV Endwalker OST)");
				AddMusic("Sounds/Music/ToTheEdge", "Masayoshi Soken - To The Edge (FFXIV Shadowbringers OST)");

				AddMusic("Sounds/Music/MageOfViolet", "Murasaki Shion - Mage of Violet (Instrumental)");

				AddMusic("Sounds/Music/ShadowsCastByTheMighty", "REVO - Shadows Cast By The Mighty: Swift Judgement (Bravely Default 2 OST)");
				AddMusic("Sounds/Music/TheMightOfTheHellblade", "REVO - The Might of The Hellblade (Bravely Default 2 OST)");

				AddMusic("Sounds/Music/FirstWarning", "Studio EIM - First Warning (LoR ver.) (Library of Ruina OST)");
				AddMusic("Sounds/Music/SecondWarning", "Studio EIM - Second Warning (LoR ver.) (Library of Ruina OST)");
				*/
			}
		}

        /*public override void UpdateMusic(ref int music, ref MusicPriority priority) Will be replaced by SceneEffects.
		{
			if (Main.myPlayer != -1 && !Main.gameMenu)
			{
				if (Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].GetModPlayer<StarsAbovePlayer>().stellarPerformanceActive == true)
				{
					
					

				}
				if (Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].HasBuff(ModContent.BuffType<Buffs.StellarListener>()) && !sharedAudio)
				{

					music = MusicLoader.GetMusicSlot(this, "Sounds/Music/NextColorPlanet");
					priority = MusicPriority.BossLow;

				}
				if (Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].HasBuff(ModContent.BuffType<Buffs.EverlastingLight>()))
				{

					music = MusicLoader.GetMusicSlot(this, "Sounds/Music/EverlastingLight");
					priority = MusicPriority.Event;

				}
				
				
				if (Main.player[Main.myPlayer].active && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SeaOfStars)
				{
					music = MusicLoader.GetMusicSlot(this, "Sounds/Music/MareLamentorum");
					priority = MusicPriority.BiomeHigh;
				}
				if (Main.player[Main.myPlayer].active && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().Observatory)
				{
					if (MusicMod != null)
					{

						music = MusicLoader.GetMusicSlot(this, "Sounds/Music/ElpisDay");
						priority = MusicPriority.BiomeHigh;
					}
					else
					{
						music = MusicLoader.GetMusicSlot(this, "Sounds/Music/MareLamentorum");
						priority = MusicPriority.BiomeHigh;
					}
					
				}
				if (Main.player[Main.myPlayer].active && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BleachedWorld)
				{

					music = MusicLoader.GetMusicSlot(this, "Sounds/Music/MareLamentorum");
					priority = MusicPriority.BiomeHigh;

				}
				if (Main.player[Main.myPlayer].active && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().City)
				{
					if (MusicMod != null)
					{

						music = MusicLoader.GetMusicSlot(this, "Sounds/Music/ACYBERSWORLD");
						priority = MusicPriority.BiomeHigh;
					}
					else
					{
						music = MusicLoader.GetMusicSlot(this, "Sounds/Music/MareLamentorum");
						priority = MusicPriority.BiomeHigh;
					}

				}
			}

			base.UpdateMusic(ref music, ref priority);
		}*/

        #region SubLib
        private static bool DenyRead(MessageBuffer buffer, int start, int length)
        {
            if (buffer.readBuffer[start + 2] == 250 && (ModNet.NetModCount < 256 ? buffer.readBuffer[start + 3] : BitConverter.ToUInt16(buffer.readBuffer, start + 3)) == ModContent.GetInstance<StarsAbove>().NetID)
            {
                return false;
            }
            if (!SubworldSystem.playerLocations.TryGetValue(Netplay.Clients[buffer.whoAmI].Socket, out int id))
            {
                return false;
            }

            Netplay.Clients[buffer.whoAmI].TimeOutTimer = 0;

            byte[] packet = new byte[length + 1];
            packet[0] = (byte)buffer.whoAmI;
            Buffer.BlockCopy(buffer.readBuffer, start, packet, 1, length);
            SubworldSystem.links[id].Send(packet);

            return packet[3] != 2 && (packet[3] != 82 || BitConverter.ToUInt16(packet, 4) != NetManager.Instance.GetId<NetBestiaryModule>());
        }

        private static bool DenySend(ISocket socket, byte[] data, int start, int length, ref object state)
        {
            return Thread.CurrentThread.Name != "Subserver Packets" && SubworldSystem.playerLocations.ContainsKey(socket);
        }

        private static void Sleep(Stopwatch stopwatch, double delta, ref double target)
        {
            double now = stopwatch.ElapsedMilliseconds;
            double remaining = target - now;
            target += delta;
            if (target < now)
            {
                target = now + delta;
            }
            if (remaining <= 0)
            {
                Thread.Sleep(0);
                return;
            }
            Thread.Sleep((int)remaining);
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            if (Main.netMode == 2)
            {
                RemoteClient client = Netplay.Clients[whoAmI];

                if (SubworldSystem.current != null)
                {
                    Main.player[whoAmI] = new Player();

                    client.IsActive = false;
                    client.Socket = null;
                    client.State = 0;
                    client.ResetSections();
                    client.SpamClear();

                    NetMessage.SyncDisconnectedPlayer(whoAmI);

                    bool connection = false;
                    for (int i = 0; i < 255; i++)
                    {
                        if (Netplay.Clients[i].State > 0)
                        {
                            connection = true;
                            break;
                        }
                    }
                    if (!connection)
                    {
                        Netplay.Disconnect = true;
                        Netplay.HasClients = false;
                    }

                    return;
                }

                ushort id = reader.ReadUInt16();
                ModPacket packet;

                bool inSubworld = SubworldSystem.playerLocations.TryGetValue(client.Socket, out int location);
                if (inSubworld)
                {
                    byte[] data = new byte[5] { (byte)whoAmI, 4, 0, 250, (byte)NetID }; // client, (ushort) size, packet id, sublib net id
                    SubworldSystem.links[location].Send(data);

                    if (id == ushort.MaxValue)
                    {
                        SubworldSystem.playerLocations.Remove(client.Socket);
                        client.State = 0;

                        packet = GetPacket();
                        packet.Write(id);
                        packet.Send(whoAmI);

                        return;
                    }
                }
                if (id == ushort.MaxValue)
                {
                    return;
                }

                packet = GetPacket();
                packet.Write(id);
                packet.Send(whoAmI);

                // this respects the vanilla call order

                if (!inSubworld)
                {
                    Main.player[whoAmI].active = false;
                    NetMessage.SendData(14, -1, whoAmI, null, whoAmI, 0);
                }

                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.SubworldLibrary.Move", client.Name, SubworldSystem.subworlds[id].DisplayName), new Color(255, 240, 20), whoAmI);

                if (!inSubworld)
                {
                    Player.Hooks.PlayerDisconnect(whoAmI);
                }

                SubworldSystem.playerLocations[client.Socket] = id;
                client.ResetSections();

                SubworldSystem.StartSubserver(id);
            }
            else
            {
                ushort id = reader.ReadUInt16();
                Task.Factory.StartNew(SubworldSystem.ExitWorldCallBack, id < ushort.MaxValue ? id : -1);
            }
        }
        
    }

	public interface ICopyWorldData : ILoadable
	{
		/// <summary>
		/// Called on all content with this interface before <see cref="Subworld.OnEnter"/>, and after <see cref="Subworld.OnExit"/>.
		/// <br/>This is where you copy data from the main world to a subworld, via <see cref="SubworldSystem.CopyWorldData"/>.
		/// <code>SubworldSystem.CopyWorldData(nameof(DownedSystem.downedBoss), DownedSystem.downedBoss);</code>
		/// </summary>
		void CopyMainWorldData() { }

		/// <summary>
		/// Called on all content with this interface before a subworld generates, or after a subworld loads from file.
		/// <br/>This is where you read data copied from the main world to a subworld, via <see cref="SubworldSystem.ReadCopiedWorldData"/>.
		/// <code>DownedSystem.downedBoss = SubworldSystem.ReadCopiedWorldData&lt;bool&gt;(nameof(DownedSystem.downedBoss));</code>
		/// </summary>
		void ReadCopiedMainWorldData() { }
	}

	public abstract class Subworld : ModType, ICopyWorldData, ILocalizedModType
	{
		public string LocalizationCategory => "Subworlds";
		public virtual LocalizedText DisplayName => this.GetLocalization(nameof(DisplayName), PrettyPrintName);

        public override void SetStaticDefaults()
		{
			SubworldSystem.subworlds.Add(this);

			base.SetStaticDefaults();
        }
        protected sealed override void Register()
		{
			ModTypeLookup<Subworld>.Register(this);
		}
		public sealed override void SetupContent() => SetStaticDefaults();

		/// <summary>
		/// The subworld's width.
		/// </summary>
		public abstract int Width { get; }
		/// <summary>
		/// The subworld's height.
		/// </summary>
		public abstract int Height { get; }
		/// <summary>
		/// The subworld's generation tasks.
		/// </summary>
		public abstract List<GenPass> Tasks { get; }
		public virtual WorldGenConfiguration Config => null;
		/// <summary>
		/// The index of the subworld the player will be sent to when choosing to return. See <see cref="SubworldSystem.GetIndex{T}"/>.
		/// <br/>Set to -1 to send the player back to the main world.
		/// <br/>Set to <see cref="int.MinValue"/> to send the player to the main menu.
		/// <br/>Default: -1
		/// </summary>
		public virtual int ReturnDestination => -1;
		/// <summary>
		/// Whether the subworld should save or not.
		/// <br/>Default: false
		/// </summary>
		public virtual bool ShouldSave => false;
		/// <summary>
		/// Reverts changes to players when they leave the subworld.
		/// <br/>Default: false
		/// </summary>
		public virtual bool NoPlayerSaving => false;
		/// <summary>
		/// Completely disables vanilla world updating in the subworld.
		/// <br/>Do not enable unless you are replicating a standard world!
		/// <br/>Default: false
		/// </summary>
		public virtual bool NormalUpdates => false;
		/// <summary>
		/// If <see cref="ChangeAudio"/> returns true, this completely disables vanilla audio updating.
		/// <br/>Typically not required. Only enable this if you know what you are doing.
		/// <br/>Default: false
		/// </summary>
		public virtual bool ManualAudioUpdates => false;
		/// <summary>
		/// Called when entering a subworld.
		/// <br/>Before this is called, the return button and underworld's visibility are reset.
		/// </summary>
		public virtual void OnEnter() { }
		/// <summary>
		/// Called when exiting a subworld.
		/// <br/>After this is called, the return button and underworld's visibility are reset.
		/// </summary>
		public virtual void OnExit() { }
		/// <summary>
		/// Called on all subworlds before <see cref="OnEnter"/>, and after <see cref="OnExit"/>.
		/// <br/>This is where you copy data from the main world to the subworld, via <see cref="SubworldSystem.CopyWorldData"/>.
		/// <code>SubworldSystem.CopyWorldData(nameof(DownedSystem.downedBoss), DownedSystem.downedBoss);</code>
		/// </summary>
		public virtual void CopyMainWorldData() { }
		/// <summary>
		/// Called before <see cref="OnExit"/>.
		/// <br/>This is where you copy data from the subworld to another world, via <see cref="SubworldSystem.CopyWorldData"/>.
		/// <code>SubworldSystem.CopyWorldData(nameof(DownedSystem.downedBoss), DownedSystem.downedBoss);</code>
		/// </summary>
		public virtual void CopySubworldData() { }
		/// <summary>
		/// Called on all subworlds before one generates, or after one loads from file.
		/// <br/>This is where you read data copied from the main world to the subworld, via <see cref="SubworldSystem.ReadCopiedWorldData"/>.
		/// <code>DownedSystem.downedBoss = SubworldSystem.ReadCopiedWorldData&lt;bool&gt;(nameof(DownedSystem.downedBoss));</code>
		/// </summary>
		public virtual void ReadCopiedMainWorldData() { }
		/// <summary>
		/// Called while leaving the subworld, either before a different world generates, or after a different world loads from file.
		/// <br/>This is where you read data copied from the subworld to another world, via <see cref="SubworldSystem.ReadCopiedWorldData"/>.
		/// <code>DownedSystem.downedBoss = SubworldSystem.ReadCopiedWorldData&lt;bool&gt;(nameof(DownedSystem.downedBoss));</code>
		/// </summary>
		public virtual void ReadCopiedSubworldData() { }
		/// <summary>
		/// Called after the subworld generates or loads from file.
		/// </summary>
		public virtual void OnLoad() { }
		/// <summary>
		/// Called while leaving the subworld, before a different world either generates or loads from file.
		/// </summary>
		public virtual void OnUnload() { }
		/// <summary>
		/// Requires knowledge of how vanilla world file loading works to use properly! Only override this if you know what you are doing.
		/// </summary>
		/// <returns>The exit status. A number above 0 indicates that world file reading has failed.</returns>
		public virtual int ReadFile(BinaryReader reader)
		{
			int status = WorldFile.LoadWorld_Version2(reader);
			Main.ActiveWorldFileData.Name = Main.worldName;
			Main.ActiveWorldFileData.Metadata = Main.WorldFileMetadata;
			return status;
		}
		/// <summary>
		/// Requires knowledge of how vanilla world file loading works to use properly! Only override this if you know what you are doing.
		/// </summary>
		public virtual void PostReadFile()
		{
			SubworldSystem.PostLoadWorldFile();
		}
		/// <summary>
		/// Corrects zoom and clears the screen, then calls DrawMenu and draws the cursor.
		/// <code>
		/// PlayerInput.SetZoom_Unscaled();
		/// Main.instance.GraphicsDevice.Clear(Color.Black);
		/// Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
		/// DrawMenu(gameTime);
		/// Main.DrawCursor(Main.DrawThickCursor());
		/// Main.spriteBatch.End();
		/// </code>
		/// </summary>
		public virtual void DrawSetup(GameTime gameTime)
		{
			PlayerInput.SetZoom_Unscaled();

			Main.instance.GraphicsDevice.Clear(Color.Black);

			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);

			DrawMenu(gameTime);
			Main.DrawCursor(Main.DrawThickCursor());

			Main.spriteBatch.End();
		}
		/// <summary>
		/// Called by DrawSetup to draw the subworld's loading menu.
		/// <br/>Defaults to text on a black background.
		/// </summary>
		public virtual void DrawMenu(GameTime gameTime)
		{
			Main.spriteBatch.DrawString(FontAssets.DeathText.Value, Main.statusText, new Vector2(Main.screenWidth, Main.screenHeight) / 2 - FontAssets.DeathText.Value.MeasureString(Main.statusText) / 2, Color.White);
		}
		/// <summary>
		/// Called before music is chosen, including in the loading menu.
		/// <br/>Return true to disable vanilla behaviour, allowing for modification of variables such as <see cref="Main.newMusic"/>.
		/// <br/>Default: false
		/// </summary>
		public virtual bool ChangeAudio() => false;
		/// <summary>
		/// Controls the gravity of an entity in the subworld.
		/// <br/>Default: 1
		/// </summary>
		public virtual float GetGravity(Entity entity) => 1;
		/// <summary>
		/// Controls how a tile in the subworld is lit.
		/// <br/>Return true to disable vanilla behaviour.
		/// <br/>Default: false
		/// </summary>
		public virtual bool GetLight(Tile tile, int x, int y, ref FastRandom rand, ref Vector3 color) => false;
	}

	internal class SubserverSocket : ISocket
	{
		private int index;

		internal static NamedPipeClientStream pipe;

		public SubserverSocket(int index)
		{
			this.index = index;
		}

		void ISocket.AsyncReceive(byte[] data, int offset, int size, SocketReceiveCallback callback, object state = null) { }

		void ISocket.AsyncSend(byte[] data, int offset, int size, SocketSendCallback callback, object state = null)
		{
			byte[] packet = new byte[size + 1];
			packet[0] = (byte)index;
			Buffer.BlockCopy(data, offset, packet, 1, size);
			pipe.Write(packet);
		}

		void ISocket.Close() { }

		void ISocket.Connect(RemoteAddress address) { }

		RemoteAddress ISocket.GetRemoteAddress() => new TcpAddress(IPAddress.Any, 0);

		bool ISocket.IsConnected() => true;

		bool ISocket.IsDataAvailable() => false;

		void ISocket.SendQueuedPackets() { }

		bool ISocket.StartListening(SocketConnectionAccepted callback) => true;

		void ISocket.StopListening() { }
	}

	internal class SubserverLink
	{
		public readonly NamedPipeClientStream pipe;
		private List<byte[]> queue;

		public SubserverLink(string name)
		{
			pipe = new NamedPipeClientStream(".", name + ".IN", PipeDirection.Out);
			queue = new List<byte[]>(16);
		}

		public bool QueueData(byte[] data)
		{
			if (queue == null)
			{
				return false;
			}

			queue.Add(data);
			return true;
		}

		public void Send(byte[] data)
		{
			lock (this)
			{
				if (QueueData(data))
				{
					return;
				}
			}
			pipe.Write(data);
		}

		public void ConnectAndProcessQueue()
		{
			pipe.Connect();
			pipe.Write(queue[0]);
			lock (this)
			{
				int size = 0;
				for (int i = 1; i < queue.Count; i++)
				{
					size += queue[i].Length;
				}

				byte[] bytes = new byte[size];
				size = 0;
				for (int i = 1; i < queue.Count; i++)
				{
					Buffer.BlockCopy(queue[i], 0, bytes, size, queue[i].Length);
					size += queue[i].Length;
				}
				pipe.Write(bytes);

				queue = null;
			}
		}
	}

	public class SubworldSystem : ModSystem
	{
		internal static List<Subworld> subworlds;

		internal static Subworld current;
		internal static Subworld cache;
		internal static WorldFileData main;

		internal static TagCompound copiedData;

		internal static Dictionary<ISocket, int> playerLocations;
		internal static Dictionary<int, SubserverLink> links;

		public override void OnModLoad()
		{
			subworlds = new List<Subworld>();
			Player.Hooks.OnEnterWorld += OnEnterWorld;
			Netplay.OnDisconnect += OnDisconnect;
		}

		public override void Unload()
		{
			Player.Hooks.OnEnterWorld -= OnEnterWorld;
			Netplay.OnDisconnect -= OnDisconnect;
		}

		/// <summary>
		/// Hides the Return button.
		/// <br/>Its value is reset before <see cref="Subworld.OnEnter"/> is called, and after <see cref="Subworld.OnExit"/> is called.
		/// </summary>
		public static bool noReturn;
		/// <summary>
		/// Hides the Underworld background.
		/// <br/>Its value is reset before <see cref="Subworld.OnEnter"/> is called, and after <see cref="Subworld.OnExit"/> is called.
		/// </summary>
		public static bool hideUnderworld;

		/// <summary>
		/// The current subworld.
		/// </summary>
		public static Subworld Current => current;
		/// <summary>
		/// Returns true if the current subworld's ID matches the specified ID.
		/// <code>SubworldSystem.IsActive("MyMod/MySubworld")</code>
		/// </summary>
		public static bool IsActive(string id) => current?.FullName == id;
		/// <summary>
		/// Returns true if the specified subworld is active.
		/// </summary>
		public static bool IsActive<T>() where T : Subworld => current?.GetType() == typeof(T);
		/// <summary>
		/// Returns true if the current subworld is from the specified mod.
		/// </summary>
		public static bool AnyActive(Mod mod) => current?.Mod == mod;
		/// <summary>
		/// Returns true if the current subworld is from the specified mod.
		/// </summary>
		public static bool AnyActive<T>() where T : Mod => current?.Mod == ModContent.GetInstance<T>();
		/// <summary>
		/// The current subworld's file path.
		/// </summary>
		public static string CurrentPath => Path.Combine(Main.WorldPath, Path.GetFileNameWithoutExtension(main.Path), current.Mod.Name + "_" + current.Name + ".wld");

		/// <summary>
		/// Tries to enter the subworld with the specified ID.
		/// <code>SubworldSystem.Enter("MyMod/MySubworld")</code>
		/// </summary>
		public static bool Enter(string id)
		{
			if (current == cache)
			{
				for (int i = 0; i < subworlds.Count; i++)
				{
					if (subworlds[i].FullName == id)
					{
						BeginEntering(i);
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Enters the specified subworld.
		/// </summary>
		public static bool Enter<T>() where T : Subworld
		{
			if (current == cache)
			{
				for (int i = 0; i < subworlds.Count; i++)
				{
					if (subworlds[i].GetType() == typeof(T))
					{
						BeginEntering(i);
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Exits the current subworld.
		/// </summary>
		public static void Exit()
		{
			if (current != null && current == cache)
			{
				BeginEntering(current.ReturnDestination);
			}
		}

		/// <summary>
		/// Tries to get the index of the subworld with the specified ID.
		/// <br/> Typically used for <see cref="Subworld.ReturnDestination"/>.
		/// <br/> Returns <see cref="int.MinValue"/> if the subworld couldn't be found.
		/// <code>public override int ReturnDestination => SubworldSystem.GetIndex("MyMod/MySubworld");</code>
		/// </summary>
		public static int GetIndex(string id)
		{
			for (int i = 0; i < subworlds.Count; i++)
			{
				if (subworlds[i].FullName == id)
				{
					return i;
				}
			}
			return int.MinValue;
		}

		/// <summary>
		/// Gets the index of the specified subworld.
		/// <br/> Typically used for <see cref="Subworld.ReturnDestination"/>.
		/// </summary>
		public static int GetIndex<T>() where T : Subworld
		{
			for (int i = 0; i < subworlds.Count; i++)
			{
				if (subworlds[i].GetType() == typeof(T))
				{
					return i;
				}
			}
			return int.MinValue;
		}

		/// <summary>
		/// Can only be called in <see cref="Subworld.CopyMainWorldData"/> or <see cref="Subworld.OnExit"/>!
		/// <br/>Stores data to be transferred between worlds under the specified key, if that key is not already in use.
		/// <br/>Naming the key after the variable pointing to the data is highly recommended to avoid redundant copying. This can be done automatically with nameof().
		/// <code>SubworldSystem.CopyWorldData(nameof(DownedSystem.downedBoss), DownedSystem.downedBoss);</code>
		/// </summary>
		public static void CopyWorldData(string key, object data)
		{
			if (data != null && !copiedData.ContainsKey(key))
			{
				copiedData[key] = data;
			}
		}

		/// <summary>
		/// Can only be called in <see cref="Subworld.ReadCopiedMainWorldData"/> or <see cref="Subworld.ReadCopiedSubworldData"/>!
		/// <br/>Reads data copied from another world stored under the specified key.
		/// <code>DownedSystem.downedBoss = SubworldSystem.ReadCopiedWorldData&lt;bool&gt;(nameof(DownedSystem.downedBoss));</code>
		/// </summary>
		public static T ReadCopiedWorldData<T>(string key) => copiedData.Get<T>(key);

		/// <summary>
		/// Sends a packet from the specified mod directly to the main server.
		/// </summary>
		public static void SendToMainServer(Mod mod, byte[] data)
		{
			int extra = ModNet.NetModCount < 256 ? 6 : 8;
			byte[] packet = new byte[data.Length + extra];

			int i = 0;
			packet[i++] = 0;

			packet[i++] = (byte)packet.Length;
			packet[i++] = (byte)(packet.Length >> 8);

			packet[i++] = 250;

			short netID = ModContent.GetInstance<StarsAbove>().NetID;

			packet[i++] = (byte)netID;
			if (ModNet.NetModCount >= 256)
			{
				packet[i++] = (byte)(netID >> 8);
				packet[i + 1] = (byte)(mod.NetID >> 8);
			}

			packet[i] = (byte)mod.NetID;

			Buffer.BlockCopy(data, 0, packet, extra, data.Length);
			SubserverSocket.pipe.Write(packet);
		}

		private static void EraseSubworlds(int index)
		{
			WorldFileData world = Main.WorldList[index];
			string path = Path.Combine(Main.WorldPath, Path.GetFileNameWithoutExtension(world.Path));
			if (FileUtilities.Exists(path, world.IsCloudSave))
			{
				FileUtilities.Delete(path, world.IsCloudSave);
			}
		}

		private static bool ChangeAudio()
		{
			if (current != null)
			{
				return current.ChangeAudio();
			}
			if (cache != null)
			{
				return cache.ChangeAudio();
			}
			return false;
		}

		private static bool ManualAudioUpdates()
		{
			if (current != null)
			{
				return current.ManualAudioUpdates;
			}
			if (cache != null)
			{
				return cache.ManualAudioUpdates;
			}
			return false;
		}

		private static void BeginEntering(int index)
		{
			if (index == int.MinValue && Main.netMode != 2)
			{
				Task.Factory.StartNew(ExitWorldCallBack, null);
				return;
			}

			if (Main.netMode == 0)
			{
				if (current == null && index >= 0)
				{
					main = Main.ActiveWorldFileData;
				}
				Task.Factory.StartNew(ExitWorldCallBack, index);
			}
			else if (Main.netMode == 1)
			{
				ModPacket packet = ModContent.GetInstance<StarsAbove>().GetPacket();
				packet.Write(index < 0 ? ushort.MaxValue : (ushort)index);
				packet.Send();
			}
		}

		private static void CopyMainWorldData()
		{
			copiedData["seed"] = Main.ActiveWorldFileData.SeedText;
			copiedData["gameMode"] = Main.ActiveWorldFileData.GameMode;
			copiedData["hardMode"] = Main.hardMode;

			// it's called reflection because the code is ugly like you
			using (MemoryStream stream = new MemoryStream())
			{
				using BinaryWriter writer = new BinaryWriter(stream);

				FieldInfo field = typeof(NPCKillsTracker).GetField("_killCountsByNpcId", BindingFlags.NonPublic | BindingFlags.Instance);
				Dictionary<string, int> kills = (Dictionary<string, int>)field.GetValue(Main.BestiaryTracker.Kills);

				writer.Write(kills.Count);
				foreach (KeyValuePair<string, int> item in kills)
				{
					writer.Write(item.Key);
					writer.Write(item.Value);
				}

				field = typeof(NPCWasNearPlayerTracker).GetField("_wasNearPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
				HashSet<string> sights = (HashSet<string>)field.GetValue(Main.BestiaryTracker.Sights);

				writer.Write(sights.Count);
				foreach (string item in sights)
				{
					writer.Write(item);
				}

				field = typeof(NPCWasChatWithTracker).GetField("_chattedWithPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
				HashSet<string> chats = (HashSet<string>)field.GetValue(Main.BestiaryTracker.Chats);

				writer.Write(chats.Count);
				foreach (string item in chats)
				{
					writer.Write(item);
				}

				copiedData["bestiary"] = stream.GetBuffer();
			}
			using (MemoryStream stream = new MemoryStream())
			{
				using BinaryWriter writer = new BinaryWriter(stream);

				FieldInfo field = typeof(CreativePowerManager).GetField("_powersById", BindingFlags.NonPublic | BindingFlags.Instance);
				foreach (KeyValuePair<ushort, ICreativePower> item in (Dictionary<ushort, ICreativePower>)field.GetValue(CreativePowerManager.Instance))
				{
					if (item.Value is IPersistentPerWorldContent power)
					{
						writer.Write((ushort)(item.Key + 1));
						power.Save(writer);
					}
				}
				writer.Write((ushort)0);

				copiedData["powers"] = stream.GetBuffer();
			}

			copiedData[nameof(Main.drunkWorld)] = Main.drunkWorld;
			copiedData[nameof(Main.getGoodWorld)] = Main.getGoodWorld;
			copiedData[nameof(Main.tenthAnniversaryWorld)] = Main.tenthAnniversaryWorld;
			copiedData[nameof(Main.dontStarveWorld)] = Main.dontStarveWorld;
			copiedData[nameof(Main.notTheBeesWorld)] = Main.notTheBeesWorld;
			copiedData[nameof(Main.remixWorld)] = Main.remixWorld;
			copiedData[nameof(Main.noTrapsWorld)] = Main.noTrapsWorld;
			copiedData[nameof(Main.zenithWorld)] = Main.zenithWorld;

			CopyDowned();

			foreach (ICopyWorldData data in ModContent.GetContent<ICopyWorldData>())
			{
				data.CopyMainWorldData();
			}
		}

		private static void ReadCopiedMainWorldData()
		{
			Main.ActiveWorldFileData.SetSeed(copiedData.Get<string>("seed"));
			Main.GameMode = copiedData.Get<int>("gameMode");
			Main.hardMode = copiedData.Get<bool>("hardMode");

			// i'm sorry that was mean
			using (MemoryStream stream = new MemoryStream(copiedData.Get<byte[]>("bestiary")))
			{
				using BinaryReader reader = new BinaryReader(stream);

				FieldInfo field = typeof(NPCKillsTracker).GetField("_killCountsByNpcId", BindingFlags.NonPublic | BindingFlags.Instance);
				Dictionary<string, int> kills = (Dictionary<string, int>)field.GetValue(Main.BestiaryTracker.Kills);

				int count = reader.ReadInt32();
				for (int i = 0; i < count; i++)
				{
					kills[reader.ReadString()] = reader.ReadInt32();
				}

				field = typeof(NPCWasNearPlayerTracker).GetField("_wasNearPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
				HashSet<string> sights = (HashSet<string>)field.GetValue(Main.BestiaryTracker.Sights);

				count = reader.ReadInt32();
				for (int i = 0; i < count; i++)
				{
					sights.Add(reader.ReadString());
				}

				field = typeof(NPCWasChatWithTracker).GetField("_chattedWithPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
				HashSet<string> chats = (HashSet<string>)field.GetValue(Main.BestiaryTracker.Chats);

				count = reader.ReadInt32();
				for (int i = 0; i < count; i++)
				{
					chats.Add(reader.ReadString());
				}
			}
			using (MemoryStream stream = new MemoryStream(copiedData.Get<byte[]>("powers")))
			{
				using BinaryReader reader = new BinaryReader(stream);

				FieldInfo field = typeof(CreativePowerManager).GetField("_powersById", BindingFlags.NonPublic | BindingFlags.Instance);
				Dictionary<ushort, ICreativePower> powers = (Dictionary<ushort, ICreativePower>)field.GetValue(CreativePowerManager.Instance);

				ushort id;
				while ((id = reader.ReadUInt16()) > 0)
				{
					((IPersistentPerWorldContent)powers[(ushort)(id - 1)]).Load(reader, 0);
				}
			}

			Main.drunkWorld = copiedData.Get<bool>(nameof(Main.drunkWorld));
			Main.getGoodWorld = copiedData.Get<bool>(nameof(Main.getGoodWorld));
			Main.tenthAnniversaryWorld = copiedData.Get<bool>(nameof(Main.tenthAnniversaryWorld));
			Main.dontStarveWorld = copiedData.Get<bool>(nameof(Main.dontStarveWorld));
			Main.notTheBeesWorld = copiedData.Get<bool>(nameof(Main.notTheBeesWorld));
			Main.remixWorld = copiedData.Get<bool>(nameof(Main.remixWorld));
			Main.noTrapsWorld = copiedData.Get<bool>(nameof(Main.noTrapsWorld));
			Main.zenithWorld = copiedData.Get<bool>(nameof(Main.zenithWorld));

			ReadCopiedDowned();

			foreach (ICopyWorldData data in ModContent.GetContent<ICopyWorldData>())
			{
				data.ReadCopiedMainWorldData();
			}
		}

		private static void CopyDowned()
		{
			copiedData[nameof(NPC.downedSlimeKing)] = NPC.downedSlimeKing;

			copiedData[nameof(NPC.downedBoss1)] = NPC.downedBoss1;
			copiedData[nameof(NPC.downedBoss2)] = NPC.downedBoss2;
			copiedData[nameof(NPC.downedBoss3)] = NPC.downedBoss3;

			copiedData[nameof(NPC.downedQueenBee)] = NPC.downedQueenBee;
			copiedData[nameof(NPC.downedDeerclops)] = NPC.downedDeerclops;

			copiedData[nameof(NPC.downedQueenSlime)] = NPC.downedQueenSlime;

			copiedData[nameof(NPC.downedMechBoss1)] = NPC.downedMechBoss1;
			copiedData[nameof(NPC.downedMechBoss2)] = NPC.downedMechBoss2;
			copiedData[nameof(NPC.downedMechBoss3)] = NPC.downedMechBoss3;
			copiedData[nameof(NPC.downedMechBossAny)] = NPC.downedMechBossAny;

			copiedData[nameof(NPC.downedPlantBoss)] = NPC.downedPlantBoss;
			copiedData[nameof(NPC.downedGolemBoss)] = NPC.downedGolemBoss;

			copiedData[nameof(NPC.downedFishron)] = NPC.downedFishron;
			copiedData[nameof(NPC.downedEmpressOfLight)] = NPC.downedEmpressOfLight;

			copiedData[nameof(NPC.downedAncientCultist)] = NPC.downedAncientCultist;

			copiedData[nameof(NPC.downedTowerSolar)] = NPC.downedTowerSolar;
			copiedData[nameof(NPC.downedTowerVortex)] = NPC.downedTowerVortex;
			copiedData[nameof(NPC.downedTowerNebula)] = NPC.downedTowerNebula;
			copiedData[nameof(NPC.downedTowerStardust)] = NPC.downedTowerStardust;

			copiedData[nameof(NPC.downedMoonlord)] = NPC.downedMoonlord;

			copiedData[nameof(NPC.downedGoblins)] = NPC.downedGoblins;
			copiedData[nameof(NPC.downedClown)] = NPC.downedClown;
			copiedData[nameof(NPC.downedFrost)] = NPC.downedFrost;
			copiedData[nameof(NPC.downedPirates)] = NPC.downedPirates;
			copiedData[nameof(NPC.downedMartians)] = NPC.downedMartians;

			copiedData[nameof(NPC.downedHalloweenTree)] = NPC.downedHalloweenTree;
			copiedData[nameof(NPC.downedHalloweenKing)] = NPC.downedHalloweenKing;

			copiedData[nameof(NPC.downedChristmasTree)] = NPC.downedChristmasTree;
			copiedData[nameof(NPC.downedChristmasSantank)] = NPC.downedChristmasSantank;
			copiedData[nameof(NPC.downedChristmasIceQueen)] = NPC.downedChristmasIceQueen;

			copiedData[nameof(DD2Event.DownedInvasionT1)] = DD2Event.DownedInvasionT1;
			copiedData[nameof(DD2Event.DownedInvasionT2)] = DD2Event.DownedInvasionT2;
			copiedData[nameof(DD2Event.DownedInvasionT3)] = DD2Event.DownedInvasionT3;
		}

		private static void ReadCopiedDowned()
		{
			NPC.downedSlimeKing = copiedData.Get<bool>(nameof(NPC.downedSlimeKing));

			NPC.downedBoss1 = copiedData.Get<bool>(nameof(NPC.downedBoss1));
			NPC.downedBoss2 = copiedData.Get<bool>(nameof(NPC.downedBoss2));
			NPC.downedBoss3 = copiedData.Get<bool>(nameof(NPC.downedBoss3));

			NPC.downedQueenBee = copiedData.Get<bool>(nameof(NPC.downedQueenBee));
			NPC.downedDeerclops = copiedData.Get<bool>(nameof(NPC.downedDeerclops));

			NPC.downedQueenSlime = copiedData.Get<bool>(nameof(NPC.downedQueenSlime));

			NPC.downedMechBoss1 = copiedData.Get<bool>(nameof(NPC.downedMechBoss1));
			NPC.downedMechBoss2 = copiedData.Get<bool>(nameof(NPC.downedMechBoss2));
			NPC.downedMechBoss3 = copiedData.Get<bool>(nameof(NPC.downedMechBoss3));
			NPC.downedMechBossAny = copiedData.Get<bool>(nameof(NPC.downedMechBossAny));

			NPC.downedPlantBoss = copiedData.Get<bool>(nameof(NPC.downedPlantBoss));
			NPC.downedGolemBoss = copiedData.Get<bool>(nameof(NPC.downedGolemBoss));

			NPC.downedFishron = copiedData.Get<bool>(nameof(NPC.downedFishron));
			NPC.downedEmpressOfLight = copiedData.Get<bool>(nameof(NPC.downedEmpressOfLight));

			NPC.downedAncientCultist = copiedData.Get<bool>(nameof(NPC.downedAncientCultist));

			NPC.downedTowerSolar = copiedData.Get<bool>(nameof(NPC.downedTowerSolar));
			NPC.downedTowerVortex = copiedData.Get<bool>(nameof(NPC.downedTowerVortex));
			NPC.downedTowerNebula = copiedData.Get<bool>(nameof(NPC.downedTowerNebula));
			NPC.downedTowerStardust = copiedData.Get<bool>(nameof(NPC.downedTowerStardust));

			NPC.downedMoonlord = copiedData.Get<bool>(nameof(NPC.downedMoonlord));

			NPC.downedGoblins = copiedData.Get<bool>(nameof(NPC.downedGoblins));
			NPC.downedClown = copiedData.Get<bool>(nameof(NPC.downedClown));
			NPC.downedFrost = copiedData.Get<bool>(nameof(NPC.downedFrost));
			NPC.downedPirates = copiedData.Get<bool>(nameof(NPC.downedPirates));
			NPC.downedMartians = copiedData.Get<bool>(nameof(NPC.downedMartians));

			NPC.downedHalloweenTree = copiedData.Get<bool>(nameof(NPC.downedHalloweenTree));
			NPC.downedHalloweenKing = copiedData.Get<bool>(nameof(NPC.downedHalloweenKing));

			NPC.downedChristmasTree = copiedData.Get<bool>(nameof(NPC.downedChristmasTree));
			NPC.downedChristmasSantank = copiedData.Get<bool>(nameof(NPC.downedChristmasSantank));
			NPC.downedChristmasIceQueen = copiedData.Get<bool>(nameof(NPC.downedChristmasIceQueen));

			DD2Event.DownedInvasionT1 = copiedData.Get<bool>(nameof(DD2Event.DownedInvasionT1));
			DD2Event.DownedInvasionT2 = copiedData.Get<bool>(nameof(DD2Event.DownedInvasionT2));
			DD2Event.DownedInvasionT3 = copiedData.Get<bool>(nameof(DD2Event.DownedInvasionT3));
		}

		public static void StartSubserver(int id)
		{
			if (links.ContainsKey(id))
			{
				return;
			}

			Process p = new Process();
			p.StartInfo.FileName = Process.GetCurrentProcess().MainModule!.FileName;
			p.StartInfo.Arguments = "tModLoader.dll -server -showserverconsole -world \"" + Main.worldPathName + "\" -subworld \"" + subworlds[id].FullName + "\"";
			p.StartInfo.UseShellExecute = true;
			p.Start();

			new Thread(MainServerCallBack)
			{
				Name = "Subserver Packets",
				IsBackground = true
			}.Start(id);

			links[id] = new SubserverLink(subworlds[id].FullName);

			copiedData = new TagCompound();
			CopyMainWorldData();

			using (MemoryStream stream = new MemoryStream())
			{
				TagIO.ToStream(copiedData, stream);
				links[id].QueueData(stream.ToArray());
				copiedData = null;
			}

			Task.Run(links[id].ConnectAndProcessQueue);
		}

		private static void MainServerCallBack(object id)
		{
			NamedPipeServerStream pipe = new NamedPipeServerStream(subworlds[(int)id].FullName + ".OUT", PipeDirection.In);
			try
			{
				pipe.WaitForConnection();

				while (pipe.IsConnected)
				{
					byte[] packetInfo = new byte[3];
					if (pipe.Read(packetInfo) < 3)
					{
						break;
					}

					byte low = packetInfo[1];
					byte high = packetInfo[2];
					int length = (high << 8) | low;

					byte[] data = new byte[length];
					pipe.Read(data, 2, length - 2);
					data[0] = low;
					data[1] = high;

					if (data[2] == 250 && (ModNet.NetModCount < 256 ? data[3] : BitConverter.ToUInt16(data, 3)) == ModContent.GetInstance<StarsAbove>().NetID)
					{
						MemoryStream stream = new MemoryStream(data);
						using BinaryReader reader = new BinaryReader(stream);
						if (ModNet.NetModCount < 256)
						{
							stream.Position = 5;
							ModNet.GetMod(data[4]).HandlePacket(reader, 256);
						}
						else
						{
							stream.Position = 6;
							ModNet.GetMod(BitConverter.ToUInt16(data, 5)).HandlePacket(reader, 256);
						}
					}
					else
					{
						Netplay.Clients[packetInfo[0]].Socket.AsyncSend(data, 0, length, (state) => { });
					}
				}
			}
			finally
			{
				pipe.Close();
				links[(int)id].pipe.Close();
				links.Remove((int)id);
			}
		}

		private static bool LoadIntoSubworld()
		{
			if (Program.LaunchParameters.TryGetValue("-subworld", out string id))
			{
				for (int i = 0; i < subworlds.Count; i++)
				{
					if (subworlds[i].FullName == id)
					{
						Main.myPlayer = 255;
						main = Main.ActiveWorldFileData;
						current = subworlds[i];

						NamedPipeServerStream pipe = new NamedPipeServerStream(current.FullName + ".IN", PipeDirection.In);
						pipe.WaitForConnection();

						copiedData = TagIO.FromStream(pipe);
						LoadWorld();
						copiedData = null;

						for (int j = 0; j < Netplay.Clients.Length; j++)
						{
							Netplay.Clients[j].Id = j;
							Netplay.Clients[j].Reset();
							Netplay.Clients[j].ReadBuffer = null; // not used by subservers, saves 262kb total
						}

						new Thread(SubserverCallBack)
						{
							IsBackground = true
						}.Start(pipe);

						SubserverSocket.pipe = new NamedPipeClientStream(".", current.FullName + ".OUT", PipeDirection.Out);
						SubserverSocket.pipe.Connect();

						return true;
					}
				}
				Netplay.Disconnect = true;
				Main.instance.Exit();
				return true;
			}

			playerLocations = new Dictionary<ISocket, int>();
			links = new Dictionary<int, SubserverLink>();

			return false;
		}

		private static void SubserverCallBack(object pipeObject)
		{
			NamedPipeServerStream pipe = (NamedPipeServerStream)pipeObject;
			try
			{
				while (pipe.IsConnected && !Netplay.Disconnect)
				{
					byte[] packetInfo = new byte[3];
					if (pipe.Read(packetInfo) < 3)
					{
						break;
					}

					MessageBuffer buffer = NetMessage.buffer[packetInfo[0]];
					int length = BitConverter.ToUInt16(packetInfo, 1);

					lock (buffer)
					{
						while (buffer.totalData + length > buffer.readBuffer.Length)
						{
							Monitor.Exit(buffer);
							Thread.Yield();
							Monitor.Enter(buffer);
						}

						buffer.readBuffer[buffer.totalData] = packetInfo[1];
						buffer.readBuffer[buffer.totalData + 1] = packetInfo[2];
						pipe.Read(buffer.readBuffer, buffer.totalData + 2, length - 2);

						if (buffer.readBuffer[buffer.totalData + 2] == 1)
						{
							Netplay.Clients[buffer.whoAmI].Socket = new SubserverSocket(buffer.whoAmI);
							Netplay.Clients[buffer.whoAmI].IsActive = true;
							Netplay.HasClients = true;
						}

						buffer.totalData += length;
						buffer.checkBytes = true;
					}
				}
			}
			finally
			{
				Netplay.Disconnect = true;

				pipe.Close();
				SubserverSocket.pipe.Close();
			}
		}

		internal static void ExitWorldCallBack(object index)
		{
			// presumably avoids a race condition?
			int netMode = Main.netMode;

			if (index != null)
			{
				if (netMode == 0)
				{
					WorldFile.CacheSaveTime();

					if (copiedData == null)
					{
						copiedData = new TagCompound();
					}
					if (cache != null)
					{
						cache.CopySubworldData();
						cache.OnExit();
					}
					if ((int)index >= 0)
					{
						CopyMainWorldData();
					}
				}
				else
				{
					Netplay.Connection.State = 1;
					cache?.OnExit();
				}

				current = (int)index < 0 ? null : subworlds[(int)index];
			}
			else
			{
				current = null;
			}

			Main.invasionProgress = -1;
			Main.invasionProgressDisplayLeft = 0;
			Main.invasionProgressAlpha = 0;
			Main.invasionProgressIcon = 0;

			noReturn = false;

			if (current != null)
			{
				hideUnderworld = true;
				current.OnEnter();
			}
			else
			{
				hideUnderworld = false;
			}

			Main.gameMenu = true;

			SoundEngine.StopTrackedSounds();
			CaptureInterface.ResetFocus();

			Main.ActivePlayerFileData.StopPlayTimer();
			Player.SavePlayer(Main.ActivePlayerFileData);
			Player.ClearPlayerTempInfo();

			Rain.ClearRain();

			if (netMode != 1)
			{
				WorldFile.SaveWorld();
			}
			else if (index == null)
			{
				Netplay.Disconnect = true;
				Main.netMode = 0;
			}
			SystemLoader.OnWorldUnload();

			Main.fastForwardTimeToDawn = false;
			Main.fastForwardTimeToDusk = false;
			Main.UpdateTimeRate();

			if (index == null)
			{
				Main.menuMode = 0;
				return;
			}

			WorldGen.noMapUpdate = true;
			if (cache != null && cache.NoPlayerSaving)
			{
				PlayerFileData playerData = Player.GetFileData(Main.ActivePlayerFileData.Path, Main.ActivePlayerFileData.IsCloudSave);
				if (playerData != null)
				{
					playerData.Player.whoAmI = Main.myPlayer;
					playerData.SetAsActive();
				}
			}

			if (netMode != 1)
			{
				LoadWorld();
			}
			else
			{
				NetMessage.SendData(1);
			}
		}

		private static void LoadWorld()
		{
			bool isSubworld = current != null;
			bool cloud = main.IsCloudSave;
			string path = isSubworld ? CurrentPath : main.Path;

			Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);

			cache?.OnUnload();

			Main.ToggleGameplayUpdates(false);

			WorldGen.gen = true;
			WorldGen.loadFailed = false;
			WorldGen.loadSuccess = false;

			if (!isSubworld || current.ShouldSave)
			{
				if (!isSubworld)
				{
					Main.ActiveWorldFileData = main;
				}

				TryLoadWorldFile(path, cloud, 0);
			}

			if (isSubworld)
			{
				if (WorldGen.loadFailed)
				{
					ModContent.GetInstance<StarsAbove>().Logger.Warn("Failed to load \"" + Main.worldName + (WorldGen.worldBackup ? "\" from file" : "\" from file, no backup"));
				}

				if (!WorldGen.loadSuccess)
				{
					LoadSubworld(path, cloud);
				}

				current.OnLoad();
			}
			else if (!WorldGen.loadSuccess)
			{
				ModContent.GetInstance<StarsAbove>().Logger.Error("Failed to load \"" + main.Name + (WorldGen.worldBackup ? "\" from file" : "\" from file, no backup"));
				Main.menuMode = 0;
				if (Main.netMode == 2)
				{
					Netplay.Disconnect = true;
				}
				return;
			}

			WorldGen.gen = false;

			if (Main.netMode != 2)
			{
				if (Main.mapEnabled)
				{
					Main.Map.Load();
				}
				Main.sectionManager.SetAllSectionsLoaded();
				while (Main.mapEnabled && Main.loadMapLock)
				{
					Main.statusText = Lang.gen[68].Value + " " + (int)((float)Main.loadMapLastX / Main.maxTilesX * 100 + 1) + "%";
					Thread.Sleep(0);
				}

				if (Main.anglerWhoFinishedToday.Contains(Main.LocalPlayer.name))
				{
					Main.anglerQuestFinished = true;
				}

				Main.QueueMainThreadAction(SpawnPlayer);
			}
		}

		private static void SpawnPlayer()
		{
			Main.LocalPlayer.Spawn(PlayerSpawnContext.SpawningIntoWorld);
			WorldFile.SetOngoingToTemps();
			Main.resetClouds = true;
			Main.gameMenu = false;
		}

		private static void OnEnterWorld(Player player)
		{
			if (Main.netMode == 1)
			{
				cache?.OnUnload();
				current?.OnLoad();
			}
			cache = current;
		}

		private static void OnDisconnect()
		{
			if (current != null || cache != null)
			{
				Main.menuMode = 14;
			}
			current = null;
			cache = null;
		}

		private static void LoadSubworld(string path, bool cloud)
		{
			Main.worldName = current.DisplayName.Value;
			if (Main.netMode == 2)
			{
				Console.Title = Main.worldName;
			}
			WorldFileData data = new WorldFileData(path, cloud)
			{
				Name = Main.worldName,
				GameMode = Main.GameMode,
				CreationTime = DateTime.Now,
				Metadata = FileMetadata.FromCurrentSettings(FileType.World),
				WorldGeneratorVersion = Main.WorldGeneratorVersion,
				UniqueId = Guid.NewGuid()
			};
			data.SetSeed(main.SeedText);
			Main.ActiveWorldFileData = data;

			Main.maxTilesX = current.Width;
			Main.maxTilesY = current.Height;
			Main.spawnTileX = Main.maxTilesX / 2;
			Main.spawnTileY = Main.maxTilesY / 2;
			WorldGen.setWorldSize();
			WorldGen.clearWorld();
			Main.worldSurface = Main.maxTilesY * 0.3;
			Main.rockLayer = Main.maxTilesY * 0.5;
			GenVars.waterLine = Main.maxTilesY;
			Main.weatherCounter = 18000;
			Cloud.resetClouds();

			ReadCopiedMainWorldData();

			double weight = 0;
			for (int i = 0; i < current.Tasks.Count; i++)
			{
				weight += current.Tasks[i].Weight;
			}
			WorldGenerator.CurrentGenerationProgress = new GenerationProgress();
			WorldGenerator.CurrentGenerationProgress.TotalWeight = weight;

			WorldGenConfiguration config = current.Config;

			for (int i = 0; i < current.Tasks.Count; i++)
			{
				WorldGen._genRand = new UnifiedRandom(data.Seed);
				Main.rand = new UnifiedRandom(data.Seed);

				GenPass task = current.Tasks[i];

				WorldGenerator.CurrentGenerationProgress.Start(task.Weight);
				task.Apply(WorldGenerator.CurrentGenerationProgress, config?.GetPassConfiguration(task.Name));
				WorldGenerator.CurrentGenerationProgress.End();
			}
			WorldGenerator.CurrentGenerationProgress = null;

			SystemLoader.OnWorldLoad();
		}

		private static void TryLoadWorldFile(string path, bool cloud, int tries)
		{
			LoadWorldFile(path, cloud);
			if (WorldGen.loadFailed)
			{
				if (tries == 1)
				{
					if (FileUtilities.Exists(path + ".bak", cloud))
					{
						WorldGen.worldBackup = true;

						FileUtilities.Copy(path, path + ".bad", cloud);
						FileUtilities.Copy(path + ".bak", path, cloud);
						FileUtilities.Delete(path + ".bak", cloud);

						string tMLPath = Path.ChangeExtension(path, ".twld");
						if (FileUtilities.Exists(tMLPath, cloud))
						{
							FileUtilities.Copy(tMLPath, tMLPath + ".bad", cloud);
						}
						if (FileUtilities.Exists(tMLPath + ".bak", cloud))
						{
							FileUtilities.Copy(tMLPath + ".bak", tMLPath, cloud);
							FileUtilities.Delete(tMLPath + ".bak", cloud);
						}
					}
					else
					{
						WorldGen.worldBackup = false;
						return;
					}
				}
				else if (tries == 3)
				{
					FileUtilities.Copy(path, path + ".bak", cloud);
					FileUtilities.Copy(path + ".bad", path, cloud);
					FileUtilities.Delete(path + ".bad", cloud);

					string tMLPath = Path.ChangeExtension(path, ".twld");
					if (FileUtilities.Exists(tMLPath, cloud))
					{
						FileUtilities.Copy(tMLPath, tMLPath + ".bak", cloud);
					}
					if (FileUtilities.Exists(tMLPath + ".bad", cloud))
					{
						FileUtilities.Copy(tMLPath + ".bad", tMLPath, cloud);
						FileUtilities.Delete(tMLPath + ".bad", cloud);
					}

					return;
				}
				TryLoadWorldFile(path, cloud, tries++);
			}
		}

		private static void LoadWorldFile(string path, bool cloud)
		{
			bool flag = cloud && SocialAPI.Cloud != null;
			if (!FileUtilities.Exists(path, flag))
			{
				return;
			}

			if (current != null)
			{
				Main.ActiveWorldFileData = new WorldFileData(path, cloud);
			}

			try
			{
				int status;
				using (BinaryReader reader = new BinaryReader(new MemoryStream(FileUtilities.ReadAllBytes(path, flag))))
				{
					status = current != null ? current.ReadFile(reader) : WorldFile.LoadWorld_Version2(reader);
				}
				if (Main.netMode == 2)
				{
					Console.Title = Main.worldName;
				}
				SystemLoader.OnWorldLoad();
				typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.IO.WorldIO").GetMethod("Load", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { path, flag });
				if (status != 0)
				{
					WorldGen.loadFailed = true;
					WorldGen.loadSuccess = false;
					return;
				}
				WorldGen.loadSuccess = true;
				WorldGen.loadFailed = false;

				if (current != null)
				{
					current.PostReadFile();
					cache?.ReadCopiedSubworldData();
					ReadCopiedMainWorldData();
				}
				else
				{
					PostLoadWorldFile();
					cache.ReadCopiedSubworldData();
					copiedData = null;
				}
			}
			catch
			{
				WorldGen.loadFailed = true;
				WorldGen.loadSuccess = false;
			}
		}

		internal static void PostLoadWorldFile()
		{
			GenVars.waterLine = Main.maxTilesY;
			Liquid.QuickWater(2);
			WorldGen.WaterCheck();
			Liquid.quickSettle = true;
			int updates = 0;
			int amount = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
			float num = 0;
			while (Liquid.numLiquid > 0 && updates < 100000)
			{
				updates++;
				float progress = (amount - Liquid.numLiquid + LiquidBuffer.numLiquidBuffer) / (float)amount;
				if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > amount)
				{
					amount = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
				}
				if (progress > num)
				{
					num = progress;
				}
				else
				{
					progress = num;
				}
				Main.statusText = Lang.gen[27].Value + " " + (int)(progress * 100 / 2 + 50) + "%";
				Liquid.UpdateLiquid();
			}
			Liquid.quickSettle = false;
			Main.weatherCounter = WorldGen.genRand.Next(3600, 18000);
			Cloud.resetClouds();
			WorldGen.WaterCheck();
			NPC.setFireFlyChance();
			if (Main.slimeRainTime > 0)
			{
				Main.StartSlimeRain(false);
			}
			NPC.SetWorldSpecificMonstersByWorldID();
		}
	}
	#endregion
}