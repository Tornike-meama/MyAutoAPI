using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using MyAutoAPI1.Controllers.GetBody.Statement;
using MyAutoAPI1.Models;
using MyAutoAPI1.Models.Responses;
using MyAutoAPI1.Services.Currency;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services
{
    public class StatementServices : IStatementServices
    {
        private readonly MyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrencyServices _currencyServices;

        public StatementServices(MyDbContext dbContext, IMapper mapper, ICurrencyServices currencyServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currencyServices = currencyServices;
        }

        public async Task<IComonResponse<List<Statement>>>GetAllStatementsAsync(int count, int fromIndex)
        {
            try
            {
                if (count == 0)
                {
                    count = 10;
                }

                var res = await _dbContext.Statement.Skip(fromIndex).Take(count).ToListAsync();

                return new ComonResponse<List<Statement>>(res);
            }
            catch (Exception ex)
            {
                return new BadRequest<List<Statement>>(ex.Message);
            }
        }
        public async Task<IComonResponse<Statement>>GetStatementByIdAsync(int id)
        {
            try
            {
                var res = await _dbContext.Statement.FirstOrDefaultAsync(o => o.Id == id);
                if(res == null)
                {
                    return new NotFound<Statement>("Not found");
                }
                return new ComonResponse<Statement>(res);
            }
            catch (Exception ex)
            {
                return new BadRequest<Statement>(ex.Message);
            }
        }
        public async Task<IComonResponse<List<Statement>>>GetStatementByUserIdAsync(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return new BadRequest<List<Statement>>("you don't provide userId");
                }

                var res = await _dbContext.Statement.Where(o => o.Creator.CompareTo(userId) == 0).ToListAsync();

                return new ComonResponse<List<Statement>>(res);
            }
            catch (Exception ex)
            {
                return new BadRequest<List<Statement>>(ex.Message);
            }
        }
        public async Task<IComonResponse<Statement>>AddStatementAsync(AddStatementModel data, string creatorId)
        {
            try
            {
                var isUser = _dbContext.Users.Any(o => o.Id == creatorId);
                if(!isUser) new NotFound<Statement>("Can't find Creator User");

                var currentCurrency = await _currencyServices.GetCurrencyByIdAsync(data.CurrencyId);
                if (currentCurrency.IsError) return new NotFound<Statement>("Can't find Currency");

                var mappedResponse = _mapper.Map<Statement>(data);

                var statement = _mapper.Map<Statement>(data);

                StatementValidator validaor = new StatementValidator();
                ValidationResult validationResult = validaor.Validate(statement);

               if (!validationResult.IsValid) return new BadRequest<Statement>(string.Join(", ", validationResult.Errors.Select(o => o.ErrorMessage)));

                _dbContext.Statement.Add(statement);
                await _dbContext.SaveChangesAsync();

                return new ComonResponse<Statement>(statement); 
            }
            catch (Exception ex)
            {
                return new BadRequest<Statement>(ex.Message);
            }
        }
        public async Task<IComonResponse<Statement>>UpdateStatementAsync(UpdateStatement data, string creatorId)
        {
            try
            {
                var currentCurrency = await _currencyServices.GetCurrencyByIdAsync(data.CurrencyId);
                if (data.CurrencyId < 1 || currentCurrency.IsError)
                {
                    return new NotFound<Statement>("Currency not found");
                }

                var statement = _dbContext.Statement.FirstOrDefault(o => o.Id == data.Id);
                if(statement == null)
                {
                    return new NotFound<Statement>("Statement not found invalid ID");
                }

                if(statement.Creator != creatorId)
                {
                    return new NotFound<Statement>("You can't edit other statement");
                }

                statement.Title = data.Title;
                statement.Description = data.Description;
                statement.Price = data.Price;
                statement.CurrencyId = data.CurrencyId;

                var dataResponse = new Statement()
                {
                    Id = statement.Id,
                    Creator = creatorId,
                    Title = data.Title,
                    Description = data.Description,
                    Price = data.Price,
                    CurrencyId = data.CurrencyId,
                };

                await _dbContext.SaveChangesAsync();
                return new ComonResponse<Statement>(dataResponse); ;
            }
            catch (Exception ex)
            {
                return new BadRequest<Statement>(ex.Message);
            }
        }
        public async Task<IComonResponse<Statement>>DeleteStatementAsync(int id, string userId)
        {
            try
            {
                var deleteStatement = _dbContext.Statement.FirstOrDefault(o => o.Id == id);

                if(deleteStatement == null)
                {
                    return new BadRequest<Statement>("Statement not found");
                }

                if(deleteStatement.Creator != userId)
                {
                    return new BadRequest<Statement>("User only can delete own statement");
                }

                _dbContext.Statement.Remove(deleteStatement);
                await _dbContext.SaveChangesAsync();
                return new ComonResponse<Statement>(deleteStatement); ;
            }
            catch (Exception ex)
            {
                return new BadRequest<Statement>(ex.Message);
            }
        }
    }
}
