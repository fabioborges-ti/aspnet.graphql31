using QuoteOfTheDay.Entities;
using System.Collections.Generic;

namespace QuoteOfTheDay.Data.Repositories.Interfaces
{
    public interface IQuoteRepository
    {
        IEnumerable<Quote> GetAll();
        Quote GetById(int id);
        Quote AddQuote(Quote quote);
        Quote UpdateQuote(Quote quote);
        bool DeleteQuote(int id);
    }
}
