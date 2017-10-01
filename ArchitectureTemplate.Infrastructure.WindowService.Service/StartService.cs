using ArchitectureTemplate.Infrastructure.WindowService.Service.Services;
using SimpleInjector;
using System;
using System.ServiceProcess;

namespace ArchitectureTemplate.Infrastructure.WindowService.Service
{
    public partial class StartService : ServiceBase
    {
        #region Fields

        private readonly Container _container = new Container();

        #endregion

        #region Constructors

        public StartService()
        {
            InitializeContainer(_container);
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
                //if (ConfigurationManager.AppSettings["CreateLog"] == "true")
                //UtilService.Log($"[{DateTime.Now.ToShortTimeString()}] {ex.Message}", ConfigurationManager.AppSettings["Log"]);
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
