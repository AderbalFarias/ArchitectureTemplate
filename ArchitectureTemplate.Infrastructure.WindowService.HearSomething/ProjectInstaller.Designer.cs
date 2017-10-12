namespace ArchitectureTemplate.Infrastructure.WindowService.HearSomething
{
    partial class ProjectInstaller
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
            this.serviceProcessInstallerArchitectureTemplate = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstallerArchitectureTemplate = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstallerArchitectureTemplate
            // 
            this.serviceProcessInstallerArchitectureTemplate.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstallerArchitectureTemplate.Password = null;
            this.serviceProcessInstallerArchitectureTemplate.Username = null;
            // 
            // serviceInstallerArchitectureTemplate
            // 
            this.serviceInstallerArchitectureTemplate.Description = "Windows Service ArchitectureTemplate";
            this.serviceInstallerArchitectureTemplate.DisplayName = "ArchitectureTemplate.Service";
            this.serviceInstallerArchitectureTemplate.ServiceName = "ArchitectureTemplate Service";
            this.serviceInstallerArchitectureTemplate.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstallerArchitectureTemplate,
            this.serviceInstallerArchitectureTemplate});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstallerArchitectureTemplate;
        private System.ServiceProcess.ServiceInstaller serviceInstallerArchitectureTemplate;
    }
}