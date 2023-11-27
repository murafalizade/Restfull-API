using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Positions;

public class GetAllPositionsQuery:IRequest<List<PositionDTO>>
{
}

public class GetAllPositionsQueryHandler:IRequestHandler<GetAllPositionsQuery, List<PositionDTO>>
{
    private readonly IMapper _mapper;
    private readonly IPositionRepository _positionRepository;

    public GetAllPositionsQueryHandler(IMapper mapper, IPositionRepository positionRepository)
    {
        _mapper = mapper;
        _positionRepository = positionRepository;
    }
    public async Task<List<PositionDTO>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<PositionDTO>>(await _positionRepository.GetAllAsync());
    }
}