using FunctionApp1.Models;
using System;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace FunctionApp1.Repositorios
{
    public class RepositorioPessoa
    {
        private readonly SQLiteConnection sqliteConnection;

        public RepositorioPessoa()
        {
            try
            {
                var path = Path.Combine(Environment.CurrentDirectory, "db.sqlite");
                if (!File.Exists(path))
                {
                    SQLiteConnection.CreateFile(path);

                }

                sqliteConnection = new SQLiteConnection($"Data Source={path};Version=3;");
                sqliteConnection.Open();

                string sql = @"CREATE TABLE IF NOT EXISTS Pessoa (Id int , Nome varchar(50) ,Email varchar(80))";

                SQLiteCommand command = new SQLiteCommand(sql, sqliteConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void AdicionarPessoa(Pessoa pessoa)
        {
            try
            {
                using (var cmd = sqliteConnection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Pessoa(id, Nome, email ) values (@id, @nome, @email)";
                    cmd.Parameters.AddWithValue("@Id", pessoa.Id);
                    cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@Email", pessoa.Email);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Pessoa BuscarPessoa(int id)
        {
            try
            {
                using (var cmd = sqliteConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Pessoa Where Id=" + id;
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        return new Pessoa
                        {
                            Nome = reader["Nome"].ToString(),
                            Email = reader["Email"].ToString(),
                            Id = int.Parse(reader["Id"].ToString())
                        };
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}