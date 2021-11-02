using System;
using System.Collections.Generic;
using System.Data;
using L4.Service;
using MySql.Data.MySqlClient;

namespace L4.Data
{
    public class Database
    {
        private const string ConnectionString = "" +
                                                "datasource=127.0.0.1;" + // adres domyślny dla serwera lokalnego
                                                "port=3306;" + // domyślny port
                                                "username=root;" + // domyślna nazwa użytkownika który ma dostęp do bazy danych
                                                "password=PASSWORD_HERE;" + // hasło dostępowe
                                                "SslMode = none;"; // tryb połączenia SSL

        private readonly MySqlConnection _databaseConnection = new(ConnectionString);

        private readonly IErrorHandler _errorHandler;

        public Database(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }

        public void OpenConnection()
        {
            try
            {
                _databaseConnection.Open();
            }
            catch (Exception ex)
            {
                _errorHandler.OnError(ex.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                _databaseConnection.Close();
            }
            catch (Exception ex)
            {
                _errorHandler.OnError(ex.Message);
            }
        }

        private MySqlCommand BuildCommand(string query)
        {
            return new(query, _databaseConnection)
            {
                CommandTimeout = 60
            };
        }

        public void Execute(string query)
        {
            MySqlCommand commandDatabase = BuildCommand(query);

            try
            {
                commandDatabase.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _errorHandler.OnError($"Failed to execute query: {ex.Message}\n\t{query}");
            }
        }

        public List<T> RetrieveData<T>(string query, Func<IDataRecord, T> parse)
        {
            MySqlCommand command = BuildCommand(query);

            var results = new List<T>();

            try
            {
                using var reader = command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                        results.Add(parse(reader));
            }
            catch (Exception ex)
            {
                _errorHandler.OnError(ex.Message);
            }

            return results;
        }
    }
}