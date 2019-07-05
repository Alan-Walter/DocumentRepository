using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace DocumentRepositoryApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        const string ScriptFileName = @"account_script.sql";

        private static ISessionFactory sessionFactory;

        //  To get the NHibernate session for the current web request, we use
        //  SessionFactory.GetCurrentSession(). 
        public static ISessionFactory SessionFactory
        {
            get
            {
                if(sessionFactory == null)
                {
                    var config = new Configuration().Configure();
                    var mapper = new ModelMapper();
                    mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
                    HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
                    config.AddMapping(mapping);
                    new SchemaUpdate(config).Execute(true, true);
                    sessionFactory = config.BuildSessionFactory();
                }
                return sessionFactory;
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            using(var s = SessionFactory.OpenSession())
            {
                string script = string.Empty;
                using (var reader = new StreamReader(Server.MapPath("~/" + ScriptFileName)))
                    script = reader.ReadToEnd();
                ISQLQuery query = s.CreateSQLQuery(script);
                query.ExecuteUpdate();
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var session = CurrentSessionContext.Unbind(SessionFactory);
            session.Dispose();
        }
    }
}