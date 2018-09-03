using PhoneBook.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneBook.Data;

namespace PhoneBook.Services
{
    public class PhoneBookService : IPhoneBookService
    {
        private IRepository<PhoneBook.Data.PhoneBook> _phoneBookRepository;

        public PhoneBookService(IRepository<PhoneBook.Data.PhoneBook> phoneBookRepository)
        {
            _phoneBookRepository = phoneBookRepository;
        }

        public IEnumerable<PhoneBook.Data.PhoneBook> GetPhoneBooks()
        {
            return _phoneBookRepository.GetAll();
        }

        public PhoneBook.Data.PhoneBook GetPhoneBook(int Id)
        {
            return _phoneBookRepository.Get(Id);
        }

        public void InsertPhoneBook(PhoneBook.Data.PhoneBook user)
        {
            _phoneBookRepository.Insert(user);
        }
        public void UpdatePhoneBook(PhoneBook.Data.PhoneBook user)
        {
            _phoneBookRepository.Update(user);
        }

        public void DeletePhoneBook(int Id)
        {
            PhoneBook.Data.PhoneBook user = GetPhoneBook(Id);
            _phoneBookRepository.Delete(user);
        }
    }
}
