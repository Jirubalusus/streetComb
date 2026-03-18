namespace StreetComb.Combat
{
    public enum CombatState
    {
        Idle = 0,
        Startup = 1,
        ActiveAttack = 2,
        Recovery = 3,
        Block = 4,
        HitStun = 5,
        Dash = 6,
        Airborne = 7,
        Special = 8,
        KO = 9,
    }
}
