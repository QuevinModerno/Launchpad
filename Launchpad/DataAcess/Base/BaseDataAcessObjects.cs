using Data.Context;
using Microsoft.EntityFrameworkCore;
/*
 *
 *utilizar transactionScope, sempre que quiseres fazer uma transacao na base de
dados. Por exemplo: num update, iniciar isso. Fazer as cenas do update. e finalizar
com . complete. Caso de merda aquilo faz rollback automaticamente.
 * 
 * 
 * 
 *
 * 
namespace DataAcess.Base
{

 public class BaseDataAcessObject<T> : IBaseDataAcessObjects<T> where T : Entity
 {
     private readonly ProjectManagementContext _context;
     public BaseDataAcessObject(ProjectManagementContext context)
     {
         _context = context;
     }

     public async Task<IEnumerable<T>> ListAsync()
     {
         return await _context.Set<T>().ToListAsync();

     }

     public async Task<T?> GetAsync(int id)
     {
         return await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);

     }

     public async Task DeleteAsync(int id)
     {
         var record = await GetAsync(id);
         if (record is null) return;
         _context.Set<T>().Remove(record);

         await _context.SaveChangesAsync();

     }
     public async Task<T> InsertAsync(T record)
     {
         var trackResult = await _context.AddAsync(record);
         await _context.SaveChangesAsync();
         return trackResult.Entity;

     }


     public async Task UpdateAsync(T record)
     {
         _context.Set<T>().Update(record);

         await _context.SaveChangesAsync();
     }

 }
}
*/