using System.Collections.Generic;
using Models;

namespace DataServices
{
    public interface IPhonebookService : ICommonService<Phonebook>
    {
        List<Phonebook> GetUserPhonebook(int idUser);
        Phonebook Post(string name, string nickname, string cellphone, int idUser);
    }
}