[System.Serializable]
public class MonsterStats
{
    public int hp;
    public int attack;

    // Initialize the monster's stats
    public void InitializeStats(int initialHP, int initialAttack)
    {
        hp = initialHP;
        attack = initialAttack;
    }

    // Method to modify HP
    public void ModifyHP(int amount)
    {
        hp += amount;
    }

    // Method to modify Attack
    public void ModifyAttack(int amount)
    {
        attack += amount;
    }
}
