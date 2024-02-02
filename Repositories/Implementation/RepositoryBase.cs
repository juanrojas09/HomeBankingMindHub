using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace HomeBankingNetMvc.Repositories.Implementation
{
    // a modo de anotacion, "where T:class" indica que es una referencia a una clase n, no a un tipo especifico. Para tratar con operaciones ABM con un tipo generico
    public class RepositoryBase<T> : IRepositoryBase<T> where T:class
    {

        protected readonly HomeBankingContext _context;

        public RepositoryBase(HomeBankingContext context)
        {
            _context = context;
        }


        public void Create(T entity)
        {
            try
            {
                this._context.Set<T>().Add(entity);
                     
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear entidad" + ex.Message);
            }
        }
        public void Delete(T entity)
        {
            try
            {

            }catch(Exception ex)
            {
                throw new Exception("Error al eliminar entidad" + ex.Message);
            }
        }

        public IQueryable<T> FindAll()
        {
            try
            {
               return this._context.Set<T>().AsNoTrackingWithIdentityResolution();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar entidades" + ex.Message);
            }
        }

        /// <summary>
        /// Lista una coleccion de entidades, pero parametrizada con dos paramteros.
        /// </summary>
        /// <param name="includes">
        /// el primero también puede ser un IQueryable, al cual le podemos pasar una condición (por ejemplo todos los objetos que tengan un rango de fechas, etc)
        /// el segundo IIncludableQuery nos permite decidir si queremos que además incluya otros elementos de un objeto(Calculo que los objetos asociados, ejemplo. Las cuentas del cliente)
        /// 
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IQueryable<T> FindAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            try
            {
                //El set devuelve un tipo de dato en la entidad
                IQueryable<T> queryable = this._context.Set<T>();
                if (includes != null)
                {
                    queryable = includes(queryable);
                }
                return queryable.AsNoTrackingWithIdentityResolution();

            }
            catch(Exception ex)
            {
                throw new Exception("Error al buscar todas las entidades"+ex.Message);
            }
        }

        /// <summary>
        /// Busca la entidad apartir de una expresion "el lambda que iria en el where", pero que es generico y lo toma del parametro
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            try
            {
                return this._context.Set<T>().Where(expression).AsNoTrackingWithIdentityResolution();

            }catch(Exception ex)
            {
                throw new Exception("Error al buscar entidad por una condicion"+ex.Message);
            }
        }

        public void SaveChanges()
        {
            try
            {
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar" + ex.Message);
            }
        }

        public void Update(T entity)
        {
            try
            {
                this._context.Set<T>().Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar entidad" + ex.Message);
            }
        }
    }
}
