using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class UnitOfWork 
    {
        private readonly EvolentContactInformationContext _context;

        public UnitOfWork(EvolentContactInformationContext context)
        {
            _context = context;
        }

        private GenericRepository<Contact> _contactRepository;
        public GenericRepository<Contact> ContactRepository
        {
            get
            {
                return _contactRepository = _contactRepository ?? new GenericRepository<Contact>(_context);
            }
        }
    }
}
