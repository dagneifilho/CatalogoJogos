using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using ApiCatalogoJogos.Entities;
using Microsoft.Extensions.Configuration;

namespace ApiCatalogoJogos.Repositories {
    public class JogoSqlServerRepository : IJogoRepository {
        private readonly SqlConnection sqlConnection;


        public JogoSqlServerRepository (IConfiguration configuration) {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Jogo>> Obter(int pagina, int quantidade) {
            var jogos = new List<Jogo>();
            var comando = $"select * from Jogos order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read()) {
                //estava com problemas para na conversão do id para guid e do preco para double.
                //encontrei esta solução.
                string idString = (string)sqlDataReader["Id"];
                double preco = (float)sqlDataReader["Preco"];
                jogos.Add(new Jogo {
                    Id = Guid.Parse(idString),
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = preco
                }); ;
            }
            await sqlConnection.CloseAsync();
            return jogos;
        }
        public async Task<Jogo> Obter(Guid id) {
            Jogo jogo = null;
            var comando = $"select * from jogos where Id = '{id}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read()) {
                string idString = (string)sqlDataReader["Id"];
                double preco = (float)sqlDataReader["Preco"];
                jogo = new Jogo {
                    Id = Guid.Parse(idString),
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = preco
                };
            }

            await sqlConnection.CloseAsync();
            return jogo;
        }
        public async Task<List<Jogo>> Obter (string nome, string produtora) {
            var jogos = new List<Jogo>();
            var comando = $"select * from jogos where Nome = '{nome}' and Produtora = '{produtora}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read()) {
                string idString = (string)sqlDataReader["Id"];
                double preco = (float)sqlDataReader["Preco"];
                jogos.Add(new Jogo {
                    Id =Guid.Parse(idString),
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = preco
                });
            }
            await sqlConnection.CloseAsync();
            return jogos;
        }
        public async Task Inserir(Jogo jogo) {
            await sqlConnection.OpenAsync();
            var comando = $"insert Jogos (Id, Nome, Produtora, Preco) values ('{jogo.Id}','{jogo.Nome}','{jogo.Produtora}','{jogo.Preco.ToString().Replace(",",".")}')";
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
        public async Task Atualizar (Jogo jogo) {
            var comando = $"update Jogos set Nome = '{jogo.Nome}', Produtora = '{jogo.Produtora}', Preco = '{jogo.Preco.ToString().Replace(",", ".")}' where Id = '{jogo.Id}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
        public async Task Remover (Guid id) {
            var comando = $"delete from Jogos where Id = '{id}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
        public void Dispose() {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
