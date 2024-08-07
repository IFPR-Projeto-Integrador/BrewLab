﻿using BrewLab.Models;
using BrewLab.Models.Base;
using BrewLab.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BrewLab.Repository.Base;
public abstract class Repository<TModel>(BrewLabContext context) where TModel : class, IBrewLabModel<int>
{
    private readonly BrewLabContext _context = context;

    protected IQueryable<TEntity> Get<TEntity>() where TEntity : class, IBrewLabModel<int>
    {
        if (TModelIs<IVirtualDeleteable>())
        {
            return _context.Set<TEntity>()
                .Cast<IVirtualDeleteable>()
                .Where(v => v.Deleted == false)
                .Cast<TEntity>();
        }
        else
        {
            return _context.Set<TEntity>().AsQueryable();
        }
    }

    protected ICollection<TModel> Find(Func<TModel, bool> filter)
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

        return result
            .Where(filter)
            .ToList();
    }

    protected async Task<TModel?> FindSingle(Func<TModel, bool> filter) => Find(filter).FirstOrDefault();

    protected async Task Delete(TModel model)
    {
        using var trans = await _context.Database.BeginTransactionAsync();

        await DeleteWithoutSaving(model);

        await _context.SaveChangesAsync();

        await trans.CommitAsync();
    }

    private async Task DeleteWithoutSaving(TModel model)
    {
        if (model is ILoggedEntity loggedModel)
            loggedModel.UpdatedAt = DateTime.UtcNow;

        if (model is IVirtualDeleteable vDeleteableModel)
        {
            vDeleteableModel.Deleted = true;

            var virtualDeleteables = _context.Entry(model).Navigations
                .Where(e => e.CurrentValue is not null && e.CurrentValue is TModel && e.CurrentValue is IVirtualDeleteable)
                .Select(e => (TModel?)e.CurrentValue);

            foreach (var toDelete in virtualDeleteables)
            {
                if (toDelete is not null)
                    await DeleteWithoutSaving(toDelete);
            }
        }
        else
        {
            _context.Remove(model);
        }
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

    public bool Exists(Func<TModel, bool> filter)
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

    public bool Exists(int id)
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
            .Where(m => m.Id == id)
            .Any();
    }

    private static bool TModelIs<TCompare>() => typeof(TCompare).IsAssignableFrom(typeof(TModel));
}
