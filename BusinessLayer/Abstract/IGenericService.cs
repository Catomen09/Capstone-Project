using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IGenericService<T> where T : class
	{
		public void TAdd(T entity);
		public void TDelete(T entity);
		public void TUpdate(T entity);
		public T TGetById(int id);
		List<T> TGetListAll();
	}
}
