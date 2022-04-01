using Microsoft.EntityFrameworkCore;
using QuoteOfTheDay.Data.Context;
using QuoteOfTheDay.Data.Repositories.Interfaces;
using QuoteOfTheDay.Entities;
using System.Collections.Generic;
using System.Linq;

namespace QuoteOfTheDay.Data.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly AppDbContext _dbContext;

        public QuoteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Quote> GetAll()
        {
            return _dbContext
                    .Quotes
                    .Include(q => q.Category)
                    .ToList();
        }

        public Quote GetById(int id)
        {
            return _dbContext
                    .Quotes
                    .Include(q => q.Category)
                    .FirstOrDefault(q => q.Id == id);
        }

        public Quote AddQuote(Quote quote)
        {
            _dbContext.Add(quote);

            _dbContext.SaveChanges();

            _dbContext
                .Entry(quote)
                .Reference(q => q.Category)
                .Load();

            return quote;
        }

        public Quote UpdateQuote(Quote quote)
        {
            _dbContext.Attach(quote);

            _dbContext.Entry(quote).State = EntityState.Modified;

            _dbContext.SaveChanges();

            _dbContext
                .Entry(quote)
                .Reference(q => q.Category)
                .Load();

            return quote;
        }

        public bool DeleteQuote(int id)
        {
            var quote = GetById(id);

            if (quote != null)
                _dbContext.Remove(quote);

            _dbContext.SaveChanges();

            return quote != null;
        }
    }
}
