public class Enemy : Character
{
    public Enemy(string name, int health) : base(name, health) { }
    public override void Attack(Character target)
    {
        target.Health -= 5;
        Console.WriteLine($"{Name} hits {target.Name}, {target.Name} HP = {target.Health}");
    }
}