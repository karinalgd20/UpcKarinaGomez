using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UPC.Intranet.Modelo.Dto.Request;
using UPC.Intranet.Modelo.Dto.Response;
using UPC.Intranet.Modelo.Modelo;

namespace UPC.Intranet.Datos
{
    public class UpcContext : DbContext, IQueryableUnitOfWork
    {
        public UpcContext() : base("name=cnUPC")
        {
            Database.Log = (sql) => Debug.Write(sql);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.ValidateOnSaveEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<UpcContext>(null);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Solicitud>()
               .ToTable("SOLICITUD");


            modelBuilder.Entity<Detalle_Solicitud>()
                 .ToTable("DETALLE_SOLICITUD");
        }

        public virtual DbSet<Solicitud> Solicitud { get; set; }
        public virtual DbSet<Detalle_Solicitud> Detalle_Solicitud { get; set; }


        #region IQueryableUnitOfWork
        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            base.Entry<TEntity>(item).State = EntityState.Unchanged;
        }
        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            base.Entry<TEntity>(item).State = EntityState.Modified;
        }
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            base.Entry<TEntity>(original).CurrentValues.SetValues(current);
        }
        public void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }
        public async Task<int> CommitAsync()
        {
            return await base.SaveChangesAsync();
        }
        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;
            do
            {
                try
                {
                    base.SaveChanges();
                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });
                }
            } while (saveFailed);
        }
        public void RollbackChanges()
        {
            base.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);
        }
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }
        public async Task<int> ExecuteCommandAsync(string sqlCommand, params object[] parameters)
        {
            return await base.Database.ExecuteSqlCommandAsync(sqlCommand, parameters);
        }
        public DbContextTransaction BeginTransaction()
        {
            return base.Database.BeginTransaction();
        }

        #endregion

        public Detalle_SolicitudDtoResponse ListarDetalleSolicitud(Detalle_SolicitudDtoRequest dto)
        {
            object[] parametros = {
                new OracleParameter("P_COD_LINEA_NEGOCIO", OracleDbType.Char,( dto.COD_LINEA_NEGOCIO ==null) ? "" :dto.COD_LINEA_NEGOCIO, ParameterDirection.Input),
                new OracleParameter("P_COD_MODAL_EST", OracleDbType.Char,( dto.COD_MODAL_EST ==null) ? "" :dto.COD_MODAL_EST, ParameterDirection.Input),
                new OracleParameter("P_COD_PERIODO", OracleDbType.Char, (dto.COD_PERIODO ==null) ?  "" :dto.COD_PERIODO , ParameterDirection.Input),
                new OracleParameter("P_COD_TRAMITE", OracleDbType.Int32, (dto.COD_TRAMITE ==null) ? 0 : dto.COD_TRAMITE, ParameterDirection.Input),
                new OracleParameter("P_ESTADO", OracleDbType.Varchar2, (dto.ESTADO ==null) ? "" : dto.ESTADO, ParameterDirection.Input),
                new OracleParameter("RS_RESULTADO", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            var consulta = ExecuteQuery<Detalle_SolicitudDtoResponse>("BEGIN UPC_LISTA_SOLICITUD_DETALLE(:P_COD_LINEA_NEGOCIO, :P_COD_MODAL_EST,:P_COD_PERIODO,:P_COD_TRAMITE,:P_ESTADO,:P_RESULTADO); end;", parametros).ToList();
            var datosconsulta = new Detalle_SolicitudDtoResponse
            {
                ListDetalle_SolicitudDtoResponse = consulta
            };
            return datosconsulta;
        }
    }

    public abstract class BaseDomainContext : DbContext
    {
        static BaseDomainContext()
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }

}
