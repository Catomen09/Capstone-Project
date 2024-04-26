using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
	public interface IGenericDal<T> where T : class
	{
		public void Add(T entity);
		public void Delete(T entity);
		public void Update(T entity);
		public T GetById(int id);
		public List<T> GetListAll();
	}
}
