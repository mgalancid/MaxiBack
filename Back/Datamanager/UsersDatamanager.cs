using Back.Models;
using Back.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.Dto;

namespace back.Datamanager
{
    public class UsersDatamanager : IUsersRepository<Users, UsersDTO>
    {
        readonly CruzRojaContext _context;

        public UsersDatamanager(CruzRojaContext context)
        {
            _context = context;

        }

        //Obtiene todos los usuarios, en base al DTO.
        public IEnumerable<UsersDTO> GetAllDto()
        {
            if (_context != null)
            {
                return (from d in _context.Users
                        select new UsersDTO
                        {

                            IdUsers = d.IdUsers,
                            Name = d.Name,
                            LastName = d.LastName,
                            Dni = d.Dni,
                            Phone = d.Phone,
                            Email = d.Email,
                            Gender = d.Gender,
                            State = d.State,
                            BirthDate = d.BirthDate,
                            CreationDate = d.CreationDate,
                            IdRole = d.IdRole,
                        });

            }
            return null;

        }

        //Obtiene todos los usuarios por ID, en base al DTO.
        public UsersDTO GetDto(long id)
        {

            if (_context != null)
            {
                return (from d in _context.Users
                        where d.IdUsers == id

                        select new UsersDTO
                        {
                            IdUsers = d.IdUsers,
                            Name = d.Name,
                            LastName = d.LastName,
                            Dni = d.Dni,
                            Phone = d.Phone,
                            Email = d.Email,
                            Gender = d.Gender,
                            State = d.State,
                            BirthDate = d.BirthDate,
                            CreationDate = d.CreationDate,
                            IdRole = d.IdRole

                        }).FirstOrDefault();

            }
            return null;
        }



    }
}


