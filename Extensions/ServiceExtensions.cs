using Npgsql;

namespace matelso.contactmanager.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection MigrateDatabase(this IServiceCollection services, string wehat)
        {

            try
            {

                using var connection = new NpgsqlConnection
                (wehat);
                connection.Open();

                using var command = new NpgsqlCommand
                {
                    Connection = connection
                };

                command.CommandText = "DROP TABLE IF EXISTS Contact";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Contact(Id uuid, 
                                                                Salutation VARCHAR(5),
                                                                Firstname VARCHAR(24) NOT NULL,
                                                                Lastname VARCHAR(24) NOT NULL,
                                                                Displayname VARCHAR(50),
                                                                Birthdate timestamp,                                                    
                                                                CreationTimestamp timestamp,
                                                                LastChangeTimestamp timestamp,
                                                                NotifyHasBirthdaySoon boolean,
                                                                Email VARCHAR(50) UNIQUE NOT NULL,
                                                                Phonenumber VARCHAR(24),
                                                                PRIMARY KEY (Id))";
                command.ExecuteNonQuery();
                Console.WriteLine("Table creation is succesful");
                Guid TrialGuid = Guid.NewGuid();
                command.CommandText =
                "INSERT INTO Contact"
                + "(Id, Salutation, Firstname, Lastname, Displayname, "
                + "Birthdate, CreationTimestamp, LastChangeTimestamp, NotifyHasBirthdaySoon, Email, Phonenumber)"
                + $"VALUES ('{TrialGuid}','Mr', 'Can' , 'Dincer' ,'','2016-06-22 19:10:25-07',"
                + $"'{DateTime.Now}','{DateTime.Now}', true,'trialrun1@email.com','02123445566')";
                command.ExecuteNonQuery();
                Console.WriteLine("First test user created");
                TrialGuid = Guid.NewGuid();
                command.CommandText =
                "INSERT INTO Contact"
                + "(Id, Salutation, Firstname, Lastname, Displayname, "
                + "Birthdate, CreationTimestamp, LastChangeTimestamp, NotifyHasBirthdaySoon, Email, Phonenumber)"
                + $"VALUES ('{TrialGuid}','Mr', 'Cem' , 'Dicer' ,'','2014-06-22 19:10:25-07',"
                + $"'{DateTime.Now}', '{DateTime.Now}', true,'trialrun2@email.com','02123558899')";
                command.ExecuteNonQuery();
                Console.WriteLine("Second test user created");
                command.CommandText =
               "INSERT INTO Contact"
               + "(Id, Salutation, Firstname, Lastname, Displayname, "
               + "Birthdate, CreationTimestamp, LastChangeTimestamp, NotifyHasBirthdaySoon, Email, Phonenumber)"
               + $"VALUES ('4b2056a9-7ee4-47b1-a64f-15770ceab7aa','Ms', 'Kimberly' , 'Director' ,'KimDirector','1974-11-13T19:10:25',"
               + $"'{DateTime.Now}', '{DateTime.Now}', true,'trialrun3@email.com','02124669900')";
                command.ExecuteNonQuery();
                Console.WriteLine("Third test user created / stricly for update user scenario");

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Database connection or table creation failed" + ex.Message);
            }

            return services;
        }
    }
}
