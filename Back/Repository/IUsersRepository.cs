using Back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Repository
{
    public interface IUsersRepository<TEntity, TDto>
    {
        IEnumerable<TDto> GetAllDto();
        TDto GetDto(long id); //


    }
}
