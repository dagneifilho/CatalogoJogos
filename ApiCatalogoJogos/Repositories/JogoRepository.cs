using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.Entities;

namespace ApiCatalogoJogos.Repositories {
    public class JogoRepository : IJogoRepository {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>() {
            {Guid.Parse("6f7ea09e-e836-4d20-8cf9-4b22f15caa25"), new Jogo{Id = Guid.Parse("6f7ea09e-e836-4d20-8cf9-4b22f15caa25") , Nome = "Fifa 21",Produtora = "EA", Preco = 200} },
            {Guid.Parse("ea10490f-98ca-4bed-9059-29a1a77dfe14"), new Jogo{ Id = Guid.Parse("ea10490f-98ca-4bed-9059-29a1a77dfe14"),Nome =  "NBA 2k21", Produtora = "2K Sports", Preco = 250 } },
            {Guid.Parse("2417b14c-26d9-4518-8e5a-436f31b993fe"), new Jogo{Id = Guid.Parse("2417b14c-26d9-4518-8e5a-436f31b993fe"), Nome = "GTA V", Produtora = "Rockstar", Preco = 150} }

        };

        public Task<List<Jogo>> Obter(int pagina, int quantidade) {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }
        public Task<Jogo> Obter(Guid id) {
            if (!jogos.ContainsKey(id))
                return null;
            return Task.FromResult(jogos[id]);
        }
        public Task<List<Jogo>>Obter(string nome, string produtora) {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }
        public Task<List<Jogo>> ObterSemLambda(string nome, string produtora) {
            var retorno = new List<Jogo>();
            foreach(var jogo in jogos.Values) {

                if (jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                    retorno.Add(jogo);
            }
            return Task.FromResult(retorno);
        }
        public Task Inserir(Jogo jogo) {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }
        public Task Atualizar(Jogo jogo) {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }
        public Task Remover(Guid id) {
            jogos.Remove(id);
            return Task.CompletedTask;
        }
        public void Dispose() {
            //Fecha conexão com o banco de dados.
        }
    }
}

