using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.App
{
    public class Food
    {
         

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public   class Animal
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual string Species { get; set; }

        public Food? Food { get; set; }
    }

    public abstract class Pet : Animal
    {
         
        public string? Vet { get; set; }

        public ICollection<Human> Humans { get; } = new List<Human>();
    }

    public class FarmAnimal : Animal
    {
         

        public override string Species { get; set; }

        [Precision(18, 2)]
        public decimal Value { get; set; }

        public override string ToString()
            => $"Farm animal '{Name}' ({Species}/{Id}) worth {Value:C} eats {Food?.ToString() ?? "<Unknown>"}";
    }

    public class Cat : Pet
    { 
        public string EducationLevel { get; set; }
        public override string Species => "Felis catus";

        public override string ToString()
            => $"Cat '{Name}' ({Species}/{Id}) with education '{EducationLevel}' eats {Food?.ToString() ?? "<Unknown>"}";
    }

    public class Dog : Pet
    {
        

        public string FavoriteToy { get; set; }
        public override string Species => "Canis familiaris";

        public override string ToString()
            => $"Dog '{Name}' ({Species}/{Id}) with favorite toy '{FavoriteToy}' eats {Food?.ToString() ?? "<Unknown>"}";
    }

    public class Human : Animal
    { 

        public override string Species => "Homo sapiens";

        public Animal? FavoriteAnimal { get; set; }
        public ICollection<Pet> Pets { get; } = new List<Pet>();

        public override string ToString()
            => $"Human '{Name}' ({Species}/{Id}) with favorite animal '{FavoriteAnimal?.Name ?? "<Unknown>"}'" +
               $" eats {Food?.ToString() ?? "<Unknown>"}";
    }
}

