using PomoAssistant.Core;

namespace PomoAssistant.Services;

public sealed class PomoService : IPomoService
{
    private readonly List<Pomo> _pomos = [];
    
    public PomoModels.ReadModel? GetById(Guid id)
    {
        Pomo? pomo = _pomos.FirstOrDefault(p => p.Id == id);
        return pomo is null ? null : Map(pomo);
    }

    public List<PomoModels.ReadModel> Search(PomoModels.SearchRequest request)
    {
        return _pomos
            .Where(p =>
                (request.From is null || p.CompletedAt >= request.From.Value) && 
                (request.To is null || p.CompletedAt <= request.To.Value))
            .Select(Map)
            .ToList();
    }

    public PomoModels.ReadModel Create(PomoModels.CreateRequest request)
    {
        Pomo pomo = new(request.CompletedAt, request.Intent)
        {
            Description = request.Description,
        };
        _pomos.Add(pomo);
        return Map(pomo);
    }

    public PomoModels.ReadModel? Update(PomoModels.UpdateRequest request)
    {
        Pomo? pomo = _pomos.FirstOrDefault(p => p.Id == request.Id);
        if (pomo is null)
        {
            return null;
        }
        pomo.CompletedAt = request.CompletedAt;
        pomo.Intent = request.Intent;
        pomo.Description = request.Description;
        return Map(pomo);
    }

    public PomoModels.ReadModel? Patch(PomoModels.PatchRequest request)
    {
        Pomo? pomo = _pomos.FirstOrDefault(p => p.Id == request.Id);
        if (pomo is null)
        {
            return null;
        }

        if (request.CompletedAt != null)
        {
            pomo.CompletedAt = request.CompletedAt.Value;
        }

        if (request.Intent != null)
        {
            pomo.Intent = request.Intent.Value;
        }

        if (request.Description != null)
        {
            pomo.Description = request.Description;
        }
        return Map(pomo);
    }

    public void Delete(Guid id)
    {
        _pomos.RemoveAll(p => p.Id == id);
    }

    private PomoModels.ReadModel Map(Pomo pomo)
    {
        return new PomoModels.ReadModel
        {
            Id = pomo.Id,
            CompletedAt = pomo.CompletedAt,
            Intent = pomo.Intent,
            Description = pomo.Description,
            DurationInMinutes = pomo.DurationInMinutes
        };
    }
}