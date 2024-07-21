namespace PomoAssistant.Services;

public interface IPomoService
{
    PomoModels.ReadModel? GetById(Guid id);
    List<PomoModels.ReadModel> Search(PomoModels.SearchRequest request);
    PomoModels.ReadModel Create(PomoModels.CreateRequest request);
    PomoModels.ReadModel? Update(PomoModels.UpdateRequest request);
    PomoModels.ReadModel? Patch(PomoModels.PatchRequest request);
    void Delete(Guid id);
}