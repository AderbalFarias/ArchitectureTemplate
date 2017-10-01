using ArchitectureTemplate.Infrastructure.CrossCutting.IoC;
using ArchitectureTemplate.Infrastructure.WindowService.Service.Services;
using SimpleInjector;
using System.Configuration;

namespace ArchitectureTemplate.Infrastructure.WindowService.Service
{
    partial class StartService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            timerStart = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(timerStart)).BeginInit();

            timerStart.Enabled = true;
            timerStart.Interval = double.Parse(ConfigurationManager.AppSettings["Timer"].ToString());
            timerStart.Elapsed += new System.Timers.ElapsedEventHandler(timerStart_Elapsed);

            ServiceName = "StartService";
            ((System.ComponentModel.ISupportInitialize)(timerStart)).EndInit();
        }

        private void InitializeContainer(Container container)
        {
            BootstrapperService.RegisterServices(_container);
            container.Register<UserService>();
        }

        #endregion

        private System.Timers.Timer timerStart;
    }
}
