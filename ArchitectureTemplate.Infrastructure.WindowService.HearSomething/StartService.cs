using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infrastructure.WindowService.HearSomething.Services;
using SimpleInjector;
using System;
using System.Configuration;
using System.ServiceProcess;

namespace ArchitectureTemplate.Infrastructure.WindowService.HearSomething
{
    public partial class StartService : ServiceBase
    {
        #region Fields

        private readonly Container _container = new Container();

        #endregion

        #region Constructors

        public StartService()
        {
            InitializeContainer();
            InitializeComponent();
        }

        #endregion

        #region Methods Protected

        protected override void OnStart(string[] args)
        {
            timerStart.Start();
        }

        protected override void OnStop()
        {
            timerStart.Stop();
        }

        #endregion

        #region Methods Private

        private void timerStart_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //int hora = int.Parse(ConfigurationManager.AppSettings["HoraInicio"]);
                //if (DateTime.Now.Hour < hora) return;

                _container.GetInstance<UserService>().Action();

                timerStart.Stop();
                //code to do something here

            }
            catch (Exception ex)
            {
                LogFile.Create(ex, ConfigurationManager.AppSettings["Log"]);
                //if (ConfigurationManager.AppSettings["CreateLog"] == "true")
            }
            finally
            {
                GC.SuppressFinalize(this);
                timerStart.Start();
            }
        }

        #endregion
    }
}
