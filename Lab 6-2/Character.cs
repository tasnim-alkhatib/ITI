public abstract class Character
{
    public string? Name;
    public int Health;

    public Character(string name, int health)
    {
        Name = name;
        Health = health;
    }
    public bool IsAlive() => Health > 0;
    public abstract void Attack(Character target);
    public override string ToString() => $"Name: {Name}\nHealth: {Health}";
}