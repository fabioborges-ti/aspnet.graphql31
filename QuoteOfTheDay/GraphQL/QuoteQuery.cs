using GraphQL;
using GraphQL.Types;
using QuoteOfTheDay.Data.Repositories.Interfaces;
using QuoteOfTheDay.GraphQL.Types;
using System;

namespace QuoteOfTheDay.GraphQL
{
    public class QuoteQuery : ObjectGraphType
    {
        public QuoteQuery(Defer<IQuoteRepository> quoteRepository)
        {
            Field<ListGraphType<QuoteType>>("quotes",
                resolve: context => quoteRepository.Value.GetAll());

            Field<QuoteType>("quote",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                {
                    Name = "id"
                }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");

                    return quoteRepository.Value.GetById(id);
                });
        }
    }
}
