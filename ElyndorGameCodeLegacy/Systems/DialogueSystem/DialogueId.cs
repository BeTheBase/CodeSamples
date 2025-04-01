public enum DialogueID
{
    None,
    NPC_Greeting,
    Quit,
    SecondStep,
    LeftPath,
    RightPath,
    FollowLight,
    IgnoreLight,

    // User Agreement
    UserAgreement_Intro,
    UserAgreement_Disagree,

    // Headphones Check
    Headphones_Intro,
    Headphones_NoResponse,
    Headphones_Forced,
    Headphones_Confirmation,

    // Opening Narration
    Narrator_Welcome_02_Epic,
    AcceptAdventure_EpicAlt,
    DeclineAdventure_EpicAlt,
    SilentMode_EpicAlt,

    // Main Game Scenes
    Narrator_OpeningScene,
    Narrator_LockedDoor,
    Narrator_PuzzleHint,

    // Puzzle: Riddle of the Wind
    Puzzle_Riddle_Wind,
    Narrator_Puzzle_Riddle_Wind_CorrectAnswer,
    Narrator_Puzzle_Riddle_Wind_WrongAnswer,
    Narrator_GiveUp,
    Narrator_AlternativePath,

    // Exploration & Dynamic Narration
    Narrator_Wandering,
    Narrator_DoorOpens,

    // Special Events
    FoundShard,
    GreedyEnding,
    TruePath
}
