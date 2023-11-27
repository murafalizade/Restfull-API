using Application.DTOs;
using Application.Features.Positions;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class GeneralMapping:Profile
{
    public GeneralMapping()
    {
        CreateMap<Position, PositionDTO>();
        CreateMap<CreatePositionCommand, Position>();
    }   
}