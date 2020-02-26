using BikeWatcher.Models;
using System;
using System.Linq;
using BikeWatcher.Data;

namespace BikeWatcher.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Favoris.Any())
            {
                return;   // DB has been seeded
            }

            var favories = new Favoris[]
            {
            new Favoris{IDStation=16005},
            new Favoris{IDStation=16045}

            };
            foreach (Favoris f in favories)
            {
                context.Favoris.Add(f);
            }
            context.SaveChanges();

            if (!context.User.Any())
            {
                var utilisateur = new User()
                {
                    Id = 1,
                    Mail = "test@test.test",
                    Password = "toto",
                };
                context.User.Add(utilisateur);
                context.SaveChanges();
            }

            context.Database.EnsureCreated();

            // Look for any students.
            if (context.SignVelo.Any())
            {
                return;   // DB has been seeded
            }

            var signVelos = new SignVelo[]
            {
                new SignVelo{IdVelo = 125,Commentaire = "Velo",Email = "Test@test.test"},

            };
            foreach (SignVelo f in signVelos)
            {
                context.SignVelo.Add(f);
            }
            context.SaveChanges();


        }


    }
}