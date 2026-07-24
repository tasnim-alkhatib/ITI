public class Hero : Character, IMovable
{
    public Hero(string name, int health) : base(name, health) { }
    public void Move() => Console.WriteLine($"{Name} moved forward");
    public override void Attack(Character target)
    {
        target.Health -= 10;
        Console.WriteLine($"{Name} attacks {target.Name}, {target.Name} HP = {target.Health}");
    }
}