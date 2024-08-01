using BrewLab.Models;
using BrewLab.Models.Base;
using BrewLab.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BrewLab.Repository.Base;
public abstract class Repository<TModel>(BrewLabContext context) where TModel : class, IBrewLabModel<int>
{
    private readonly BrewLabContext _context = context;
    protected async Task<ICollection<TModel>> Find(Func<TModel, bool> filter)
    {
        IQueryable<TModel>? result = null;

        if (TModelIs<IVirtualDeleteable>())
        {
            result = _context.Set<TModel>()
                .Cast<IVirtualDeleteable>()
                .Where(v => v.Deleted == false)
                .Cast<TModel>();
        }
        else
        {
            result = _context.Set<TModel>().AsQueryable();
        }

        return await result.ToListAsync();
    }

    protected async Task<TModel?> FindSingle(Func<TModel, bool> filter) => (await Find(filter)).FirstOrDefault();

    protected async Task Delete(TModel model)
    {
        if (model is IVirtualDeleteable vDeleteableModel)
            vDeleteableModel.Deleted = true;
        else
            _context.Remove(model);

        if (model is ILoggedEntity loggedModel)
            loggedModel.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    protected async Task Update(TModel model)
    {
        if (model is ILoggedEntity loggedModel)
            loggedModel.UpdatedAt = DateTime.UtcNow;

        _context.Update(model);

        await _context.SaveChangesAsync();
    }

    protected async Task Insert(TModel model)
    {
        _context.Set<TModel>()
            .Add(model);

        await _context.SaveChangesAsync();
    }

    protected bool Exists(Func<TModel, bool> filter)
    {
        IQueryable<TModel> results = null!;

        if (TModelIs<IVirtualDeleteable>())
            results = _context.Set<TModel>()
                .Cast<IVirtualDeleteable>()
                .Where(d => d.Deleted == false)
                .Cast<TModel>();
        else
            results = _context.Set<TModel>()
                .AsQueryable();

        return results
            .Where(filter)
            .Any();
    }

    private static bool TModelIs<TCompare>() => typeof(TCompare).IsAssignableFrom(typeof(TModel));
}
