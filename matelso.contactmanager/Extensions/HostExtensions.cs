using Npgsql;

namespace matelso.contactmanager.Extensions
{
    public static class HostExtensions
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
                                                                NotifyHasBirthdaySoon boolean,
                                                                Email VARCHAR(50) UNIQUE NOT NULL,
                                                                Phonenumber VARCHAR(24),
                                                                PRIMARY KEY (Id))";
                command.ExecuteNonQuery();
                Console.WriteLine("Table creation is succesful");
                Guid TrialGuid = Guid.NewGuid();
                command.CommandText =
                   "INSERT INTO Contact"
                   + "(Id,Salutation, Firstname, Lastname, Displayname, "
                   + "Birthdate,CreationTimestamp,NotifyHasBirthdaySoon,Email,Phonenumber)"
                   + $"VALUES ('{TrialGuid}','Mr', 'Can' , 'Dincer' ,'','2016-06-22 19:10:25-07',"
                   + $"'{DateTime.Now}', true,'trialrun1@email.com','02123445566')";
                command.ExecuteNonQuery();
                Console.WriteLine("First test result created");
                TrialGuid = Guid.NewGuid();
                command.CommandText =
                "INSERT INTO Contact"
                + "(Id,Salutation, Firstname, Lastname, Displayname, "
                + "Birthdate,CreationTimestamp,NotifyHasBirthdaySoon,Email,Phonenumber)"
                + $"VALUES ('{TrialGuid}','Mr', 'Cem' , 'Dicer' ,'','2014-06-22 19:10:25-07',"
                + $"'{DateTime.Now}', true,'trialrun2@email.com','02123558899')";
                command.ExecuteNonQuery();

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Database connection or table creation failed" + ex.Message);
            }

            return services;
        }
    }
}
