using BrewLab.Models;
using BrewLab.Models.Base;
using BrewLab.Models.Models;

namespace BrewLab.Repository.Base;
public abstract class Repository<TModel>(BrewLabContext context) where TModel : class, IBrewLabModel<int>
{
    private readonly BrewLabContext _context = context;
    protected ICollection<TModel> Find(Func<TModel, bool> filter)
    {
        IQueryable<TModel>? result = null;

        if (TModelIs(typeof(IVirtualDeleteable)))
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

        return result.ToList();
    }

    protected void Delete(TModel model)
    {
        if (model is IVirtualDeleteable vDeleteableModel)
            vDeleteableModel.Deleted = true;
        else
            _context.Remove(model);

        if (model is ILoggedEntity loggedModel)
            loggedModel.UpdatedAt = DateTime.Now;

        _context.SaveChanges();
    }

    protected void Update(TModel model)
    {
        if (model is ILoggedEntity loggedModel)
            loggedModel.UpdatedAt = DateTime.Now;

        _context.Update(model);
    }

    protected void Insert(TModel model)
    {
        _context.Set<TModel>()
            .Add(model);

        _context.SaveChanges();
    }

    private static bool TModelIs(Type type) => type.IsAssignableFrom(typeof(TModel));
}
