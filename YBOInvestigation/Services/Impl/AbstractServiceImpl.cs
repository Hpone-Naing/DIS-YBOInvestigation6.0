using YBOInvestigation.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using YBOInvestigation.Paging;
using YBOInvestigation.Classes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace YBOInvestigation.Services.Impl
{
    public class AbstractServiceImpl<T> : AbstractService<T> where T : class
    {
        protected readonly YBOInvestigationDBContext _context;

        public AbstractServiceImpl(YBOInvestigationDBContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public PagingList<T> GetAllWithPagin(List<T> list, int? pageNo, int pageSize)
        {
            return PagingList<T>.CreateAsync(list.AsQueryable<T>(), pageNo ?? 1, pageSize);
        }

        public List<T> GetUniqueList(Func<T, object> keySelector)
        {
            return _context.Set<T>().GroupBy(keySelector).Select(group => group.First()).ToList(); ;
        }

        protected bool IsSearchDataContained(object obj, string searchData, string column = null)
        {
            if (obj != null)
            {
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        if (column == null || string.Equals(prop.Name, column, StringComparison.OrdinalIgnoreCase))
                        {
                            string propValue = (string)prop.GetValue(obj);

                            if (propValue != null)
                            {
                                if (propValue.IndexOf(searchData, StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    return true;
                                }
                            }
                        }

                    }
                }
            }

            return false;
        }
        private IQueryable<T> FiltersWithoutAdvSearchOptions<T>(IQueryable<T> query, AdvanceSearch advanceSearch) where T : class
        {
            return query.Where(obj =>
                (!string.IsNullOrEmpty(advanceSearch.CngQty) && EF.Property<string>(obj, "CngQty") != null && EF.Property<string>(obj, "CngQty").ToLower().Contains(advanceSearch.CngQty.ToLower()))
                || (!string.IsNullOrEmpty(advanceSearch.CctvInstalled) && EF.Property<string>(obj, "CctvInstalled") != null && EF.Property<string>(obj, "CctvInstalled").ToLower().Contains(advanceSearch.CctvInstalled.ToLower()))
                );
        }

        private bool FilterAdvSearchOptions(Object obj, string colName, string advSearchString, string advSearchOption)
        {
            string columnValue = (string)EF.Property<string>(obj, colName);
            if (int.TryParse(columnValue, out int parseIntColumnValue))
            {
                var parseIntAdvSeaerchString = int.Parse(advSearchString);
                switch (advSearchOption)
                {
                    case ">":
                        return parseIntColumnValue > parseIntAdvSeaerchString;
                    case ">=":
                        return parseIntColumnValue >= parseIntAdvSeaerchString;
                    case "<":
                        return parseIntColumnValue < parseIntAdvSeaerchString;
                    case "<=":
                        return parseIntColumnValue <= parseIntAdvSeaerchString;
                    default:
                        return false;//throw new ArgumentException($"Invalid option: {option}");
                }
            }
            else
            {
                return false;
            }
        }

        public List<T> AdvanceSearch<T>(AdvanceSearch advanceSearch, DbSet<T> dbSet) where T : class
        {
            var query = dbSet.AsQueryable();
            query = FiltersWithoutAdvSearchOptions(query, advanceSearch);
            return query.ToList();
        }

        public T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T FindByString(string columnName, string str)
        {
            return _context.Set<T>().FirstOrDefault(entity =>
             EF.Property<string>(entity, columnName) == str);
        }

        public List<T> GetListByIntVal(string columnName, int intVal)
        {
            return _context.Set<T>().Where(entity =>
            EF.Property<int>(entity, columnName) == intVal).ToList();
        }

        public T GetObjByIntVal(string columnName, int intVal)
        {
            return _context.Set<T>().FirstOrDefault(entity =>
            EF.Property<int>(entity, columnName) == intVal);
        }

        public List<SelectListItem> GetItemsFromList<T>(List<T> list, string valuePropertyName, string textPropertyName)
        {
            var lstItems = new List<SelectListItem>();

            foreach (T item in list)
            {
                var itemType = item.GetType();
                var valueProperty = itemType.GetProperty(valuePropertyName);
                var textProperty = itemType.GetProperty(textPropertyName);

                if (valueProperty != null && textProperty != null)
                {
                    var value = valueProperty.GetValue(item)?.ToString();
                    var text = textProperty.GetValue(item)?.ToString();

                    lstItems.Add(new SelectListItem
                    {
                        Value = value,
                        Text = text
                    });
                }
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "ရွေးချယ်ရန်"
            };

            lstItems.Insert(0, defItem);
            return lstItems;
        }

        public bool Create(T t)
        {
            try
            {
                _context.Set<T>().Add(t);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public bool Update(T t)
        {
            try
            {
                _context.Entry(t).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
