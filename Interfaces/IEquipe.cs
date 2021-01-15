using System.Collections.Generic;
using Eplayers_AspNetCore.Models;

namespace Eplayers_AspNetCore.Interfaces
{
    public interface IEquipe
    {
         // chamando metodos de CRUD
         void Create(Equipe NovaEquipe);
         List<Equipe> ReadAll();
         void Update(Equipe NovaEquipe);
         void Delete(int idEquipe);
    }
}