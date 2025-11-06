
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
public class Saboteur : Role
{

    public Saboteur() : base(ClassInjector.DerivedConstructorPointer<Saboteur>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);

    }

    public Saboteur(System.IntPtr ptr) : base(ptr)
    {

    }

    public override string Description
    {
        get => "[After you reveal me, you take 4 damage. I disguise as a random Villager.]";
    }

    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        MelonLogger.Msg($"Act called with trigger: {trigger}");
        if (trigger == ETriggerPhase.Day)
        {
            if (charRef.statuses.Contains(ECharacterStatus.BrokenAbility)) return;
            PlayerController.PlayerInfo.health.Damage(4);
        }
    }


    public override CharacterData GetBluffIfAble(Character charRef)
    {
        if (!charRef.statuses.Contains(ECharacterStatus.Corrupted))
        {
            charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
            
            Il2Cpp.GameData gameData = ProjectContext.Instance.gameData;
            Il2CppSystem.Collections.Generic.List<CharacterData> characters = gameData.allCharacterData;
            characters = Characters.Instance.FilterBluffableCharacters(characters);
            characters = Characters.Instance.FilterCharacterType(characters, ECharacterType.Villager);
            characters = Characters.Instance.FilterAlignmentCharacters(characters, EAlignment.Good);
            CharacterData character = characters[UnityEngine.Random.Range(0, characters.Count)];
            return character;
        }
        else
        {
            Il2CppSystem.Collections.Generic.List<Character> characters = new Il2CppSystem.Collections.Generic.List<Character>(Gameplay.CurrentCharacters.Pointer);

            characters = Characters.Instance.FilterBluffableCharacters(characters);
            characters = Characters.Instance.FilterAlignmentCharacters(characters, EAlignment.Evil);

            return characters[UnityEngine.Random.Range(0, characters.Count)].GetCharacterBluffIfAble();
        }
    }

}