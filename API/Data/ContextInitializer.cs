namespace API.Data
{
    public static class ContextInitializer
    {
        public static async Task InitializeAsync(Context context)
        {
            if (context.Database.GetPendingMigrations().Count() > 0)
            {
                await context.Database.MigrateAsync();
            }

            if (!context.Country.Any())
            {
                foreach(var country in SD.GetCountries())
                {
                    context.Country.Add(new Country { Name = country });
                }
            }

            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var canadaId = await context.Country.Where(x => x.Name == "Canada").Select(x => x.Id).FirstOrDefaultAsync();
                var usaId = await context.Country.Where(x => x.Name == "United-States-of-America").Select(x => x.Id).FirstOrDefaultAsync();
                var ukId = await context.Country.Where(x => x.Name == "United-Kingdom").Select(x => x.Id).FirstOrDefaultAsync();
                var germanyId = await context.Country.Where(x => x.Name == "Germany").Select(x => x.Id).FirstOrDefaultAsync();

                var john = new ApplicationUser
                {
                    UserName = "john",
                    PlayerName = "john",
                    Password = SD.GetDefaultPassword(),
                    CountryId = canadaId
                };
                context.Users.Add(john);

                var peter = new ApplicationUser
                {
                    UserName = "peter",
                    PlayerName = "peter",
                    Password = SD.GetDefaultPassword(),
                    CountryId = usaId
                };
                context.Users.Add(peter);

                var tom = new ApplicationUser
                {
                    UserName = "tom",
                    PlayerName = "tom",
                    Password = SD.GetDefaultPassword(),
                    CountryId = ukId
                };
                context.Users.Add(tom);

                var barb = new ApplicationUser
                {
                    UserName = "barb",
                    PlayerName = "barb",
                    Password = SD.GetDefaultPassword(),
                    CountryId = germanyId
                };
                context.Users.Add(barb);

                if (context.ChangeTracker.HasChanges())
                {
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
