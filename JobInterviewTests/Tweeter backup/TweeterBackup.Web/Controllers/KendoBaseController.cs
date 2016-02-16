namespace TweeterBackup.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using TweeterBackup.Data;
    using TweeterBackup.Data.Common.Repository;
    using TweeterBackup.Logic;
    using TweeterBackup.Web.Extensions;
    using TweeterBackup.Web.Infrastructure.UserProvider;

    public abstract class KendoBaseController<T, Tmodel> : BaseController where T : class
    {
        public KendoBaseController(ITweeterBackupData backupData, ITwitter tweeter, IUserIdProvider userProvider)
            : base(backupData, tweeter, userProvider)
        {
        }

        public virtual ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var queryModel = this.GetAllData()      
                                 .Project().To<Tmodel>();
            DataSourceResult result = queryModel.ToDataSourceResult(request);
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Destroy([DataSourceRequest] DataSourceRequest request, Tmodel newModel)
        {
            if (newModel != null)
            {
                var databaseModel = this.GetById(newModel);
                if (databaseModel != null)
                {
                    this.GetData().Delete(databaseModel);
                    this.BackupData.SaveChanges();
                }
            }

            return this.JsonKendoResult(newModel, request);
        }

        protected abstract IRepository<T> GetData();

        protected abstract IQueryable<T> GetAllData();

        protected abstract object ModelId(Tmodel model);

        protected ActionResult JsonKendoResult(Tmodel newModel, DataSourceRequest request)
        {
            return this.Json(new[] { newModel }.ToDataSourceResult(request, this.ModelState));
        }

        protected T GetById(Tmodel newModel)
        {
            return this.GetData().GetById(this.ModelId(newModel));
        }
    }
}