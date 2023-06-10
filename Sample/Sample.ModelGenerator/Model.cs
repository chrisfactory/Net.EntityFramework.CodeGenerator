using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.App
{

    public class BasicEntity
    {
        [Column("IdColumName")]
        public int Id { get; set; }

        [Column("DataName")]
        public string Data { get; set; }
        //[Column("yoloId")]
        //public int FreindAnimalId { get; set; }
        [Column("DataNameV2")]
        public Animal FreindAnimal { get; set; }
    }

    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual string Species { get; set; }
        public int? FoodId { get; set; }
        public Food? Food { get; set; }

        //public Animal FreindAnimal { get; set; }
    }
    public abstract class Pet : Animal
    {
        public string? Vet { get; set; }
    }


    public class Cat : Animal
    {
        public string EducationLevel { get; set; }
        public override string Species => "Felis catus";

        public override string ToString()
            => $"Cat '{Name}' ({Species}/{Id}) with education '{EducationLevel}' eats {Food?.ToString() ?? "<Unknown>"}";
    }

    public class Dog : Pet
    {
        public int DogId { get; set; }
        public string FavoriteToy { get; set; }
        public override string Species => "Canis familiaris";

        public override string ToString()
            => $"Dog '{Name}' ({Species}/{Id}) with favorite toy '{FavoriteToy}' eats {Food?.ToString() ?? "<Unknown>"}";
    }



    [Table("CustomFood")]
    public class Food
    {
        [Column("FoodId")]
        public int Id { get; set; }
        [Column("NameAlias")]
        public string Name { get; set; }

    }




    [Table(nameof(MyAnimal), Schema = "CustomSchema")]
    public class MyAnimal
    {
        [Key]
        public int MyKey { get; set; }
        public string Name { get; set; }

        public Animal Animal { get; set; }
    }



}

