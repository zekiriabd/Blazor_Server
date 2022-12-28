using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Test.Data;

namespace Test
{
    public partial class TestxService
    {
        TestxContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly TestxContext context;
        private readonly NavigationManager navigationManager;

        public TestxService(TestxContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportCategoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/testx/categories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/testx/categories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCategoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/testx/categories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/testx/categories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCategoriesRead(ref IQueryable<Models.Testx.Category> items);

        public async Task<IQueryable<Models.Testx.Category>> GetCategories(Query query = null)
        {
            var items = Context.Categories.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnCategoriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCategoryCreated(Models.Testx.Category item);
        partial void OnAfterCategoryCreated(Models.Testx.Category item);

        public async Task<Models.Testx.Category> CreateCategory(Models.Testx.Category category)
        {
            OnCategoryCreated(category);

            var existingItem = Context.Categories
                              .Where(i => i.CategoryId == category.CategoryId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Categories.Add(category);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(category).State = EntityState.Detached;
                throw;
            }

            OnAfterCategoryCreated(category);

            return category;
        }
        public async Task ExportRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/testx/roles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/testx/roles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/testx/roles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/testx/roles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRolesRead(ref IQueryable<Models.Testx.Role> items);

        public async Task<IQueryable<Models.Testx.Role>> GetRoles(Query query = null)
        {
            var items = Context.Roles.AsQueryable();

            items = items.Include(i => i.User);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRoleCreated(Models.Testx.Role item);
        partial void OnAfterRoleCreated(Models.Testx.Role item);

        public async Task<Models.Testx.Role> CreateRole(Models.Testx.Role role)
        {
            OnRoleCreated(role);

            var existingItem = Context.Roles
                              .Where(i => i.id == role.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Roles.Add(role);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(role).State = EntityState.Detached;
                role.User = null;
                throw;
            }

            OnAfterRoleCreated(role);

            return role;
        }
        public async Task ExportUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/testx/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/testx/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/testx/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/testx/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsersRead(ref IQueryable<Models.Testx.User> items);

        public async Task<IQueryable<Models.Testx.User>> GetUsers(Query query = null)
        {
            var items = Context.Users.AsQueryable();

            items = items.Include(i => i.Category);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUserCreated(Models.Testx.User item);
        partial void OnAfterUserCreated(Models.Testx.User item);

        public async Task<Models.Testx.User> CreateUser(Models.Testx.User user)
        {
            OnUserCreated(user);

            var existingItem = Context.Users
                              .Where(i => i.UserId == user.UserId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Users.Add(user);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(user).State = EntityState.Detached;
                user.Category = null;
                throw;
            }

            OnAfterUserCreated(user);

            return user;
        }

        partial void OnCategoryDeleted(Models.Testx.Category item);
        partial void OnAfterCategoryDeleted(Models.Testx.Category item);

        public async Task<Models.Testx.Category> DeleteCategory(int? categoryId)
        {
            var itemToDelete = Context.Categories
                              .Where(i => i.CategoryId == categoryId)
                              .Include(i => i.Users)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCategoryDeleted(itemToDelete);

            Context.Categories.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCategoryDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCategoryGet(Models.Testx.Category item);

        public async Task<Models.Testx.Category> GetCategoryByCategoryId(int? categoryId)
        {
            var items = Context.Categories
                              .AsNoTracking()
                              .Where(i => i.CategoryId == categoryId);

            var itemToReturn = items.FirstOrDefault();

            OnCategoryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Testx.Category> CancelCategoryChanges(Models.Testx.Category item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCategoryUpdated(Models.Testx.Category item);
        partial void OnAfterCategoryUpdated(Models.Testx.Category item);

        public async Task<Models.Testx.Category> UpdateCategory(int? categoryId, Models.Testx.Category category)
        {
            OnCategoryUpdated(category);

            var itemToUpdate = Context.Categories
                              .Where(i => i.CategoryId == categoryId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(category);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterCategoryUpdated(category);

            return category;
        }

        partial void OnRoleDeleted(Models.Testx.Role item);
        partial void OnAfterRoleDeleted(Models.Testx.Role item);

        public async Task<Models.Testx.Role> DeleteRole(int? id)
        {
            var itemToDelete = Context.Roles
                              .Where(i => i.id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRoleDeleted(itemToDelete);

            Context.Roles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRoleDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnRoleGet(Models.Testx.Role item);

        public async Task<Models.Testx.Role> GetRoleByid(int? id)
        {
            var items = Context.Roles
                              .AsNoTracking()
                              .Where(i => i.id == id);

            items = items.Include(i => i.User);

            var itemToReturn = items.FirstOrDefault();

            OnRoleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Testx.Role> CancelRoleChanges(Models.Testx.Role item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRoleUpdated(Models.Testx.Role item);
        partial void OnAfterRoleUpdated(Models.Testx.Role item);

        public async Task<Models.Testx.Role> UpdateRole(int? id, Models.Testx.Role role)
        {
            OnRoleUpdated(role);

            var itemToUpdate = Context.Roles
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(role);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterRoleUpdated(role);

            return role;
        }

        partial void OnUserDeleted(Models.Testx.User item);
        partial void OnAfterUserDeleted(Models.Testx.User item);

        public async Task<Models.Testx.User> DeleteUser(int? userId)
        {
            var itemToDelete = Context.Users
                              .Where(i => i.UserId == userId)
                              .Include(i => i.Roles)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUserDeleted(itemToDelete);

            Context.Users.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUserDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnUserGet(Models.Testx.User item);

        public async Task<Models.Testx.User> GetUserByUserId(int? userId)
        {
            var items = Context.Users
                              .AsNoTracking()
                              .Where(i => i.UserId == userId);

            items = items.Include(i => i.Category);

            var itemToReturn = items.FirstOrDefault();

            OnUserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Testx.User> CancelUserChanges(Models.Testx.User item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUserUpdated(Models.Testx.User item);
        partial void OnAfterUserUpdated(Models.Testx.User item);

        public async Task<Models.Testx.User> UpdateUser(int? userId, Models.Testx.User user)
        {
            OnUserUpdated(user);

            var itemToUpdate = Context.Users
                              .Where(i => i.UserId == userId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(user);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterUserUpdated(user);

            return user;
        }
    }
}
