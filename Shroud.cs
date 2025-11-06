
using Il2Cpp;
using Il2CppFIMSpace.Basics;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using UnityEngine;
using RevealDilemmaMod;
using System.ComponentModel.Design;
using static Il2CppSystem.Globalization.CultureInfo;
using static MelonLoader.MelonLaunchOptions;

namespace RevealDilemmaMod;

[RegisterTypeInIl2Cpp]
public class Shroud : Demon
{

    public Shroud() : base(ClassInjector.DerivedConstructorPointer<Shroud>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);

    }

    public Shroud(System.IntPtr ptr) : base(ptr)
    {

    }

    public override string Description
    {
        get => "[After you reveal a character, I deal 1 damage to you. \nI Lie and Disguise.]";
    }

    // public override Il2CppSystem.Collections.Generic.List<SpecialRule> GetRules()
    // {

    //     // var rules = new Il2CppSystem.Collections.Generic.List<SpecialRule>();

    //     // rules.Add(new NightModeRule(2));

    //     // return rules;
    //     return null;
    // }

    public override void Act(ETriggerPhase trigger, Character charRef)
    {

        if (trigger == ETriggerPhase.Start)
            charRef.statuses.AddStatus(ECharacterStatus.UnkillableByDemon, charRef);

        if (charRef.state == ECharacterState.Dead) return;
    }

    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }

}