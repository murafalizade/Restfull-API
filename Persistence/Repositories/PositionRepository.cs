using Application.Interfaces;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.Repositories;

public class PositionRepository:GenericRepository<Position>, IPositionRepository
{
    public PositionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}