namespace RoRService.Models.DataModels.Enums
{
    public enum CardStatus
    {
        Active,                               // A war that is active
        Assigned,                             // Owned by a Senator (Concessions or Open Body Guard)
        Dead,                                 // Killed Senator
        Deck,                                 // Not yet drawn.
        Destroyed,                            // Concessions destroyed
        Discarded,                            // Card used and discarded
        Enacted,                              // Laws that have be played
        Exile,                                // Statesman in exile
        Forum,                                // In the forum
        Governor,                             // A Senator / Statesman away governing a province
        Hand,                                 // In the hand of a faction
        Inactive,                             // A war that is inactive
        InPlay,                               // In play by a faction
        LeadingArmy,                          // Send to lead Senate Army
        Rebel,                                // A Senator / Statesman siding with a Rebel leader.
        RebelLeader                           // A Senator / Statesman in control of an army who has rebelled.
    }

}