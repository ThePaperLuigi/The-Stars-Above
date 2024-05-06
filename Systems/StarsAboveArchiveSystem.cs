namespace StarsAbove.Systems
{
    //Basic outline of visual novel code: (flowchart)
    /*
	 *
	 * 
	 * */


    public class ArchiveListing
    {
        private string name;
        private string listInfo;
        private bool viewable;
        private int dialogueID;
        private string unlockConditions;

        public ArchiveListing(string name, string listInfo, bool viewable, int dialogueID, string unlockConditions)
        {
            this.name = name;
            this.listInfo = listInfo;
            this.viewable = viewable;
            this.dialogueID = dialogueID;
            this.unlockConditions = unlockConditions;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string ListInformation
        {
            get { return listInfo; }
            set { listInfo = value; }
        }
        public bool IsViewable
        {
            get { return viewable; }
            set { viewable = value; }
        }
        public int DialogueID
        {
            get { return dialogueID; }
            set { dialogueID = value; }
        }


        public string UnlockConditions
        {
            get { return unlockConditions; }
            set { unlockConditions = value; }
        }




    }


    
}

/*
 * if (archiveChosenList == 0)
                {
                    if (archiveListNumber == 1)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Default Idle Dialogue" +
                            "\n" +
                            "\nThis dialogue appears" +
                            "\nwhen you've already seen" +
                            "\nthe normal idle dialogue " +
                            "\nrecently.";
                    }//Idle dialogue.
                    if (archiveListNumber == 2)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 1" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 3)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 2" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 4)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 3" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 5)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 4" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 6)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 5" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 7)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 6" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 8)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 7" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 9)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 8" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 10)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 9" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 11)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 10" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 12)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 11" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 13)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 12" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 14)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 13" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 15)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 14" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 16)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 15" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 17)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 16" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 18)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 17" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 19)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 18" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 20)//      |
                    {
                        if (WarriorOfLightDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Idle Conversation EX" +
                            "\n" +
                            "\nA world shrouded in" +
                            "\nLight." +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\n???" +
                            "\n" +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 21)//      |
                    {
                        if (nalhaunBossItemDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Nalhaun Item" +
                            "\n" +
                            "\nGrants the item" +
                            "\nneeded to summon" +
                            "\nNalhaun." +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\n???" +
                            "\n" +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 22)//      |
                    {
                        if (penthBossItemDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Penthesilea Item" +
                            "\n" +
                            "\nGrants the item" +
                            "\nneeded to summon" +
                            "\nPenthesilea." +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\n???" +
                            "\n" +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 23)//      |
                    {
                        if (arbiterBossItemDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Arbitration Item" +
                            "\n" +
                            "\nGrants the item" +
                            "\nneeded to summon" +
                            "\nArbitration." +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\n???" +
                            "\n" +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 24)//      |
                    {
                        if (warriorBossItemDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Warrior of Light Item" +
                            "\n" +
                            "\nGrants the item" +
                            "\nneeded to summon" +
                            "\nThe Warrior of Light." +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\n???" +
                            "\n" +
                            "\n" +
                            "\n";
                        }

                    }
                }//All idle dialogues.
                if (archiveChosenList == 1)
                {
                    if (archiveListNumber == 1)//      |
                    {
                        if (slimeDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Slime King" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nSlime King" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 2)//      |
                    {

                        if (eyeDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Eye of Cthulhu" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nEye of Cthulhu" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 3)//      |
                    {
                        if (corruptBossDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Corrupted Boss" +
                            "\n" +
                            "\nChanges depending on" +
                            "\nthe world's evil." +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nCorruption/Crimson" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 4)//      |
                    {
                        if (BeeBossDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Queen Bee" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nQueen Bee" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 5)//      |
                    {
                        if (SkeletonDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Skeletron" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nSkeletron" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 6)//      |
                    {
                        if (WallOfFleshDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Wall of Flesh" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nWall of Flesh" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 7)//      |
                    {
                        if (TwinsDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "The Twins" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nThe Twins" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 8)//      |
                    {
                        if (DestroyerDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "The Destroyer" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nThe Destroyer" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 9)//      |
                    {
                        if (SkeletronPrimeDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Skeletron Prime" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nSkeletron Prime" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 10)//      |
                    {
                        if (AllMechsDefeatedDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "All Mechs Defeated" +
                            "\n" +
                            "\nPlays when all mech" +
                            "\nbosses have been" +
                            "\ndefeated." +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nAll Mech Bosses" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 11)//      |
                    {
                        if (PlanteraDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Plantera" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nPlantera" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 12)//      |
                    {
                        if (GolemDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Golem" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nGolem" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 13)//      |
                    {
                        if (DukeFishronDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Duke Fishron" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDuke Fishron" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 14)//      |
                    {
                        if (CultistDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Lunatic Cultist" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nLunatic Cultist" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 15)//      |
                    {
                        if (MoonLordDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Moon Lord" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nMoon Lord" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 16)//      |
                    {
                        if (WarriorOfLightDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Warrior of Light" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nWarrior of Light" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 17)//      |
                    {
                        if (AllVanillaBossesDefeatedDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "All Vanilla Bosses" +
                            "\n" +
                            "\nAll vanilla bosses" +
                            "\ndefeated." +
                            "\nGives an Essence." +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nAll Vanilla Bosses" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 18)//      |
                    {
                        if (EverythingDefeatedDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Bosses + Warrior" +
                            "\n" +
                            "\nAll vanilla bosses" +
                            "\nand the Warrior of" +
                            "\nLight defeated." +
                            "\nGives an Essence.";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nVanilla Bosses +" +
                            "\nWarrior of Light" +
                            "\nboss dialogue." +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 19)//      |
                    {
                        if (vagrantDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Vagrant" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nVagrant" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 20)//      |
                    {
                        if (nalhaunDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Nalhaun" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nNalhaun" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 21)//      |
                    {
                        if (penthDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Penthesilea" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nPenthesilea" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 22)//      |
                    {
                        if (arbiterDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Arbitration" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nArbitration" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 23)//      |
                    {
                        if (tsukiyomiDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Tsukiyomi" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nTsukiyomi" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 24)//      |
                    {
                        if (desertscourgeDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Desert Scourge" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDesert Scourge" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 25)//      |
                    {
                        if (crabulonDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Crabulon" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nCrabulon" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 26)//      |
                    {
                        if (hivemindDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Hive Mind" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nHive Mind" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 27)//      |
                    {
                        if (perforatorDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Perforators" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nPerforators" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 28)//      |
                    {
                        if (slimegodDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Slime God" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nSlime God" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 29)//      |
                    {
                        if (cryogenDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Cryogen" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nCryogen" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 30)//      |
                    {
                        if (aquaticscourgeDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Aquatic Scourge" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nAquatic Scourge" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 31)//      |
                    {

                        if (brimstoneelementalDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Brimstone Elemental" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nBrimstone Elemental" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 32)//      |
                    {

                        if (calamitasDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Calamitas" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nCalamitas" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 33)//      |
                    {

                        if (leviathanDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Leviathan" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nLeviathan" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 34)//      |
                    {

                        if (astrumaureusDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Astrum Aureus" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nAstrum Aureus" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 35)//      |
                    {

                        if (plaguebringerDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Plaguebringer Goliath" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nPlaguebringer Goliath" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 36)//      |
                    {

                        if (ravagerDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Ravager" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nRavager" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 37)//      |
                    {

                        if (astrumdeusDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Astrum Deus" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nAstrum Deus" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                }//Boss Dialogues
                if (archiveChosenList == 2)//Weapon Dialogues
                {
                    //Eye of Cthulhu
                    if (archiveListNumber == 1)//      |
                    {
                        if (EyeBossWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Carian Dark Moon" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom Eye of Cthulhu." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Konpaku Katana" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Eye of Cthulhu." +
                              "\n" +
                               "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Eye of Cthulhu." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //King Slime
                    if (archiveListNumber == 2)//      |
                    {
                        if (KingSlimeWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Aegis Driver" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom King Slime," +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Rad Gun" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom King Slime." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat King Slime." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Corruption Boss
                    if (archiveListNumber == 3)//      |
                    {
                        if (CorruptBossWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Neo Dealmaker" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Corruption boss." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Ashen Ambition" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Corruption boss." +
                              "\n" +
                               "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Corruption/Crimson's" +
                            "\nboss." +
                            "\n" +
                            "\n";
                        }
                    }
                    //Corruption Boss (EoW / Brain)
                    if (archiveListNumber == 4)//      |
                    {
                        if (TakodachiWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Takonomicon" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Skeletron," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Skeletron," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Queen Bee
                    if (archiveListNumber == 5)//      |
                    {
                        if (QueenBeeWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Skofnung" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Queen Bee." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Inugami Ripsaw" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Queen Bee." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Queen Bee." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Skeletron
                    if (archiveListNumber == 6)//      |
                    {
                        if (SkeletonWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Der Freischutz" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Skeletron." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Death In Four Acts" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Skeletron." +
                              "\n" +
                               "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Skeletron." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Post Skeletron
                    if (archiveListNumber == 7)//      |
                    {
                        if (MiseryWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Misery's Company" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Skeletron," +
                              "\nthen waiting." +
                              "\n";


                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Skeletron," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Beach After Skeletron
                    if (archiveListNumber == 8)//      |
                    {
                        if (OceanWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Apalistik" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Skeletron," +
                              "\nthen visiting the" +
                              "\nBeach.";


                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Skeletron," +
                            "\nthen visit the" +
                            "\nBeach." +
                            "\n";
                        }

                    }
                    //Hell After Skeletron
                    if (archiveListNumber == 9)//      |
                    {
                        if (HellWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Persephone" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Skeletron," +
                              "\nthen visiting the" +
                              "\nUnderworld.";
                            }
                            else
                            {
                                archiveListInfo = "Kazimierz Seraphim" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Skeletron," +
                              "\nthen visiting the" +
                              "\nUnderworld.";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Skeletron," +
                            "\nthen visit the" +
                            "\nUnderworld." +
                            "\n";
                        }
                    }
                    //Wall of Flesh
                    if (archiveListNumber == 10)//      |
                    {
                        if (WallOfFleshWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Karlan Truesilver" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Wall of Flesh." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Every Moment Matters" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Wall of Flesh." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Wall of Flesh." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Post Wall of Flesh
                    if (archiveListNumber == 11)//      |
                    {
                        if (LumaWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Luminary Wand" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom the Wall of Flesh" +
                              "\nthen waiting." +
                              "\n";


                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat the Wall of Flesh," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Wall of Flesh
                    if (archiveListNumber == 12)//      |
                    {
                        if (ForceWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Force-of-Nature" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Wall of Flesh," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Wall of Flesh," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Vagrant of Space and Time
                    if (archiveListNumber == 13)//      |
                    {
                        if (VagrantWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Izanagi's Edge" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Vagrant." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Hawkmoon" +
                              "\n" +
                              "\nMagic/Ranged weapon." +
                              "\nFrom Vagrant." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat the Vagrant" +
                            "\nof Space and Time." +
                            "\n" +
                            "\n";
                        }
                    }
                    //Any Mechanical Boss
                    if (archiveListNumber == 14)//      |
                    {
                        if (MechBossWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Veneration of Butterflies" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom any Mech boss." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Memento Muse" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom any Mech boss." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat any mechanical" +
                            "\nboss." +
                            "\n" +
                            "\n";
                        }
                    }
                    //Any Mechanical Boss
                    if (archiveListNumber == 15)//      |
                    {
                        if (MonadoWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Xenoblade" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom any mechanical" +
                              "\nboss, then waiting." +
                              "\n";


                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat any mechanical boss," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Nalhaun, the Burnished King
                    if (archiveListNumber == 16)//      |
                    {
                        if (NalhaunWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Phantom In The Mirror" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Nalhaun." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Hollowheart Albion" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Nalhaun." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Nalhaun, the" +
                            "\nBurnished King." +
                            "\n" +
                            "\n";
                        }
                    }
                    //Skeletron Prime
                    if (archiveListNumber == 17)//      |
                    {
                        if (SkyStrikerWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Sky Striker Armaments" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom " +
                              "\nSkeletron Prime," +
                              "\nthen waiting.";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat" +
                            "\nSkeletron Prime," +
                            "\nthen wait." +
                            "\n";
                        }

                    }
                    //All Mechanical Bosses
                    if (archiveListNumber == 18)//      |
                    {
                        if (AllMechBossWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Ride The Bull" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom any Mech boss." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Drachenlance" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom any Mech boss." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat all mechanical" +
                            "\nbosses." +
                            "\n" +
                            "\n";
                        }
                    }
                    //All Mechanical Bosses
                    if (archiveListNumber == 19)//      |
                    {
                        if (HullwroughtWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Hullwrought" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom all Mech" +
                              "\nBosses." +
                              "\n";


                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat all" +
                            "\nMechanical Bosses." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Plantera
                    if (archiveListNumber == 20)//      |
                    {
                        if (PlanteraWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Crimson Outbreak" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Plantera." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Voice of the Fallen" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom Plantera." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Plantera." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Plantera
                    if (archiveListNumber == 21)//      |
                    {
                        if (KifrosseWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Kifrosse" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Plantera," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Plantera," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Frost Moon (Post Plantera)
                    if (archiveListNumber == 22)//      |
                    {
                        if (FrostMoonWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Stygian Nymph" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom the Ice Queen." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Caesura of Despair" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom the Ice Queen." +
                              "\n" +
                              "\n";
                            }
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat the Ice Queen." +
                            "\n" +
                            "\n" +
                            "\n";
                        }

                    }
                    //Penthesila, the Witch of Ink
                    if (archiveListNumber == 23)//      |
                    {
                        if (PenthesileaWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Vision of Euthymia" +
                              "\n" +
                              "\nSpecial weapon." +
                              "\nFrom Penthesilea." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Kroniic Principality" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Penthesilea." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Penthesilea," +
                            "\nthe Witch of Ink." +
                            "\n" +
                            "\n";
                        }
                    }
                    //Penthesila, the Witch of Ink
                    if (archiveListNumber == 24)//      |
                    {
                        if (MuseWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Penthesilea's Muse" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Penthesilea," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Penthesilea," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Golem
                    if (archiveListNumber == 25)//      |
                    {
                        if (GolemWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Plenilune Gaze" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Golem." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Tartaglia" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Golem." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Golem." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Golem
                    if (archiveListNumber == 26)//      |
                    {
                        if (GenocideWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Genocide" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Golem," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Golem," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Arbitration
                    if (archiveListNumber == 27)//      |
                    {
                        if (ArbitrationWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Liberation Blazing" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Arbitration." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Unforgotten" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Penthesilea." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Arbitration." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Arbitration
                    if (archiveListNumber == 28)//      |
                    {
                        if (ClaimhWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Claimh Solais" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Arbitration," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Arbitration," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Duke Fishron
                    if (archiveListNumber == 29)//      |
                    {
                        if (DukeFishronWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Key of the Sinner" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Duke Fishron." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Crimson Sakura Alpha" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Duke Fishron." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Duke Fishron." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Lunatic Cultist
                    if (archiveListNumber == 30)//      |
                    {
                        if (LunaticCultistWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Rex Lapis" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Lunatic Cultist." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Yunlai Stiletto" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Lunatic Cultist." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Lunatic" +
                            "\nCultist." +
                            "\n" +
                            "\n";
                        }
                    }
                    //Lunatic Cultist
                    if (archiveListNumber == 31)//      |
                    {
                        if (TwinStarsWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Twin Stars" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom " +
                              "\nLunatic Cultist," +
                              "\nthen waiting.";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat" +
                            "\nLunatic Cultist," +
                            "\nthen wait." +
                            "\n";
                        }

                    }
                    //Moon Lord
                    if (archiveListNumber == 32)//      |
                    {
                        if (MoonLordWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Suistrume" +
                              "\n" +
                              "\nSpecial weapon." +
                              "\nFrom Moon Lord." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Naganadel" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Moon Lord." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Moon Lord." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Moon Lord
                    if (archiveListNumber == 33)//      |
                    {
                        if (ShadowlessWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Shadowless Cerulean" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom the Moon Lord." +
                              "\n" +
                              "\n";


                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat the Moon" +
                            "\nLord." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Warrior of Light
                    if (archiveListNumber == 34)//      |
                    {
                        if (WarriorWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Key of the King's Law" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Warrior of Light." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Light Unrelenting" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom Warrior of Light." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Warrior of Light." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Tsukiyomi, the First Starfarer
                    if (archiveListNumber == 35)//      |
                    {
                        if (ArchitectWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Architect's Luminance" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Tsukiyomi," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Tsukiyomi," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Tsukiyomi, the First Starfarer
                    if (archiveListNumber == 36)//      |
                    {
                        if (ArchitectWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Cosmic Destroyer" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Tsukiyomi," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Tsukiyomi," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Empress of Light
                    if (archiveListNumber == 37)//      |
                    {
                        if (NeedlepointWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Arachnid Needlepoint" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Empress of Light," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Empress of Light," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Empress of Light (Master Mode
                    if (archiveListNumber == 38)//      |
                    {
                        if (MurasamaWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "The Only Thing I Know-" +
                              "\n" +
                              "\nSpatial weapon." +
                              "\nFrom Empress of Light," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Empress of Light," +
                            "\nthen wait." +
                            "\nMaster mode exclusive." +
                            "\n";
                        }

                    }
                    //Golem
                    if (archiveListNumber == 39)//      |
                    {
                        if (MercyWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Mercy" +
                              "\n" +
                              "\nSpatial weapon." +
                              "\nFrom Golem," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Golem" +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Empress of Light 
                    if (archiveListNumber == 40)//      |
                    {
                        if (SakuraWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Sakura's Vengeance" +
                              "\n" +
                              "\nSpatial weapon." +
                              "\nFrom Empress of Light," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Empress of Light," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Moon Lord
                    if (archiveListNumber == 41)//      |
                    {
                        if (EternalWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Eternal Star" +
                              "\n" +
                              "\nSpatial weapon." +
                              "\nFrom Moon Lord," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat the Moon Lord," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Moon Lord
                    if (archiveListNumber == 42)//      |
                    {
                        if (DaemonWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Vermilion Daemon" +
                              "\n" +
                              "\nSpatial weapon." +
                              "\nFrom Moon Lord," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat the Moon Lord," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 43)//      |
                    {
                        if (OzmaWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Ozma Ascendant" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom Cultist," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Lunatic Cultist," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 44)//      |
                    {
                        if (UrgotWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Dreadnought Chemtank" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Queen Slime," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Queen Slime," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 45)//      |
                    {
                        if (BloodWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "The Blood Blade" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Pumpking," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Pumpking," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 46)//      |
                    {
                        if (MorningStarWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "The Morning Star" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Deerclops," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Deerclops," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                }//Weapon Dialogues
if (archiveChosenList == 0)
                {
                    if (archiveListNumber == 1)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Default Idle Dialogue" +
                            "\n" +
                            "\nThis dialogue appears" +
                            "\nwhen you've already seen" +
                            "\nthe normal idle dialogue " +
                            "\nrecently.";
                    }//Idle dialogue.
                    if (archiveListNumber == 2)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 1" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 3)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 2" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 4)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 3" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 5)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 4" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 6)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 5" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 7)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 6" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 8)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 7" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 9)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 8" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 10)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 9" +
                            "\n" +
                            "\nPre-hardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 11)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 10" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 12)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 11" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 13)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 12" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 14)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 13" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 15)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 14" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 16)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 15" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 17)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 16" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 18)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 17" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 19)//      |
                    {
                        canViewArchive = true;
                        archiveListInfo = "Idle Conversation 18" +
                            "\n" +
                            "\nHardmode." +
                            "\n" +
                            "\n" +
                            "\n";
                    }
                    if (archiveListNumber == 20)//      |
                    {
                        if (WarriorOfLightDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Idle Conversation EX" +
                            "\n" +
                            "\nA world shrouded in" +
                            "\nLight." +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\n???" +
                            "\n" +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 21)//      |
                    {
                        if (nalhaunBossItemDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Nalhaun Item" +
                            "\n" +
                            "\nGrants the item" +
                            "\nneeded to summon" +
                            "\nNalhaun." +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\n???" +
                            "\n" +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 22)//      |
                    {
                        if (penthBossItemDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Penthesilea Item" +
                            "\n" +
                            "\nGrants the item" +
                            "\nneeded to summon" +
                            "\nPenthesilea." +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\n???" +
                            "\n" +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 23)//      |
                    {
                        if (arbiterBossItemDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Arbitration Item" +
                            "\n" +
                            "\nGrants the item" +
                            "\nneeded to summon" +
                            "\nArbitration." +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\n???" +
                            "\n" +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 24)//      |
                    {
                        if (warriorBossItemDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Warrior of Light Item" +
                            "\n" +
                            "\nGrants the item" +
                            "\nneeded to summon" +
                            "\nThe Warrior of Light." +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\n???" +
                            "\n" +
                            "\n" +
                            "\n";
                        }

                    }
                }//All idle dialogues.
                if (archiveChosenList == 1)
                {
                    if (archiveListNumber == 1)//      |
                    {
                        if (slimeDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Slime King" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nSlime King" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 2)//      |
                    {

                        if (eyeDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Eye of Cthulhu" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nEye of Cthulhu" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 3)//      |
                    {
                        if (corruptBossDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Corrupted Boss" +
                            "\n" +
                            "\nChanges depending on" +
                            "\nthe world's evil." +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nCorruption/Crimson" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 4)//      |
                    {
                        if (BeeBossDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Queen Bee" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nQueen Bee" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 5)//      |
                    {
                        if (SkeletonDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Skeletron" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nSkeletron" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 6)//      |
                    {
                        if (WallOfFleshDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Wall of Flesh" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nWall of Flesh" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 7)//      |
                    {
                        if (TwinsDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "The Twins" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nThe Twins" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 8)//      |
                    {
                        if (DestroyerDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "The Destroyer" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nThe Destroyer" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 9)//      |
                    {
                        if (SkeletronPrimeDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Skeletron Prime" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nSkeletron Prime" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 10)//      |
                    {
                        if (AllMechsDefeatedDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "All Mechs Defeated" +
                            "\n" +
                            "\nPlays when all mech" +
                            "\nbosses have been" +
                            "\ndefeated." +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nAll Mech Bosses" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 11)//      |
                    {
                        if (PlanteraDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Plantera" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nPlantera" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 12)//      |
                    {
                        if (GolemDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Golem" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nGolem" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 13)//      |
                    {
                        if (DukeFishronDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Duke Fishron" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDuke Fishron" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 14)//      |
                    {
                        if (CultistDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Lunatic Cultist" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nLunatic Cultist" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 15)//      |
                    {
                        if (MoonLordDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Moon Lord" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nMoon Lord" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 16)//      |
                    {
                        if (WarriorOfLightDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Warrior of Light" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nWarrior of Light" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 17)//      |
                    {
                        if (AllVanillaBossesDefeatedDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "All Vanilla Bosses" +
                            "\n" +
                            "\nAll vanilla bosses" +
                            "\ndefeated." +
                            "\nGives an Essence." +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nAll Vanilla Bosses" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 18)//      |
                    {
                        if (EverythingDefeatedDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Bosses + Warrior" +
                            "\n" +
                            "\nAll vanilla bosses" +
                            "\nand the Warrior of" +
                            "\nLight defeated." +
                            "\nGives an Essence.";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nVanilla Bosses +" +
                            "\nWarrior of Light" +
                            "\nboss dialogue." +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 19)//      |
                    {
                        if (vagrantDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Vagrant" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nVagrant" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 20)//      |
                    {
                        if (nalhaunDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Nalhaun" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nNalhaun" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 21)//      |
                    {
                        if (penthDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Penthesilea" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nPenthesilea" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 22)//      |
                    {
                        if (arbiterDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Arbitration" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nArbitration" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 23)//      |
                    {
                        if (tsukiyomiDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Tsukiyomi" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nTsukiyomi" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 24)//      |
                    {
                        if (desertscourgeDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Desert Scourge" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDesert Scourge" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 25)//      |
                    {
                        if (crabulonDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Crabulon" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nCrabulon" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 26)//      |
                    {
                        if (hivemindDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Hive Mind" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nHive Mind" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 27)//      |
                    {
                        if (perforatorDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Perforators" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nPerforators" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 28)//      |
                    {
                        if (slimegodDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Slime God" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nSlime God" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 29)//      |
                    {
                        if (cryogenDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Cryogen" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nCryogen" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 30)//      |
                    {
                        if (aquaticscourgeDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Aquatic Scourge" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nAquatic Scourge" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 31)//      |
                    {

                        if (brimstoneelementalDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Brimstone Elemental" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nBrimstone Elemental" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 32)//      |
                    {

                        if (calamitasDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Calamitas" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nCalamitas" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 33)//      |
                    {

                        if (leviathanDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Leviathan" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nLeviathan" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 34)//      |
                    {

                        if (astrumaureusDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Astrum Aureus" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nAstrum Aureus" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 35)//      |
                    {

                        if (plaguebringerDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Plaguebringer Goliath" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nPlaguebringer Goliath" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 36)//      |
                    {

                        if (ravagerDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Ravager" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nRavager" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                    if (archiveListNumber == 37)//      |
                    {

                        if (astrumdeusDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Astrum Deus" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nAstrum Deus" +
                            "\nboss dialogue." +
                            "\n" +
                            "\n";
                        }
                    }
                }//Boss Dialogues
                if (archiveChosenList == 2)//Weapon Dialogues
                {
                    //Eye of Cthulhu
                    if (archiveListNumber == 1)//      |
                    {
                        if (EyeBossWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Carian Dark Moon" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom Eye of Cthulhu." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Konpaku Katana" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Eye of Cthulhu." +
                              "\n" +
                               "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Eye of Cthulhu." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //King Slime
                    if (archiveListNumber == 2)//      |
                    {
                        if (KingSlimeWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Aegis Driver" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom King Slime," +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Rad Gun" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom King Slime." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat King Slime." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Corruption Boss
                    if (archiveListNumber == 3)//      |
                    {
                        if (CorruptBossWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Neo Dealmaker" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Corruption boss." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Ashen Ambition" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Corruption boss." +
                              "\n" +
                               "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Corruption/Crimson's" +
                            "\nboss." +
                            "\n" +
                            "\n";
                        }
                    }
                    //Corruption Boss (EoW / Brain)
                    if (archiveListNumber == 4)//      |
                    {
                        if (TakodachiWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Takonomicon" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Skeletron," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Skeletron," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Queen Bee
                    if (archiveListNumber == 5)//      |
                    {
                        if (QueenBeeWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Skofnung" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Queen Bee." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Inugami Ripsaw" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Queen Bee." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Queen Bee." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Skeletron
                    if (archiveListNumber == 6)//      |
                    {
                        if (SkeletonWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Der Freischutz" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Skeletron." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Death In Four Acts" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Skeletron." +
                              "\n" +
                               "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Skeletron." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Post Skeletron
                    if (archiveListNumber == 7)//      |
                    {
                        if (MiseryWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Misery's Company" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Skeletron," +
                              "\nthen waiting." +
                              "\n";


                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Skeletron," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Beach After Skeletron
                    if (archiveListNumber == 8)//      |
                    {
                        if (OceanWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Apalistik" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Skeletron," +
                              "\nthen visiting the" +
                              "\nBeach.";


                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Skeletron," +
                            "\nthen visit the" +
                            "\nBeach." +
                            "\n";
                        }

                    }
                    //Hell After Skeletron
                    if (archiveListNumber == 9)//      |
                    {
                        if (HellWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Persephone" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Skeletron," +
                              "\nthen visiting the" +
                              "\nUnderworld.";
                            }
                            else
                            {
                                archiveListInfo = "Kazimierz Seraphim" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Skeletron," +
                              "\nthen visiting the" +
                              "\nUnderworld.";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Skeletron," +
                            "\nthen visit the" +
                            "\nUnderworld." +
                            "\n";
                        }
                    }
                    //Wall of Flesh
                    if (archiveListNumber == 10)//      |
                    {
                        if (WallOfFleshWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Karlan Truesilver" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Wall of Flesh." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Every Moment Matters" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Wall of Flesh." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Wall of Flesh." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Post Wall of Flesh
                    if (archiveListNumber == 11)//      |
                    {
                        if (LumaWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Luminary Wand" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom the Wall of Flesh" +
                              "\nthen waiting." +
                              "\n";


                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat the Wall of Flesh," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Wall of Flesh
                    if (archiveListNumber == 12)//      |
                    {
                        if (ForceWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Force-of-Nature" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Wall of Flesh," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Wall of Flesh," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Vagrant of Space and Time
                    if (archiveListNumber == 13)//      |
                    {
                        if (VagrantWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Izanagi's Edge" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Vagrant." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Hawkmoon" +
                              "\n" +
                              "\nMagic/Ranged weapon." +
                              "\nFrom Vagrant." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat the Vagrant" +
                            "\nof Space and Time." +
                            "\n" +
                            "\n";
                        }
                    }
                    //Any Mechanical Boss
                    if (archiveListNumber == 14)//      |
                    {
                        if (MechBossWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Veneration of Butterflies" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom any Mech boss." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Memento Muse" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom any Mech boss." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat any mechanical" +
                            "\nboss." +
                            "\n" +
                            "\n";
                        }
                    }
                    //Any Mechanical Boss
                    if (archiveListNumber == 15)//      |
                    {
                        if (MonadoWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Xenoblade" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom any mechanical" +
                              "\nboss, then waiting." +
                              "\n";


                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat any mechanical boss," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Nalhaun, the Burnished King
                    if (archiveListNumber == 16)//      |
                    {
                        if (NalhaunWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Phantom In The Mirror" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Nalhaun." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Hollowheart Albion" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Nalhaun." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Nalhaun, the" +
                            "\nBurnished King." +
                            "\n" +
                            "\n";
                        }
                    }
                    //Skeletron Prime
                    if (archiveListNumber == 17)//      |
                    {
                        if (SkyStrikerWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Sky Striker Armaments" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom " +
                              "\nSkeletron Prime," +
                              "\nthen waiting.";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat" +
                            "\nSkeletron Prime," +
                            "\nthen wait." +
                            "\n";
                        }

                    }
                    //All Mechanical Bosses
                    if (archiveListNumber == 18)//      |
                    {
                        if (AllMechBossWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Ride The Bull" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom any Mech boss." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Drachenlance" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom any Mech boss." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat all mechanical" +
                            "\nbosses." +
                            "\n" +
                            "\n";
                        }
                    }
                    //All Mechanical Bosses
                    if (archiveListNumber == 19)//      |
                    {
                        if (HullwroughtWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Hullwrought" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom all Mech" +
                              "\nBosses." +
                              "\n";


                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat all" +
                            "\nMechanical Bosses." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Plantera
                    if (archiveListNumber == 20)//      |
                    {
                        if (PlanteraWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Crimson Outbreak" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Plantera." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Voice of the Fallen" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom Plantera." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Plantera." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Plantera
                    if (archiveListNumber == 21)//      |
                    {
                        if (KifrosseWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Kifrosse" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Plantera," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Plantera," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Frost Moon (Post Plantera)
                    if (archiveListNumber == 22)//      |
                    {
                        if (FrostMoonWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Stygian Nymph" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom the Ice Queen." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Caesura of Despair" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom the Ice Queen." +
                              "\n" +
                              "\n";
                            }
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat the Ice Queen." +
                            "\n" +
                            "\n" +
                            "\n";
                        }

                    }
                    //Penthesila, the Witch of Ink
                    if (archiveListNumber == 23)//      |
                    {
                        if (PenthesileaWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Vision of Euthymia" +
                              "\n" +
                              "\nSpecial weapon." +
                              "\nFrom Penthesilea." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Kroniic Principality" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Penthesilea." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Penthesilea," +
                            "\nthe Witch of Ink." +
                            "\n" +
                            "\n";
                        }
                    }
                    //Penthesila, the Witch of Ink
                    if (archiveListNumber == 24)//      |
                    {
                        if (MuseWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Penthesilea's Muse" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Penthesilea," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Penthesilea," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Golem
                    if (archiveListNumber == 25)//      |
                    {
                        if (GolemWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Plenilune Gaze" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Golem." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Tartaglia" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Golem." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Golem." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Golem
                    if (archiveListNumber == 26)//      |
                    {
                        if (GenocideWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Genocide" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Golem," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Golem," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Arbitration
                    if (archiveListNumber == 27)//      |
                    {
                        if (ArbitrationWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Liberation Blazing" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Arbitration." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Unforgotten" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Penthesilea." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Arbitration." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Arbitration
                    if (archiveListNumber == 28)//      |
                    {
                        if (ClaimhWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Claimh Solais" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Arbitration," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Arbitration," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Duke Fishron
                    if (archiveListNumber == 29)//      |
                    {
                        if (DukeFishronWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Key of the Sinner" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Duke Fishron." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Crimson Sakura Alpha" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Duke Fishron." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Duke Fishron." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Lunatic Cultist
                    if (archiveListNumber == 30)//      |
                    {
                        if (LunaticCultistWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Rex Lapis" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Lunatic Cultist." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Yunlai Stiletto" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Lunatic Cultist." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Lunatic" +
                            "\nCultist." +
                            "\n" +
                            "\n";
                        }
                    }
                    //Lunatic Cultist
                    if (archiveListNumber == 31)//      |
                    {
                        if (TwinStarsWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Twin Stars" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom " +
                              "\nLunatic Cultist," +
                              "\nthen waiting.";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat" +
                            "\nLunatic Cultist," +
                            "\nthen wait." +
                            "\n";
                        }

                    }
                    //Moon Lord
                    if (archiveListNumber == 32)//      |
                    {
                        if (MoonLordWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Suistrume" +
                              "\n" +
                              "\nSpecial weapon." +
                              "\nFrom Moon Lord." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Naganadel" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Moon Lord." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Moon Lord." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Moon Lord
                    if (archiveListNumber == 33)//      |
                    {
                        if (ShadowlessWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Shadowless Cerulean" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom the Moon Lord." +
                              "\n" +
                              "\n";


                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat the Moon" +
                            "\nLord." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Warrior of Light
                    if (archiveListNumber == 34)//      |
                    {
                        if (WarriorWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            if (chosenStarfarer == 1)
                            {
                                archiveListInfo = "Key of the King's Law" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Warrior of Light." +
                              "\n" +
                              "\n";
                            }
                            else
                            {
                                archiveListInfo = "Light Unrelenting" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom Warrior of Light." +
                              "\n" +
                              "\n";
                            }

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Warrior of Light." +
                            "\n" +
                            "\n" +
                            "\n";
                        }
                    }
                    //Tsukiyomi, the First Starfarer
                    if (archiveListNumber == 35)//      |
                    {
                        if (ArchitectWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Architect's Luminance" +
                              "\n" +
                              "\nMelee weapon." +
                              "\nFrom Tsukiyomi," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Tsukiyomi," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Tsukiyomi, the First Starfarer
                    if (archiveListNumber == 36)//      |
                    {
                        if (ArchitectWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Cosmic Destroyer" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Tsukiyomi," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Tsukiyomi," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Empress of Light
                    if (archiveListNumber == 37)//      |
                    {
                        if (NeedlepointWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Arachnid Needlepoint" +
                              "\n" +
                              "\nSummon weapon." +
                              "\nFrom Empress of Light," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Empress of Light," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Empress of Light (Master Mode
                    if (archiveListNumber == 38)//      |
                    {
                        if (MurasamaWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "The Only Thing I Know-" +
                              "\n" +
                              "\nSpatial weapon." +
                              "\nFrom Empress of Light," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Empress of Light," +
                            "\nthen wait." +
                            "\nMaster mode exclusive." +
                            "\n";
                        }

                    }
                    //Golem
                    if (archiveListNumber == 39)//      |
                    {
                        if (MercyWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Mercy" +
                              "\n" +
                              "\nSpatial weapon." +
                              "\nFrom Golem," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Golem" +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Empress of Light 
                    if (archiveListNumber == 40)//      |
                    {
                        if (SakuraWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Sakura's Vengeance" +
                              "\n" +
                              "\nSpatial weapon." +
                              "\nFrom Empress of Light," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Empress of Light," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Moon Lord
                    if (archiveListNumber == 41)//      |
                    {
                        if (EternalWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Eternal Star" +
                              "\n" +
                              "\nSpatial weapon." +
                              "\nFrom Moon Lord," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat the Moon Lord," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    //Moon Lord
                    if (archiveListNumber == 42)//      |
                    {
                        if (DaemonWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Vermilion Daemon" +
                              "\n" +
                              "\nSpatial weapon." +
                              "\nFrom Moon Lord," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat the Moon Lord," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 43)//      |
                    {
                        if (OzmaWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Ozma Ascendant" +
                              "\n" +
                              "\nMagic weapon." +
                              "\nFrom Cultist," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Lunatic Cultist," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 44)//      |
                    {
                        if (UrgotWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "Dreadnought Chemtank" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Queen Slime," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Queen Slime," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 45)//      |
                    {
                        if (BloodWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "The Blood Blade" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Pumpking," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Pumpking," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                    if (archiveListNumber == 46)//      |
                    {
                        if (MorningStarWeaponDialogue == 2)
                        {
                            canViewArchive = true;
                            archiveListInfo = "The Morning Star" +
                              "\n" +
                              "\nRanged weapon." +
                              "\nFrom Deerclops," +
                              "\nthen waiting." +
                              "\n";
                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = "Locked" +
                            "\n" +
                            "\nDefeat Deerclops," +
                            "\nthen wait." +
                            "\n" +
                            "\n";
                        }

                    }
                }//Weapon Dialogues

if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveChosenList == 0)
            {
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 2;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 3;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 3)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 4;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 4)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 5;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 5)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 6;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 6)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 7;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 7)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 8;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 8)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 9;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 9)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 10;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 10)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 11;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 11)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 12;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 12)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 13;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 13)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 14;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 14)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 15;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 15)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 16;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 16)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 17;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 17)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 18;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 18)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 19;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 19)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 20;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 20)
				{
					
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 21;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
					
					

				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 21)
				{

					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 301;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;


				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 22)
				{

					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 302;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;


				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 23)
				{

					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 303;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;


				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 24)
				{

					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 304;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;


				}
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveChosenList == 1)
			{
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 51;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 52;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 3)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 53;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 4)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 54;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 5)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 55;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 6)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 56;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 7)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 57;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 8)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 58;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 9)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 59;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 10)//10
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 60;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 11)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 61;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 12)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 62;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 13)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 63;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 14)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 64;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 15)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 65;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 16)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 66;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 17)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 67;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 18)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 68;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 19)
				{
					if(modPlayer.chosenStarfarer == 1)
                    {
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueScrollTimer = 0;
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueScrollNumber = 0;
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneID = 9;
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneProgression = 0;
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNDialogueActive = true;
					}
					if (modPlayer.chosenStarfarer == 2)
					{
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueScrollTimer = 0;
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueScrollNumber = 0;
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneID = 10;
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneProgression = 0;
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNDialogueActive = true;
					}
					
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 20)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 70;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 21)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 71;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 22)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 72;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 23)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 73;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				//CALAMITY
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 24)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 201;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 25)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 202;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 26)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 203;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 27)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 204;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 28)//End of pre hardmode
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 205;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 29)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 206;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 30)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 207;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 31)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 208;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 32)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 209;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 33)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 210;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 34)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 211;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 35)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 212;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 36)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 213;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 37)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 214;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}

			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveChosenList == 2)
			{
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 136;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 104;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 3)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 137;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 4)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 133;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 5)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 103;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 6)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 101;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 7)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 120;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 8)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 123;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 9)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 102;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 10)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 105;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 11)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 124;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 12)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 131;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 13)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 115;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 14)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 106;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 15)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 125;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 16)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 117;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 17)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 135;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 18)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 107;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 19)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 121;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 20)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 108;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 21)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 129;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 22)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 126;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 23)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 118;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 24)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 128;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 25)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 109;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 26)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 132;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 27)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 119;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 28)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 127;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 29)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 116;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 30)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 110;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 31)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 134;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 32)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 111;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 33)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 122;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 34)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 112;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 35)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 130;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 36)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 138;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 37)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 140;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 38)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 139;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 39)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 141;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 40)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 142;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 41)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 143;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 42)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 144;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 43)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 145;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 44)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 146;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 45)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 147;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 46)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 148;//
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				}
			}
 * 
 * 
 */