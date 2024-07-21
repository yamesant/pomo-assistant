namespace PomoAssistant.Services;

public sealed class PomoService : IPomoService
{
    public PomoModels.ReadModel? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<PomoModels.ReadModel> Search(PomoModels.SearchRequest request)
    {
        throw new NotImplementedException();
    }

    public PomoModels.ReadModel Create(PomoModels.CreateRequest request)
    {
        throw new NotImplementedException();
    }

    public PomoModels.ReadModel? Update(PomoModels.UpdateRequest request)
    {
        throw new NotImplementedException();
    }

    public PomoModels.ReadModel? Patch(PomoModels.PatchRequest request)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}