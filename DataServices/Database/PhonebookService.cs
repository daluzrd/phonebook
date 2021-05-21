using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Models;

namespace DataServices
{
    public class PhonebookService : CommonService<Phonebook>, IPhonebookService
    {
        private readonly IUserService _userService;
        public PhonebookService(DataContext dbContext, IUserService userService) : base(dbContext) 
        {
            _userService = userService;
        }

        public List<Phonebook> GetUserPhonebook(int idUser)
        {
            if(idUser == 0)
                throw new ArgumentException("idUser is required.");

            if(_userService.GetById(idUser) == null)
                throw new NotFoundException("User not found.");

            return context.Phonebooks.Where(p => p.IdUser == idUser).ToList();
        }
        public Phonebook Post(string name, string nickname, string cellphone, int idUser)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name is required.");
            if(string.IsNullOrWhiteSpace(cellphone))
                throw new ArgumentException("cellphone is required.");
            if(idUser == 0)
                throw new ArgumentException("idUser is required.");

            if(_userService.GetById(idUser) == null)
                throw new NotFoundException("idUser is invalid.");

            Phonebook phonebook = new Phonebook();
            phonebook.Name = name;
            phonebook.Nickname = nickname;
            phonebook.Cellphone = cellphone;
            phonebook.IdUser = idUser;

            context.Add(phonebook);
            context.SaveChanges();

            return phonebook;     
        }
    }
}