using Application.Interfaces;
using Application.Validations;
using AutoMapper;
using FluentValidation;
using MediatR;
using Domain.Entities;

namespace Application.Features.Positions;

public class CreatePositionCommand:IRequest<bool>
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public double Salary { get; set; }
}

public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IPositionRepository _positionRepository;

    public CreatePositionCommandHandler(IMapper mapper, IPositionRepository positionRepository)
    {
        _mapper = mapper;
        _positionRepository = positionRepository;
    }
    
    public async Task<bool> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        var validator = new PositionValidation();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var position = _mapper.Map<Position>(request);
        await _positionRepository.AddAsync(position);
        return true;
    }
}