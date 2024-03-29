﻿using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        #region Dependencies

        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        #endregion

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found!");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task RemoveAsync(int id)
        {
            //Método para alterar o StatusId para deleted
            var obj = await _context.Seller.FindAsync(id);
            obj.StatusId = Models.Enums.StatusId.Deleted;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

            //Método para deletar do banco
            //try
            //{
            //    var obj = await _context.Seller.FindAsync(id);

            //    _context.Seller.Remove(obj);
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    throw new IntegrityException("Can't delete seller because he/she has sales");
            //}
        }

        #region Methods

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.Where(x => x.StatusId == Models.Enums.StatusId.Active).ToListAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        #endregion
    }
}
