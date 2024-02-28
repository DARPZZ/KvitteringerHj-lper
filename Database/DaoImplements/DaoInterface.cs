using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvitteringer.Database.DaoImplements
{
    public interface DaOInterface<T>
    {
        public abstract void Create(T t);

        public abstract void Update(T t, String fieldname, String value);

        public abstract void Delete(T t, int ID);
        public abstract T Get(int ID);
        List<T> GetAll();

    }
}
