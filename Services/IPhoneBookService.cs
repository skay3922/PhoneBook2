using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneBook.Data;

namespace PhoneBook.Services
{
    public interface IPhoneBookService
    {
        IEnumerable<PhoneBook.Data.PhoneBook> GetPhoneBooks();
        PhoneBook.Data.PhoneBook GetPhoneBook(int Id);
        void InsertPhoneBook(PhoneBook.Data.PhoneBook phoneBook);
        void UpdatePhoneBook(PhoneBook.Data.PhoneBook phoneBook);
        void DeletePhoneBook(int id);
    }
}
