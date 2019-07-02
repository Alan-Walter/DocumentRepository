using NHibernate;
using NHibernate.Context;
using System;
using System.Web.Mvc;

namespace DocumentRepositoryApp
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class NHibernateSessionAttribute: ActionFilterAttribute
    {
        protected ISessionFactory SessionFactory => MvcApplication.SessionFactory;

        public NHibernateSessionAttribute()
        {
            Order = 100;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var session = CurrentSessionContext.Unbind(SessionFactory);
            session.Close();
        }
    }
}