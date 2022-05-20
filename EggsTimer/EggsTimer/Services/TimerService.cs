using System.Collections.Generic;
using System.Threading.Tasks;
using EggsTimer.Data;
using EggsTimer.Models;
using Microsoft.EntityFrameworkCore;

namespace EggsTimer.Services
{
    public class TimerService
    {
        public async Task<TimerModel> Create(TimerModel model)
        {
            using (var context = new DataContext())
            {
                context.Timers.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
        }

        public async Task<TimerModel> Delete(int id)
        {
            using (var context = new DataContext())
            {
                var entity = await context.Timers.FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                    return null;

                context.Remove(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<IEnumerable<TimerModel>> Read()
        {
            using (var context = new DataContext())
            {
                return await context.Timers.ToListAsync();
            }
        }

        public async Task<TimerModel> Read(int id)
        {
            using (var context = new DataContext())
            {
                return await context.Timers.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<TimerModel> Update(int id, TimerModel model)
        {
            using (var context = new DataContext())
            {
                //context.Timers.Update(model);

                var entity = await context.Timers.FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                    return null;

                context.Entry(entity).CurrentValues.SetValues(model);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}