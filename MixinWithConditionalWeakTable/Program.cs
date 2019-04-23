using System;
using System.Runtime.CompilerServices;

/// <summary>
/// How to create mixin using C# 4.0
/// https://www.c-sharpcorner.com/uploadfile/b942f9/how-to-create-mixin-using-C-Sharp-4-0/
/// </summary>
namespace MixinWithConditionalWeakTable
{
    public interface IAgeMixin // use 'M' prefix to indicate mixin interface
    {
        // nothing needed in here, it's just a 'marker' interface
    }

    public static class AgeMixinProvider // implements the mixin using extensions methods
    {
        static ConditionalWeakTable<IAgeMixin, Fields> table;

        static AgeMixinProvider()
        {
            table = new ConditionalWeakTable<IAgeMixin, Fields>();
        }

        private sealed class Fields // mixin's fields held in private nested class
        {
            internal DateTime BirthDate = DateTime.UtcNow;
        }

        public static int GetAge(this IAgeMixin map)
        {
            DateTime dtNow = DateTime.UtcNow;
            DateTime dtBorn = table.GetOrCreateValue(map).BirthDate;
            int age = ((dtNow.Year - dtBorn.Year) * 372
                       + (dtNow.Month - dtBorn.Month) * 31
                       + (dtNow.Day - dtBorn.Day)) / 372;
            return age;
        }

        public static void SetBirthDate(this IAgeMixin map, DateTime birthDate)
        {
            table.GetOrCreateValue(map).BirthDate = birthDate;
        }
    }

    public abstract class Animal
    {
        // contents unimportant
    }

    public class Human : Animal, IAgeMixin
    {
        public string Name;

        public Human(string name)
        {
            Name = name;
        }

        // nothing needed in here to implement IAgeMixin
    }

    class Program
    {
        static void Main(string[] args)
        {
            Human h = new Human("Jim");
            h.SetBirthDate(new DateTime(1980, 1, 1));
            Console.WriteLine("Name {0}, Age = {1}", h.Name, h.GetAge());

            Human h2 = new Human("Fred");
            h2.SetBirthDate(new DateTime(1960, 6, 1));
            Console.WriteLine("Name {0}, Age = {1}", h2.Name, h2.GetAge());

            Console.ReadKey();
        }
    }
}