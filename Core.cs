using Il2Cpp;
using Il2CppInterop.Runtime.Injection;

using Il2CppDissolveExample;
using Il2CppInterop.Runtime;

using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem.IO;
using MelonLoader;
using System;
using UnityEngine;

using RevealDilemmaMod;

using static Il2Cpp.GameplayEvents;

[assembly: MelonInfo(typeof(Core), "RevealDilemmaMod", "0.1.0", "tulxoro", null)]
[assembly: MelonGame("UmiArt", "Demon Bluff")]

namespace RevealDilemmaMod;

    public class Core : MelonMod
{
    public override void OnInitializeMelon()
    {
        ClassInjector.RegisterTypeInIl2Cpp<Saboteur>();
        ClassInjector.RegisterTypeInIl2Cpp<Shroud>();

    }

    public override void OnLateInitializeMelon()
    {

        Il2Cpp.GameData gameData = ProjectContext.Instance.gameData;
        Il2CppSystem.Collections.Generic.List<CharacterData> allCharacters = gameData.allCharacterData;

        CharacterData saboteur = new CharacterData();
        saboteur.role = new Saboteur();
        saboteur.name = "Saboteur";
        saboteur.description = "After you reveal me, you take 3 damage. I disguise as a random Villager.";
        saboteur.flavorText = "\"It was an accident! I swear!\"";
        saboteur.hints = "I may disguise as a Villager not in the deck. Because I am an outcast, I usually don't lie.";
        saboteur.ifLies = "I always disguise as a character in play.";
        saboteur.picking = false;
        saboteur.startingAlignment = EAlignment.Good;
        saboteur.type = ECharacterType.Outcast;
        saboteur.abilityUsage = EAbilityUsage.Once;
        saboteur.bluffable = false;
        saboteur.characterId = "sabo_rdm";
        saboteur.artBgColor = new Color(0.3679f, 0.2014f, 0.1541f);
        saboteur.cardBgColor = new Color(0.102f, 0.0667f, 0.0392f);
        saboteur.cardBorderColor = new Color(0.7843f, 0.6471f, 0f);
        saboteur.color = new Color(0.9659f, 1f, 0.4472f);


        CharacterData shroud = new CharacterData();
        shroud.role = new Shroud();
        shroud.name = "Shroud";
        shroud.description = "After you reveal a character, I deal 1 damage to you. \nI Lie and Disguise.";
        shroud.flavorText = "\"Awaiting in the darkness, the shroud lurks.\"";
        shroud.hints = "";
        shroud.ifLies = "";
        shroud.picking = false;
        shroud.startingAlignment = EAlignment.Evil;
        shroud.type = ECharacterType.Demon;
        shroud.abilityUsage = EAbilityUsage.Once;
        shroud.bluffable = false;
        shroud.characterId = "shroud_rdm";
        shroud.artBgColor = new Color(1f, 0f, 0f);
        shroud.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        shroud.cardBorderColor = new Color(0.8208f, 0f, 0.0241f);
        shroud.color = new Color(1f, 0.3811f, 0.3811f);


        allCharacters.Add(shroud);
        allCharacters.Add(saboteur);


        AscensionsData advancedAscension = gameData.advancedAscension;

        foreach (CustomScriptData scriptData in advancedAscension.possibleScriptsData)
        {
            ScriptInfo script = scriptData.scriptInfo;


            addRole(script.startingDemons, shroud);
            addRole(script.startingOutsiders, saboteur);
        }

        GameplayEvents.OnCharacterRevealed += new Action<Character>(OnCharacterRevealed);
    }

    public void addRole(Il2CppSystem.Collections.Generic.List<CharacterData> list, CharacterData data)
    {
        if (list.Contains(data))
        {
            return;
        }
        list.Add(data);
    }

    private void OnCharacterRevealed(Character revealed)
    {

        CharacterData charData = revealed.dataRef;

        Il2CppSystem.Collections.Generic.List<Character> allChars = new Il2CppSystem.Collections.Generic.List<Character>(Gameplay.CurrentCharacters.Pointer);
        
        bool shroudIsAlive = false;
        for (int i = 0; i < allChars.Count; i++)
        {
            if (allChars[i].dataRef.characterId == "shroud_rdm" && allChars[i].state != ECharacterState.Dead)
            {
                shroudIsAlive = true;
                break;
            }
        }
        
        if (shroudIsAlive)
        {
            PlayerController.PlayerInfo.health.Damage(1);
        }

        if (charData.characterId == "sabo_rdm") {
            PlayerController.PlayerInfo.health.Damage(4);
        }
    }


}
