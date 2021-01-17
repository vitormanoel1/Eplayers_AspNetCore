using System.Collections.Generic;
using Eplayers_AspNetCore.Models;

namespace Eplayers_AspNetCore.Interfaces
{
    public interface IJogador
    {
         // chamando os metodos de CRUD
         void Create(Jogador cadJogador);
         List<Jogador> ReadAll();
         void Update(Jogador cadJogador);
         void Delete(int IdJogador);
    }
}