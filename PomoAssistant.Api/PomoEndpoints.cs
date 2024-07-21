using PomoAssistant.Core;
using PomoAssistant.Services;

namespace PomoAssistant.Api;

public static class PomoEndpoints
{
    public static void AddPomoEndpoints(this IEndpointRouteBuilder app)
    {
        var pomoGroup = app.MapGroup("/pomos");
        pomoGroup.MapGet("/{id}", GetById);
        pomoGroup.MapGet("/", Search);
        pomoGroup.MapPost("/", Create);
        pomoGroup.MapPut("/{id}", Update);
        pomoGroup.MapPatch("/{id}", Patch);
        pomoGroup.MapDelete("/{id}", Delete);
    }

    private static IResult GetById(Guid id, IPomoService service)
    {
        var result = service.GetById(id);
        return result is null ? Results.NotFound() : Results.Ok(result);
    }

    private static IResult Search(DateTime? from, DateTime? to, IPomoService service)
    {
        var request = new PomoModels.SearchRequest{ From = from, To = to };
        var result = service.Search(request);
        return Results.Ok(result);
    }

    private static IResult Create(PomoRequest pomoRequest, IPomoService service)
    {
        if (pomoRequest.CompletedAt is null ||
            pomoRequest.Intent is null ||
            pomoRequest.Description is null)
        {
            return Results.BadRequest();
        }
        var serviceRequest = new PomoModels.CreateRequest
        {
            CompletedAt = pomoRequest.CompletedAt.Value,
            Intent = pomoRequest.Intent.Value,
            Description = pomoRequest.Description,
        };
        var result = service.Create(serviceRequest);
        return Results.Ok(result);
    }

    private static IResult Update(Guid id, PomoRequest pomoRequest, IPomoService service)
    {
        if (pomoRequest.CompletedAt is null ||
            pomoRequest.Intent is null ||
            pomoRequest.Description is null)
        {
            return Results.BadRequest();
        }
        var serviceRequest = new PomoModels.UpdateRequest
        {
            Id = id,
            CompletedAt = pomoRequest.CompletedAt.Value,
            Intent = pomoRequest.Intent.Value,
            Description = pomoRequest.Description,
        };
        var result = service.Update(serviceRequest);
        return result is null ? Results.NotFound() : Results.Ok(result);
    }
    
    private static IResult Patch(Guid id, PomoRequest pomoRequest, IPomoService service)
    {
        var serviceRequest = new PomoModels.PatchRequest
        {
            Id = id,
            CompletedAt = pomoRequest.CompletedAt,
            Intent = pomoRequest.Intent,
            Description = pomoRequest.Description,
        };
        var result = service.Patch(serviceRequest);
        return result is null ? Results.NotFound() : Results.Ok(result);
    }
    
    private static IResult Delete(Guid id, IPomoService service)
    {
        service.Delete(id);
        return Results.Ok();
    }
    
    private sealed class PomoRequest
    {
        public DateTime? CompletedAt { get; set; }
        public Intent? Intent { get; set; }
        public string? Description { get; set; }
    }
}