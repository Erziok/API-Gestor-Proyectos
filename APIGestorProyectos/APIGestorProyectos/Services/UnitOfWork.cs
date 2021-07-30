using APIGestorProyectos.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Generic
{
    public interface IUnitOfWork : IDisposable
    {
        gestorproyectosContext Context { get; }
        void Commit();
        object Entry(Empresa empresa);
    }

    public class UnitOfWork : IUnitOfWork
    {
        public gestorproyectosContext Context { get; }

        public UnitOfWork(gestorproyectosContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public object Entry(Empresa empresa)
        {
            throw new NotImplementedException();
        }
    }
}
