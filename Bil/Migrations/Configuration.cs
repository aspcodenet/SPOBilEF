namespace Bil.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bil.Models.BilModel2>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Bil.Models.BilModel2";
        }

        protected override void Seed(Bil.Models.BilModel2 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Bilar.AddOrUpdate(b => b.Id,
                new Models.Bil { Id=1,Color="Blue",Manufacturer="Volvo",Model="XC90",Price=0,Year=2015 ,NumberOfWheels=4},
                new Models.Bil { Id = 2, Color = "Green", Manufacturer = "Ford", Model = "Focus", Price = 0, Year = 2014, NumberOfWheels = 4 },
                new Models.Bil { Id = 3, Color = "Yellow", Manufacturer = "Skoda", Model = "Octavia", Price = 0, Year = 2014, NumberOfWheels = 4 }
                );
        }
    }
}
